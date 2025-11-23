using BBMS.Clases;
using System;
using System.Data;
using System.Web.Security;
using System.Windows.Forms;

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
        }

        private void Reset()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            key = 0;
            label16.Text = "Contraseña";
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;

            // Reset role selection
            if (RoleCb != null && RoleCb.Items.Count > 0)
                RoleCb.SelectedIndex = -1;
        }

        private void AddEmpBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmpNameTb.Text) || string.IsNullOrWhiteSpace(EmpPassTb.Text) || EmpPassTb.Text == PassPlaceholder)
            {
                MessageBox.Show("El nombre y la contraseña no pueden estar vacíos.");
                return;
            }

            // Obtener rol seleccionado
            string selectedRole = RoleCb?.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(selectedRole))
            {
                MessageBox.Show("Selecciona un rol para el empleado.");
                return;
            }

            var hash = UserAuthService.HashPassword(EmpPassTb.Text);
            if (_service.AddEmployee(EmpNameTb.Text.Trim(), hash, selectedRole, out int newEmpId, out string error))
            {
                MessageBox.Show("Empleado guardado y rol asignado.");
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

        // Pobla la grilla con columnas formales (Id, Nombre, Contraseña, Rol)
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
                if (EmployeeDGV.Columns.Contains("Rol"))
                    EmployeeDGV.Columns["Rol"].HeaderText = "Rol";

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

                // Mostrar rol en el ComboBox si existe
                string roleValue = EmployeeDGV.Columns["Rol"] != null && row.Cells["Rol"].Value != null
                    ? row.Cells["Rol"].Value.ToString()
                    : string.Empty;

                if (!string.IsNullOrEmpty(roleValue) && RoleCb != null)
                {
                    // Intenta seleccionar por texto exacto (no sensible a mayúsculas)
                    int idx = -1;
                    for (int i = 0; i < RoleCb.Items.Count; i++)
                    {
                        if (string.Equals(RoleCb.Items[i].ToString(), roleValue, StringComparison.OrdinalIgnoreCase))
                        {
                            idx = i;
                            break;
                        }
                    }
                    RoleCb.SelectedIndex = idx;
                }
                else if (RoleCb != null)
                {
                    RoleCb.SelectedIndex = -1;
                }

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