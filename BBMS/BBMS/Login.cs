using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using BBMS.Clases; // 2. AÑADIDO

namespace BBMS
{
    public partial class Login : Form
    {
        // 3. REMOVIDA: La variable 'SqlConnection Con'

        // 4. Instanciamos la nueva clase de lógica
        private cAutenticacion gestorAutenticacion = new cAutenticacion();

        public Login()
        {
            InitializeComponent();
        }

        // 5. Botón Iniciar sesión (¡Ahora refactorizado y SEGURO!)
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string user = EmpIdTdb.Text?.Trim();
            string pass = EmpPassTb.Text ?? "";

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Introduce usuario y contraseña.");
                return;
            }

            bool esValido = gestorAutenticacion.ValidarCredenciales(user, pass);

            if (esValido)
            {
                // Login OK
                Layout mainForm = new Layout();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        // 7. REMOVIDOS:
        // El método 'VerifyPassword' ya no está aquí.
        // El método 'FixedTimeEquals' ya no está aquí.


        // --- (Métodos de navegación y eventos vacíos) ---
        #region Navegacion y Eventos
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

        private void label2_Click(object sender, EventArgs e) { }
        private void Login_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        #endregion
    }
}