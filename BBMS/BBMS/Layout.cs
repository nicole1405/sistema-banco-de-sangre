using System;
using System.Windows.Forms;

namespace BBMS
{
    public partial class Layout : Form
    {
        
        public Layout()
        {
            InitializeComponent();
        }

       
        private void BtnDonar_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var donarControl = new Donar();
            donarControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(donarControl);

        }

        private void BtnDonante_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var donanteControl = new Donante();
            donanteControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(donanteControl);
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var inventarioControl = new InventarioDeSangre();
            inventarioControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(inventarioControl);
        }

        private void BtnPaciente_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var pacienteControl = new Paciente();
            pacienteControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(pacienteControl);
        }

        private void btnDonar_Click_1(object sender, EventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            mainPanel.Controls.Clear();
            var MainFormControl = new Mainform();
            MainFormControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(MainFormControl);
        }

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var empleadoControl = new Employee();
            empleadoControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(empleadoControl);
        }

        private void BtnListaPaciente_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var listaPacienteControl = new ListaPacientes();
            listaPacienteControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(listaPacienteControl);   
        }

        private void BtnMainPanel_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var mainControl = new PanelPrincipal();
            mainControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(mainControl);
        }

        private void BtnTransfucion_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var TransfusionDeSangreControl = new TransfusionDeSangre();
            TransfusionDeSangreControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(TransfusionDeSangreControl);
        }

        private void BtnVerDonantes_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            var VerDonantesControl = new Verdonantes();
            VerDonantesControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(VerDonantesControl);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            // CERRAR SESION 
            // 1. Crea una nueva instancia de la ventana de Login
            Login loginForm = new Login();

            // 2. Muestra la ventana de Login
            loginForm.Show();

            // 3. Oculta la ventana actual (el panel principal)
            this.Hide();
        }

        private void sidebarPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
