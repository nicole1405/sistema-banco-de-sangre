using System;
using System.Data;
using System.Windows.Forms;
using BBMS.Clases; // <-- 1. Importante: Importar la carpeta Clases

namespace BBMS
{
    public partial class ListaPacientes : UserControl
    {
        // 2. Instanciar la nueva clase de lógica de datos
        private cPacienteDatos gestorPacientes = new cPacienteDatos();
        int key = 0;

        public ListaPacientes()
        {
            InitializeComponent();
            ConfigurarDataGridView();

            // Llenamos los datos
            populate();

            // 3. Renombrar las columnas a nombres formales
            ConfigurarColumnasDGV();
        }

        // 4. Se elimina la variable 'SqlConnection Con' de aquí

        private void ConfigurarDataGridView()
        {
            PatientsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PatientsDGV.MultiSelect = false;
            PatientsDGV.ReadOnly = true;
            PatientsDGV.SelectionChanged += PatientsDGV_SelectionChanged;
        }

        private void ConfigurarColumnasDGV()
        {
            // Verificamos que las columnas existan antes de renombrar
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

        // 5. El método populate ahora es mucho más simple
        private void populate()
        {
            try
            {
                // Llama al gestor para obtener los datos y los asigna
                PatientsDGV.DataSource = gestorPacientes.ObtenerPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de pacientes: " + ex.Message);
            }
        }

        private void ListaPacientes_Load(object sender, EventArgs e)
        {
            // Puedes dejar esto vacío si no se usa
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

        // 6. Método helper actualizado para usar nombres de columna (más robusto)
        private void PopulateFieldsFromRowIndex(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= PatientsDGV.Rows.Count) return;
                var row = PatientsDGV.Rows[rowIndex];

                // Función helper para obtener valor de celda de forma segura
                string GetCellValue(string columnName)
                {
                    // ¡CORRECCIÓN AQUÍ!
                    // Comprobamos si la COLUMNA existe en el DataGridView, no en la celda.
                    if (PatientsDGV.Columns.Contains(columnName) && row.Cells[columnName].Value != null)
                    {
                        return row.Cells[columnName].Value.ToString();
                    }
                    return "";
                }

                // El resto de esta lógica ya estaba correcta
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

        // 7. Lógica de eliminación (Delete) refactorizada
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona el paciente a eliminar");
                return;
            }

            try
            {
                // Llama al gestor para eliminar
                int affected = gestorPacientes.EliminarPaciente(key);

                if (affected == 0)
                {
                    MessageBox.Show("No se encontró el paciente para eliminar.");
                }
                else
                {
                    MessageBox.Show("Paciente eliminado con éxito");
                    Reset();
                    populate(); // Recarga la lista
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al eliminar el paciente: " + Ex.Message);
            }
        }

        // 8. Lógica de actualización (Update) refactorizada
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // ... (Toda tu validación inicial sigue igual) ...
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
                // Prepara los datos
                string nombre = PNameTb.Text.Trim();
                string telefono = PphoneTb.Text.Trim();
                string genero = PGenCb.SelectedItem != null ? PGenCb.SelectedItem.ToString() : PGenCb.Text;
                string grupo = PBGroupCb.SelectedItem != null ? PBGroupCb.SelectedItem.ToString() : PBGroupCb.Text;
                string direccion = PAddressTb.Text.Trim();

                // Llama al gestor para actualizar
                int affected = gestorPacientes.ActualizarPaciente(key, nombre, edad, telefono, genero, grupo, direccion);

                if (affected == 0)
                {
                    MessageBox.Show("No se encontró el paciente para actualizar.");
                }
                else
                {
                    MessageBox.Show("Paciente editado con éxito");
                    Reset();
                    populate(); // Recarga la lista
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al actualizar el paciente: " + Ex.Message);
            }
        }

        // --- (TODOS TUS OTROS MÉTODOS DE NAVEGACIÓN 'label_Click' VAN AQUÍ) ---
        // --- (No cambian en absoluto) ---
        #region Navegacion
        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Pat = new Paciente();
            Pat.Show();
            this.Hide();
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

        private void label8_Click(object sender, EventArgs e)
        {
            // ...
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
        #endregion
    }
}