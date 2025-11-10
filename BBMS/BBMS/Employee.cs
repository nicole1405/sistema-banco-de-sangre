using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BBMS
{
    public partial class Employee : Form
    {
        // Cadena de conexión
        private readonly string connStr = @"Data Source=FIDEV;Initial Catalog=BancoDeSangre;Persist Security Info=True;User ID=sa;Password=Delta92_$1911;TrustServerCertificate=True";

        // Id seleccionado
        private int key = 0;

        // Placeholder visible para indicar que existe contraseña
        private const string PassPlaceholder = "********";

        // Bandera para evitar reacción a eventos durante la inicialización
        private bool isInitializing = true;

        public Employee()
        {
            InitializeComponent();

            // Forzar estilo del TextBox de contraseña
            EmpPassTb.PasswordChar = '*';
            EmpPassTb.UseSystemPasswordChar = false;

            // Configura selección de la grilla (antes de poblar)
            EmployeeDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            EmployeeDGV.MultiSelect = false;
            EmployeeDGV.ReadOnly = true;

            // Suscribe eventos
            EmployeeDGV.SelectionChanged -= EmployeeDGV_SelectionChanged;
            EmployeeDGV.SelectionChanged += EmployeeDGV_SelectionChanged;

            EmployeeDGV.DataBindingComplete -= EmployeeDGV_DataBindingComplete;
            EmployeeDGV.DataBindingComplete += EmployeeDGV_DataBindingComplete;

            this.Shown -= Employee_Shown;
            this.Shown += Employee_Shown;

            // Pobla la grilla (queda protegido por isInitializing)
            populate();
        }

        // Limpia campos
        private void Reset()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            key = 0;
            label16.Text = "Contraseña";
            // Limpiar selección en la grilla
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
        }

        // Añadir empleado (usa hash PBKDF2)
        private void AddEmpBtn_Click(object sender, EventArgs e)
        {
            // Considerar el placeholder como vacío para evitar guardar "********"
            if (string.IsNullOrWhiteSpace(EmpNameTb.Text) || string.IsNullOrWhiteSpace(EmpPassTb.Text) || EmpPassTb.Text == PassPlaceholder)
            {
                MessageBox.Show("El nombre y la contraseña no pueden estar vacíos.");
                return;
            }

            try
            {
                string hashedPassword = HashPassword(EmpPassTb.Text);

                using (var con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "INSERT INTO EmployeeTbl (EmpId, EmpPass) VALUES (@EmpId, @EmpPass)";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", EmpNameTb.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmpPass", hashedPassword);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Empleado guardado.");
                populate();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        // Compatibilidad con evento antiguo
        private void guna2Button2_Click(object sender, EventArgs e) => AddEmpBtn_Click(sender, e);

        // Editar empleado: cambia nombre y opcionalmente contraseña
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

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    con.Open();

                    bool wantsChangePass = !string.IsNullOrWhiteSpace(EmpPassTb.Text) && EmpPassTb.Text != PassPlaceholder;
                    string query;
                    if (wantsChangePass)
                    {
                        string hashedPassword = HashPassword(EmpPassTb.Text);
                        query = "UPDATE EmployeeTbl SET EmpId = @EmpId, EmpPass = @EmpPass WHERE EmpNum = @EmpKey";
                        using (var cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", EmpNameTb.Text.Trim());
                            cmd.Parameters.AddWithValue("@EmpPass", hashedPassword);
                            cmd.Parameters.AddWithValue("@EmpKey", key);
                            int affected = cmd.ExecuteNonQuery();
                            if (affected == 0) MessageBox.Show("No se encontró el empleado para actualizar.");
                            else
                            {
                                MessageBox.Show("Empleado actualizado.");
                                populate();
                                Reset();
                            }
                        }
                    }
                    else
                    {
                        query = "UPDATE EmployeeTbl SET EmpId = @EmpId WHERE EmpNum = @EmpKey";
                        using (var cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", EmpNameTb.Text.Trim());
                            cmd.Parameters.AddWithValue("@EmpKey", key);
                            int affected = cmd.ExecuteNonQuery();
                            if (affected == 0) MessageBox.Show("No se encontró el empleado para actualizar.");
                            else
                            {
                                MessageBox.Show("Empleado actualizado.");
                                populate();
                                Reset();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message);
            }
        }

        // Eliminar empleado
        private void DeleteEmpBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona un empleado.");
                return;
            }

            var ok = MessageBox.Show("Eliminar empleado seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ok != DialogResult.Yes) return;

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "DELETE FROM EmployeeTbl WHERE EmpNum = @EmpKey";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpKey", key);
                        int affected = cmd.ExecuteNonQuery();
                        if (affected == 0) MessageBox.Show("No se encontró el empleado para eliminar.");
                        else
                        {
                            MessageBox.Show("Empleado eliminado.");
                            populate();
                            Reset();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        // Carga empleados: muestra solo el nombre en la tabla (oculta EmpNum)
        // Carga empleados: muestra solo el nombre en la tabla (oculta EmpNum)
        private void populate()
        {
            // Marcar inicializando para ignorar eventos mientras bindea
            isInitializing = true;
            try
            {
                if (string.IsNullOrEmpty(connStr))
                {
                    MessageBox.Show("Cadena de conexión no configurada.");
                    return;
                }

                using (var con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "SELECT EmpNum, EmpId FROM EmployeeTbl ORDER BY EmpNum";
                    using (var sda = new SqlDataAdapter(query, con))
                    {
                        var ds = new DataSet();
                        sda.Fill(ds);
                        EmployeeDGV.DataSource = ds.Tables[0];
                    }
                }

                // Oculta la columna de clave para que solo se vea el nombre
                if (EmployeeDGV.Columns.Contains("EmpNum"))
                    EmployeeDGV.Columns["EmpNum"].Visible = false;
                else if (EmployeeDGV.Columns.Count > 0)
                    EmployeeDGV.Columns[0].Visible = false;

                // Borra selección inmediata tras bind
                EmployeeDGV.ClearSelection();
                EmployeeDGV.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
            finally
            {
                // Importante: permitir que SelectionChanged vuelva a actuar tras terminar el bind
                isInitializing = false;
            }
        }

        // DataBindingComplete: asegurar que no quede fila seleccionada justo después del binding
        private void EmployeeDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;
        }

        // Evento Shown: finaliza inicialización y permite que SelectionChanged actúe
        private void Employee_Shown(object sender, EventArgs e)
        {
            // Un doble clear para asegurarnos
            EmployeeDGV.ClearSelection();
            EmployeeDGV.CurrentCell = null;

            // Permitir que cambios futuros afecten la UI
            isInitializing = false;
        }

        // Evento selección fila
        private void EmployeeDGV_SelectionChanged(object sender, EventArgs e)
        {
            // Ignora cambios durante la inicialización
            if (isInitializing) return;

            try
            {
                if (EmployeeDGV.CurrentRow == null || EmployeeDGV.CurrentRow.Index == -1)
                {
                    Reset();
                    return;
                }

                var row = EmployeeDGV.CurrentRow;

                // EmpId en la columna 1 (nombre)
                EmpNameTb.Text = row.Cells.Count > 1 && row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";

                // EmpNum en columna 0 (id)
                if (row.Cells.Count > 0 && row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int parsed))
                    key = parsed;
                else
                    key = 0;

                // No mostrar hash en el textbox: dejar el textbox vacío y mostrar indicación si quieres
                EmpPassTb.Text = PassPlaceholder;
                label16.Text = "Contraseña (dejar para no cambiar)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar: " + ex.Message);
            }
        }

        // Hash PBKDF2 -> "iter.salt.hash"
        private string HashPassword(string password)
        {
            const int saltSize = 16;
            const int hashSize = 32;
            const int iterations = 100000;

            byte[] salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                hash = pbkdf2.GetBytes(hashSize);

            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        // Verifica password con el hash almacenado
        private bool VerifyPassword(string password, string stored)
        {
            try
            {
                var parts = stored.Split('.');
                if (parts.Length != 3) return false;

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] hash = Convert.FromBase64String(parts[2]);

                byte[] testHash;
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                    testHash = pbkdf2.GetBytes(hash.Length);

                return FixedTimeEquals(hash, testHash);
            }
            catch
            {
                return false;
            }
        }

        // Comparación de tiempo fijo
        private bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        // Métodos vacíos para compatibilidad con diseñador
        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Employee_Load(object sender, EventArgs e) { }
        private void EmpNameTb_TextChanged(object sender, EventArgs e) { }
        private void EmpNameTb_TextChanged_1(object sender, EventArgs e) { }
        private void EmpPassTb_TextChanged(object sender, EventArgs e) { }
    }
}