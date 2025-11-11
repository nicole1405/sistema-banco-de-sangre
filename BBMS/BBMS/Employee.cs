using System;
using System.Data;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Employee : UserControl
    {
        // 1. Servicio para operaciones con empleados.
        private readonly EmployeeService _service;
        // 2. Clave primaria del empleado seleccionado.
        private int key = 0;
        // 3. Placeholder para el campo de contraseña.
        private const string PassPlaceholder = "********";
        // 4. Bandera para evitar eventos durante la inicialización.
        private bool isInitializing = true;

        // 5. Constructor: inicializa componentes y configura controles Guna.
        public Employee()
        {
            InitializeComponent();

            _service = new EmployeeService();

            // 6. Configura el campo de contraseña para ocultar el texto.
            EmpPassTb.PasswordChar = '*';
            EmpPassTb.UseSystemPasswordChar = false;

            // 7. Configura el DataGridView para selección y solo lectura.
            EmployeeDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            EmployeeDGV.MultiSelect = false;
            EmployeeDGV.ReadOnly = true;

            // 8. Suscribe eventos de selección y actualización de datos.
            EmployeeDGV.SelectionChanged -= EmployeeDGV_SelectionChanged;
            EmployeeDGV.SelectionChanged += EmployeeDGV_SelectionChanged;

            EmployeeDGV.DataBindingComplete -= EmployeeDGV_DataBindingComplete;
            EmployeeDGV.DataBindingComplete += EmployeeDGV_DataBindingComplete;

            // 9. Carga los empleados al iniciar.
            populate();
            // Puedes llamar aquí a la lógica que estaba en Employee_Shown si lo necesitas
        }

        // 10. Resetea los campos del formulario.
        private void Reset()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            key = 0;
            label16.Text = "Contraseña";
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
        }

        // 11. Evento para agregar un nuevo empleado.
        private void AddEmpBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmpNameTb.Text) || string.IsNullOrWhiteSpace(EmpPassTb.Text) || EmpPassTb.Text == PassPlaceholder)
            {
                MessageBox.Show("El nombre y la contraseña no pueden estar vacíos.");
                return;
            }
            // 12. Hashea la contraseña antes de guardar.
            var hash = UserAuthService.HashPassword(EmpPassTb.Text);
            if (_service.AddEmployee(EmpNameTb.Text.Trim(), hash, out string error))
            {
                MessageBox.Show("Empleado guardado.");
                populate();
                Reset();
            }
            else
            {
                MessageBox.Show("Error al guardar: " + error);
            }
        }

        // 13. Evento alternativo para agregar empleado (por compatibilidad con Guna2Button).
        private void guna2Button2_Click(object sender, EventArgs e) => AddEmpBtn_Click(sender, e);

        // 14. Evento para editar empleado.
        private void EditEmpBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona un empleado.");
                return;
            }
            if (string.IsNullOrWhiteSpace(EmpNameTb.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return;
            }

            string newHash = null;
            if (!string.IsNullOrWhiteSpace(EmpPassTb.Text) && EmpPassTb.Text != PassPlaceholder)
            {
                newHash = UserAuthService.HashPassword(EmpPassTb.Text);
            }

            if (_service.UpdateEmployee(key, EmpNameTb.Text.Trim(), newHash, out string error))
            {
                MessageBox.Show("Empleado actualizado.");
                populate();
                Reset();
            }
            else
            {
                MessageBox.Show("Error al editar: " + error);
            }
        }

        // 15. Evento para eliminar empleado.
        private void DeleteEmpBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona un empleado.");
                return;
            }

            var ok = MessageBox.Show("Eliminar empleado seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ok != DialogResult.Yes) return;

            if (_service.DeleteEmployee(key, out string error))
            {
                MessageBox.Show("Empleado eliminado.");
                populate();
                Reset();
            }
            else
            {
                MessageBox.Show("Error al eliminar: " + error);
            }
        }

        // 16. Carga los empleados en el DataGridView.
        private void populate()
        {
            isInitializing = true;
            try
            {
                var dt = _service.GetEmployees();
                EmployeeDGV.DataSource = dt;

                // 17. Oculta la columna Id y ajusta cabeceras.
                if (EmployeeDGV.Columns.Contains("Id"))
                    EmployeeDGV.Columns["Id"].Visible = false;
                else if (EmployeeDGV.Columns.Count > 0)
                    EmployeeDGV.Columns[0].Visible = false;

                if (EmployeeDGV.Columns.Contains("Nombre"))
                    EmployeeDGV.Columns["Nombre"].HeaderText = "Nombre";
                if (EmployeeDGV.Columns.Contains("Contraseña"))
                    EmployeeDGV.Columns["Contraseña"].HeaderText = "Contraseña";

                EmployeeDGV.ClearSelection();
                EmployeeDGV.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
            finally
            {
                isInitializing = false;
            }
        }

        // 18. Evento al completar el binding de datos en el DataGridView.
        private void EmployeeDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
        }

        // 19. Evento al cambiar la selección en el DataGridView.
        private void EmployeeDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            try
            {
                if (EmployeeDGV.CurrentRow == null || EmployeeDGV.CurrentRow.Index == -1)
                {
                    Reset();
                    return;
                }

                var row = EmployeeDGV.CurrentRow;

                EmpNameTb.Text = EmployeeDGV.Columns["Nombre"] != null && row.Cells["Nombre"].Value != null
                    ? row.Cells["Nombre"].Value.ToString()
                    : "";

                // Id en columna oculta
                if (EmployeeDGV.Columns["Id"] != null && row.Cells["Id"].Value != null && int.TryParse(row.Cells["Id"].Value.ToString(), out int parsed))
                    key = parsed;
                else
                    key = 0;

                EmpPassTb.Text = PassPlaceholder;
                label16.Text = "Contraseña (dejar para no cambiar)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar: " + ex.Message);
            }
        }

        // 20. Métodos vacíos para compatibilidad con diseñador.
        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        // 21. Evento de carga del control.
        private void Employee_Load(object sender, EventArgs e)
        {
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
            isInitializing = false;
        }
        private void EmpNameTb_TextChanged(object sender, EventArgs e) { }
        private void EmpNameTb_TextChanged_1(object sender, EventArgs e) { }
        private void EmpPassTb_TextChanged(object sender, EventArgs e) { }
    }
}