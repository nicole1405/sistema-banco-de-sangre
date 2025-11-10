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

            // Validación de UI (sigue igual)
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Introduce usuario y contraseña.");
                return;
            }

            try
            {
                // 6. Lógica de autenticación movida al gestor
                // Esta llamada ahora SÍ usa la verificación de hash.
                bool esValido = gestorAutenticacion.ValidarCredenciales(user, pass);

                if (esValido)
                {
                    // Lógica de navegación (sigue igual)
                    Type mainType = Type.GetType("BBMS.MainForm") ?? Type.GetType("BBMS.Mainform") ?? Type.GetType("BBMS.MainForm, " + typeof(Login).Assembly.FullName);
                    if (mainType != null && typeof(Form).IsAssignableFrom(mainType))
                    {
                        var main = (Form)Activator.CreateInstance(mainType);
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
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
            catch (Exception ex)
            {
                // Captura errores de UI (ej. al crear el formulario)
                MessageBox.Show("Error al iniciar sesión: " + ex.Message);
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