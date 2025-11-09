using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBMS
{
    public partial class ListaPacientes : Form
    {
        public ListaPacientes()
        {
            InitializeComponent();
            // Configuración de DataGridView para selección segura
            PatientsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PatientsDGV.MultiSelect = false;
            PatientsDGV.ReadOnly = true;
            PatientsDGV.SelectionChanged += PatientsDGV_SelectionChanged;

            populate();
        }

        // Conexión a la base de datos
        SqlConnection Con = new SqlConnection("Data Source=FIDEV;Initial Catalog=BancoDeSangre;Persist Security Info=True;User ID=sa;Password=Delta92_$1911;TrustServerCertificate=True");

        // Método para llenar la tabla de pacientes
        private void populate()
        {
            try
            {
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var sda = new SqlDataAdapter("SELECT * FROM PatientTbl", con))
                {
                    var ds = new DataSet();
                    sda.Fill(ds);
                    PatientsDGV.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de pacientes: " + ex.Message);
            }
        }

        int key = 0;

        private void ListaPacientes_Load(object sender, EventArgs e)
        {
        }

        // Manejo seguro de clic en celda
        private void PatientsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PopulateFieldsFromRowIndex(e.RowIndex);
        }

        // Manejo cuando cambia la selección (click, teclado, etc.)
        private void PatientsDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (PatientsDGV.CurrentRow == null) return;
            PopulateFieldsFromRowIndex(PatientsDGV.CurrentRow.Index);
        }

        // Método helper para extraer valores desde la fila de forma segura
        private void PopulateFieldsFromRowIndex(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= PatientsDGV.Rows.Count) return;
                var row = PatientsDGV.Rows[rowIndex];

                PNameTb.Text = (row.Cells.Count > 1 && row.Cells[1].Value != null) ? row.Cells[1].Value.ToString() : "";
                PAgeTb.Text = (row.Cells.Count > 2 && row.Cells[2].Value != null) ? row.Cells[2].Value.ToString() : "";
                PphoneTb.Text = (row.Cells.Count > 3 && row.Cells[3].Value != null) ? row.Cells[3].Value.ToString() : "";
                PGenCb.Text = (row.Cells.Count > 4 && row.Cells[4].Value != null) ? row.Cells[4].Value.ToString() : "";
                PBGroupCb.Text = (row.Cells.Count > 5 && row.Cells[5].Value != null) ? row.Cells[5].Value.ToString() : "";
                PAddressTb.Text = (row.Cells.Count > 6 && row.Cells[6].Value != null) ? row.Cells[6].Value.ToString() : "";

                if (row.Cells.Count > 0 && row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int parsed))
                    key = parsed;
                else
                    key = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar paciente: " + ex.Message);
            }
        }

        private void Reset()
        {
            PNameTb.Text = "";
            PAgeTb.Text = "";
            PphoneTb.Text = "";
            PAddressTb.Text = "";
            PGenCb.SelectedIndex = -1;
            PBGroupCb.SelectedIndex = -1;
            key = 0;
        }

        // Eliminar paciente (parametrizado)
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona el paciente a eliminar");
                return;
            }

            try
            {
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand("DELETE FROM PatientTbl WHERE PNum = @pnum", con))
                {
                    cmd.Parameters.AddWithValue("@pnum", key);
                    con.Open();
                    int affected = cmd.ExecuteNonQuery();

                    if (affected == 0)
                    {
                        MessageBox.Show("No se encontró el paciente para eliminar.");
                    }
                    else
                    {
                        MessageBox.Show("Paciente eliminado con éxito");
                        Reset();
                        populate();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al eliminar el paciente: " + Ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Pat = new Paciente();
            Pat.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Actualizar paciente (parametrizado y validado)
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PNameTb.Text) ||
                string.IsNullOrWhiteSpace(PphoneTb.Text) ||
                string.IsNullOrWhiteSpace(PAgeTb.Text) ||
                PGenCb.SelectedIndex == -1 ||
                PBGroupCb.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(PAddressTb.Text))
            {
                MessageBox.Show("Falta información");
                return;
            }

            if (key == 0)
            {
                MessageBox.Show("Selecciona el paciente a editar");
                return;
            }

            if (!int.TryParse(PAgeTb.Text, out int edad))
            {
                MessageBox.Show("Edad inválida");
                return;
            }

            try
            {
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand(
                    "UPDATE PatientTbl SET PName = @pname, PAge = @page, PPhone = @pphone, PGender = @pgender, PBGroup = @pbgroup, PAddress = @paddress WHERE PNum = @pnum", con))
                {
                    cmd.Parameters.AddWithValue("@pname", PNameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@page", edad);
                    cmd.Parameters.AddWithValue("@pphone", PphoneTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@pgender", PGenCb.SelectedItem != null ? PGenCb.SelectedItem.ToString() : PGenCb.Text);
                    cmd.Parameters.AddWithValue("@pbgroup", PBGroupCb.SelectedItem != null ? PBGroupCb.SelectedItem.ToString() : PBGroupCb.Text);
                    cmd.Parameters.AddWithValue("@paddress", PAddressTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@pnum", key);

                    con.Open();
                    int affected = cmd.ExecuteNonQuery();

                    if (affected == 0)
                    {
                        MessageBox.Show("No se encontró el paciente para actualizar.");
                    }
                    else
                    {
                        MessageBox.Show("Paciente editado con éxito");
                        Reset();
                        populate();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al actualizar el paciente: " + Ex.Message);
            }
        }
    }
}