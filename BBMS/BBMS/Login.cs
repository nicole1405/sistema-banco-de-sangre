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
        // 2. Instancia el gestor de autenticación.
        private cAutenticacion gestorAutenticacion = new cAutenticacion();

        // 3. Constructor: inicializa el formulario y los controles Guna.
        public Login()
        {
            InitializeComponent();
        }

        // 4. Evento click del botón de login (Guna2Button).
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string user = EmpIdTdb.Text?.Trim(); // 5. Obtiene el usuario del textbox Guna.
            string pass = EmpPassTb.Text ?? "";  // 6. Obtiene la contraseña del textbox Guna.

            // 7. Validación de campos vacíos.
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Introduce usuario y contraseña.");
                return;
            }

            // 8. Llama al gestor de autenticación para validar credenciales.
            bool esValido = gestorAutenticacion.ValidarCredenciales(user, pass);

            if (esValido)
            {
                // 9. Si es válido, navega al formulario principal.
                Layout mainForm = new Layout();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                // 10. Si no es válido, muestra mensaje de error.
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        // 11. Métodos de navegación y eventos vacíos.
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