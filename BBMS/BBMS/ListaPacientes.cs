using System;
using System.Data;
using System.Windows.Forms;
using BBMS.Clases; // <-- 1. Importante: Importar la carpeta Clases

namespace BBMS
{
    public partial class ListaPacientes : UserControl
    {
        // 2. Instancia la clase de lógica de datos para pacientes.
        private cPacienteDatos gestorPacientes = new cPacienteDatos();
        int key = 0;

        // 3. Constructor: inicializa componentes y configura el DataGridView.
        public ListaPacientes()
        {
            InitializeComponent();
            ConfigurarDataGridView();

            // 4. Llena los datos de pacientes.
            populate();

            // 5. Renombra las columnas a nombres formales.
            ConfigurarColumnasDGV();
        }

        // 6. Configura el DataGridView para selección y solo lectura.
        private void ConfigurarDataGridView()
        {
            PatientsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PatientsDGV.MultiSelect = false;
            PatientsDGV.ReadOnly = true;
            PatientsDGV.SelectionChanged += PatientsDGV_SelectionChanged;
        }

        // 7. Renombra las columnas del DataGridView.
        private void ConfigurarColumnasDGV()
        {
            if (PatientsDGV.Columns.Contains("PNum"))
                PatientsDGV.Columns["PNum"].HeaderText = "ID Paciente";
            if (PatientsDGV.Columns.Contains("PName"))
                PatientsDGV.Columns["PName"].HeaderText = "Nombre Completo";
            if (PatientsDGV.Columns.Contains("PAge"))
                PatientsDGV.Columns["PAge"].HeaderText = "Edad";
            if (PatientsDGV.Columns.Contains("PPhone"))
                PatientsDGV.Columns["PPhone"].HeaderText = "Teléfono";
            if (PatientsDGV.Columns.Contains("PGender"))
                PatientsDGV.Columns["PGender"].HeaderText = "Género";
            if (PatientsDGV.Columns.Contains("PBGroup"))
                PatientsDGV.Columns["PBGroup"].HeaderText = "Grupo Sanguíneo";
            if (PatientsDGV.Columns.Contains("PAddress"))
                PatientsDGV.Columns["PAddress"].HeaderText = "Dirección";
        }

        // 8. Llena el DataGridView con los pacientes.
        private void populate()
        {
            try
            {
                PatientsDGV.DataSource = gestorPacientes.ObtenerPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de pacientes: " + ex.Message);
            }
        }

        // 9. Evento de carga del formulario (sin lógica).
        private void ListaPacientes_Load(object sender, EventArgs e)
        {
        }

        // 10. Maneja el click en la celda del DataGridView.
        private void PatientsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PopulateFieldsFromRowIndex(e.RowIndex);
        }

        // 11. Maneja el cambio de selección en el DataGridView.
        private void PatientsDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (PatientsDGV.CurrentRow == null) return;
            PopulateFieldsFromRowIndex(PatientsDGV.CurrentRow.Index);
        }

        // 12. Asigna los valores de la fila seleccionada a los campos del formulario.
        private void PopulateFieldsFromRowIndex(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= PatientsDGV.Rows.Count) return;
                var row = PatientsDGV.Rows[rowIndex];

                string GetCellValue(string columnName)
                {
                    if (PatientsDGV.Columns.Contains(columnName) && row.Cells[columnName].Value != null)
                        return row.Cells[columnName].Value.ToString();
                    return "";
                }

                PNameTb.Text = GetCellValue("PName");
                PAgeTb.Text = GetCellValue("PAge");
                PphoneTb.Text = GetCellValue("PPhone");
                PGenCb.Text = GetCellValue("PGender");
                PBGroupCb.Text = GetCellValue("PBGroup");
                PAddressTb.Text = GetCellValue("PAddress");

                if (int.TryParse(GetCellValue("PNum"), out int parsed))
                    key = parsed;
                else
                    key = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar paciente: " + ex.Message);
            }
        }

        // 13. Limpia los campos del formulario.
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

        // 14. Evento click del botón Eliminar (Guna2Button): elimina el paciente seleccionado.
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona el paciente a eliminar");
                return;
            }

            try
            {
                int affected = gestorPacientes.EliminarPaciente(key);

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
            catch (Exception Ex)
            {
                MessageBox.Show("Error al eliminar el paciente: " + Ex.Message);
            }
        }

        // 15. Evento click del botón Editar (Guna2Button): actualiza el paciente seleccionado.
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
                string nombre = PNameTb.Text.Trim();
                string telefono = PphoneTb.Text.Trim();
                string genero = PGenCb.SelectedItem != null ? PGenCb.SelectedItem.ToString() : PGenCb.Text;
                string grupo = PBGroupCb.SelectedItem != null ? PBGroupCb.SelectedItem.ToString() : PBGroupCb.Text;
                string direccion = PAddressTb.Text.Trim();

                int affected = gestorPacientes.ActualizarPaciente(key, nombre, edad, telefono, genero, grupo, direccion);

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
            catch (Exception Ex)
            {
                MessageBox.Show("Error al actualizar el paciente: " + Ex.Message);
            }
        }

        
    }
}