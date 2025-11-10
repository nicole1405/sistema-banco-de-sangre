using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BBMS
{
    public partial class Donar : Form
    {
        public Donar()
        {
            InitializeComponent();
            populate();
            bloodStock();
            // Asegura que el evento SelectionChanged exista si alguien selecciona con teclado
            DonorsDGV.SelectionChanged += DonorsDGV_SelectionChanged;
        }

        // Mejor mantener la cadena en una sola variable; considerar moverla a App.config
        private readonly string connStr = "Data Source=FIDEV;Initial Catalog=BancoDeSangre;Persist Security Info=True;User ID=sa;Password=Delta92_$1911;TrustServerCertificate=True";

        // Llena el DataGridView de forma robusta
        private void populate()
        {
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var sda = new SqlDataAdapter("SELECT * FROM DonorTbl", con))
                {
                    var ds = new DataSet();
                    sda.Fill(ds);
                    DonorsDGV.DataSource = ds.Tables[0];

                    // Opciones de UX recomendadas
                    DonorsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DonorsDGV.MultiSelect = false;
                    DonorsDGV.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar donantes: " + ex.Message);
            }
        }


        private void bloodStock()
        {
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var sda = new SqlDataAdapter("SELECT * FROM BloodTbl", con))
                {
                    var ds = new DataSet();
                    sda.Fill(ds);
                    BloodStockDGV.DataSource = ds.Tables[0];

                    // Opciones de UX recomendadas
                    BloodStockDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    BloodStockDGV.MultiSelect = false;
                    BloodStockDGV.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar donantes: " + ex.Message);
            }
        }

        int oldstock;
        private void GetStock(string bgroup)
        {
            // Valor por defecto
            oldstock = 0;

            if (string.IsNullOrWhiteSpace(bgroup))
                return;

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("SELECT BStock FROM BloodTbl WHERE BGroup = @bg", con))
                {
                    cmd.Parameters.AddWithValue("@bg", bgroup);
                    con.Open();

                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        // Intentamos parsear de forma segura
                        if (!int.TryParse(result.ToString(), out oldstock))
                            oldstock = 0;
                    }
                    else
                    {
                        oldstock = 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error SQL al obtener stock: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener stock: " + ex.Message);
            }
        }




        // Maneja click en celdas de forma segura
        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DNameTb.Text = DonorsDGV.SelectedRows[0].Cells[1].Value.ToString();
            BGroupTb.Text = DonorsDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        // También cuando cambia selección (teclado, fila completa, etc.)
        private void DonorsDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DonorsDGV.CurrentRow == null) return;
            PopulateFieldsFromRowIndex(DonorsDGV.CurrentRow.Index);
        }

        // Método helper para asignar campos desde una fila (con comprobaciones)
        private void PopulateFieldsFromRowIndex(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= DonorsDGV.Rows.Count) return;
                var row = DonorsDGV.Rows[rowIndex];

                // Comprueba existencia de celdas e nulos antes de asignar
                if (row.Cells.Count > 1 && row.Cells[1].Value != null)
                    DNameTb.Text = row.Cells[1].Value.ToString();
                else
                    DNameTb.Text = "";

                // DBGroup suele ser la última columna (índice 6 en tu BD); comprobar rango
                if (row.Cells.Count > 6 && row.Cells[6].Value != null)
                    BGroupTb.Text = row.Cells[6].Value.ToString();
                else
                    BGroupTb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar fila: " + ex.Message);
            }
        }

        private void reset()
        {
            DNameTb.Text = "";
            BGroupTb.Text = "";
        }

        // Lógica de donar: incrementa BStock para el grupo sanguíneo seleccionado
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DNameTb.Text))
            {
                MessageBox.Show("Selecciona un donador");
                return;
            }

            if (string.IsNullOrWhiteSpace(BGroupTb.Text))
            {
                MessageBox.Show("Grupo sanguíneo no disponible");
                return;
            }

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    con.Open();

                    // Intentamos incrementar el stock del grupo
                    using (var cmd = new SqlCommand("UPDATE BloodTbl SET BStock = BStock + 1 WHERE BGroup = @bgroup", con))
                    {
                        cmd.Parameters.AddWithValue("@bgroup", BGroupTb.Text);
                        int rows = cmd.ExecuteNonQuery();

                        // Si no existía el grupo, insertamos un registro nuevo con stock inicial 1
                        if (rows == 0)
                        {
                            using (var insertCmd = new SqlCommand("INSERT INTO BloodTbl (BGroup, BStock) VALUES (@bgroup, 1)", con))
                            {
                                insertCmd.Parameters.AddWithValue("@bgroup", BGroupTb.Text);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Donación exitosa");

                // Refrescar la grilla que muestra BloodTbl
                bloodStock();

                // Actualiza el valor en memoria si lo necesitas
                GetStock(BGroupTb.Text);

                // Limpiar campos de la UI
                reset();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error al actualizar stock (SQL): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la donación: " + ex.Message);
            }
        }

        private void Donar_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void BloodStockDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Donante Db = new Donante();
            Db.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Verdonantes Ob = new Verdonantes();
            Ob.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Ob = new Paciente();
            Ob.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ListaPacientes Ob = new ListaPacientes();
            Ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            InventarioDeSangre Ob = new InventarioDeSangre();
            Ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            TransfusionDeSangre Ob = new TransfusionDeSangre();
            Ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            PanelPrincipal Ob = new PanelPrincipal();
            Ob.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Ob = new Login();
            Ob.Show();
            this.Hide();
        }
    }
}