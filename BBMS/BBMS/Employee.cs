using System;
using System.Data;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Employee : UserControl
    {
        private readonly EmployeeService _service;
        private int key = 0;
        private const string PassPlaceholder = "********";
        private bool isInitializing = true;

        public Employee()
        {
            InitializeComponent();

            _service = new EmployeeService();

            EmpPassTb.PasswordChar = '*';
            EmpPassTb.UseSystemPasswordChar = false;

            EmployeeDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            EmployeeDGV.MultiSelect = false;
            EmployeeDGV.ReadOnly = true;

            EmployeeDGV.SelectionChanged -= EmployeeDGV_SelectionChanged;
            EmployeeDGV.SelectionChanged += EmployeeDGV_SelectionChanged;

            EmployeeDGV.DataBindingComplete -= EmployeeDGV_DataBindingComplete;
            EmployeeDGV.DataBindingComplete += EmployeeDGV_DataBindingComplete;

            populate();
            // Puedes llamar aquí a la lógica que estaba en Employee_Shown si lo necesitas
        }

        private void Reset()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            key = 0;
            label16.Text = "Contraseña";
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
        }

        private void AddEmpBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmpNameTb.Text) || string.IsNullOrWhiteSpace(EmpPassTb.Text) || EmpPassTb.Text == PassPlaceholder)
            {
                MessageBox.Show("El nombre y la contraseña no pueden estar vacíos.");
                return;
            }

            if (_service.AddEmployee(EmpNameTb.Text.Trim(), EmpPassTb.Text, out string error))
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

        private void guna2Button2_Click(object sender, EventArgs e) => AddEmpBtn_Click(sender, e);

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

            // Si el campo tiene el placeholder o está vacío, no cambiamos contraseña
            string newPass = (string.IsNullOrWhiteSpace(EmpPassTb.Text) || EmpPassTb.Text == PassPlaceholder) ? null : EmpPassTb.Text;

            if (_service.UpdateEmployee(key, EmpNameTb.Text.Trim(), newPass, out string error))
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

        // Pobla la grilla con columnas formales (Id, Nombre, Contraseña)
        private void populate()
        {
            isInitializing = true;
            try
            {
                var dt = _service.GetEmployees();
                EmployeeDGV.DataSource = dt;

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

        private void EmployeeDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
        }

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

        // Métodos vacíos para compatibilidad con diseñador
        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        // Adapta el método Employee_Shown a Employee_Load:
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