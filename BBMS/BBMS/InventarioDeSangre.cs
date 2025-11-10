using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBMS
{
    public partial class InventarioDeSangre : Form
    {
        // Cadena de conexión (mover a App.config si quieres)
        private readonly string connStr = "Data Source=FIDEV;Initial Catalog=BancoDeSangre;Persist Security Info=True;User ID=sa;Password=Delta92_$1911;TrustServerCertificate=True";

        public InventarioDeSangre()
        {
            InitializeComponent();

            // Configura el ComboBox de filtro si existe
            if (this.Controls.Find("BGroupFilterCb", true).Length > 0)
            {
                var cb = this.Controls.Find("BGroupFilterCb", true)[0] as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;

                    // Añade "Todos" al inicio
                    if (!cb.Items.Contains("Todos"))
                        cb.Items.Insert(0, "Todos");

                    // Evita doble suscripción al evento
                    cb.SelectedIndexChanged -= BGroupFilterCb_SelectedIndexChanged;
                    cb.SelectedIndexChanged += BGroupFilterCb_SelectedIndexChanged;

                    // Selecciona "Todos" por defecto
                    if (cb.SelectedIndex == -1)
                        cb.SelectedIndex = 0;
                }
            }

            bloodStock(); // carga inicial sin filtro
        }

        // Carga BloodTbl; aplica filtro si bgroup no es "Todos"
        private void bloodStock(string bgroup = null)
        {
            try
            {
                DataTable dt = new DataTable();

                if (string.IsNullOrWhiteSpace(bgroup) || bgroup == "Todos")
                {
                    using (var con = new SqlConnection(connStr))
                    using (var sda = new SqlDataAdapter("SELECT BGroup, BStock FROM BloodTbl ORDER BY BGroup", con))
                    {
                        sda.Fill(dt);
                    }
                }
                else
                {
                    using (var con = new SqlConnection(connStr))
                    using (var cmd = new SqlCommand("SELECT BGroup, BStock FROM BloodTbl WHERE BGroup = @bg", con))
                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("@bg", bgroup);
                        sda.Fill(dt);
                    }
                }

                BloodStockDGV.DataSource = dt;

                // Configura DataGridView
                BloodStockDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                BloodStockDGV.MultiSelect = false;
                BloodStockDGV.ReadOnly = true;

                // Ajusta columnas
                if (BloodStockDGV.Columns.Count > 0)
                    BloodStockDGV.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar inventario de sangre: " + ex.Message);
            }
        }

        // Evento del ComboBox filtro
        private void BGroupFilterCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cb = sender as ComboBox;
                if (cb == null) return;

                var sel = cb.SelectedItem != null ? cb.SelectedItem.ToString() : "Todos";
                if (sel == "Todos")
                    bloodStock(null);
                else
                    bloodStock(sel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar filtro: " + ex.Message);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Opcional
        }

        private void InventarioDeSangre_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Donante Ob = new Donante();
            Ob.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Donar Ob = new Donar();
            Ob.Show();
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
    }
}