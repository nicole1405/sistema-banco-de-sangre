using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BBMS
{
    public partial class Login : Form
    {
        SqlConnection Con = new SqlConnection(@"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Botón Iniciar sesión (comparación directa, sin encriptación)
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string user = EmpIdTdb.Text?.Trim();
            string pass = EmpPassTb.Text ?? "";

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Introduce usuario y contraseña.");
                return;
            }

            try
            {
                string storedPass = null;

                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand("SELECT EmpPass FROM EmployeeTbl WHERE EmpId = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", user);
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        storedPass = result.ToString();
                }

                if (string.IsNullOrEmpty(storedPass))
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                    return;
                }

                // Comparación directa (sin hash)
                if (storedPass == pass)
                {
                    // Intentar abrir MainForm si existe; si no, abrir PanelPrincipal
                    Type mainType = Type.GetType("BBMS.MainForm") ?? Type.GetType("BBMS.Mainform") ?? Type.GetType("BBMS.MainForm, " + typeof(Login).Assembly.FullName);
                    if (mainType != null && typeof(Form).IsAssignableFrom(mainType))
                    {
                        var main = (Form)Activator.CreateInstance(mainType);
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Fallback a PanelPrincipal (existe en el proyecto)
                        var panel = new PanelPrincipal();
                        panel.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error de base de datos: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Adm = new AdminLogin();
            Adm.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Métodos de verificación existentes quedan, pero no son usados ahora.
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

        private bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}