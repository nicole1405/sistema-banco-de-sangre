using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBMS
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();


        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {
                // Configura la contraseña real en un lugar seguro (app.config, etc.)
            string adminPassword = "Password"; // Mover esto a configuración

            if (string.IsNullOrWhiteSpace(AdminPassTb.Text))
            {
                MessageBox.Show("Enter The Admin Password");
                return;
            }

            if (AdminPassTb.Text == adminPassword)
            {
                Employee Emp = new Employee();
                Emp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Password. Contact the System Admin");
                // Opcional: limpiar el campo de contraseña después de un intento fallido
                AdminPassTb.Text = "";
                AdminPassTb.Focus();
            }
        }

        private void AdminPassTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
