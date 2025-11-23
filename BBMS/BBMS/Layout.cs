using System;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Layout : Form
    {
        public Layout()
        {
            InitializeComponent();

            // Aplicar permisos en cuanto se construye el formulario
            ApplyRolePermissions();
        }

        /// <summary>
        /// Aplica las reglas de visibilidad/actividad de controles según el rol del usuario.
        /// Rol esperado: "Administrador", "Doctor", "Enfermera" (no sensible a mayúsculas).
        /// </summary>
        private void ApplyRolePermissions()
        {
            var session = UserSession.Current;
            string role = session?.Role ?? string.Empty;

            bool isAdmin = role.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
            bool isDoctor = role.Equals("Doctor", StringComparison.OrdinalIgnoreCase);
            bool isNurse = role.Equals("Enfermera", StringComparison.OrdinalIgnoreCase) ||
                           role.Equals("Enfermero", StringComparison.OrdinalIgnoreCase);

            // Reglas:
            // - Administrador: todo visible/habilitado.
            // - Doctor: inventario + acciones clínicas (no gestión de empleados ni PanelPrincipal).
            // - Enfermera: acciones clínicas y donantes/transfusiones (no inventario, no PanelPrincipal).
            // - Usuario no autenticado/rol desconocido: acceso mínimo.

            // Gestión de empleados -> solo Administrador
            SetControlState("BtnEmployee", isAdmin);

            // Inventario -> Administrador y Doctor
            SetControlState("BtnInventario", isAdmin || isDoctor);

            // Registrar donación -> Admin, Doctor, Enfermera
            SetControlState("BtnDonar", isAdmin || isDoctor || isNurse);

            // Gestión de donantes -> Admin, Doctor, Enfermera
            SetControlState("BtnDonante", isAdmin || isDoctor || isNurse);

            // Pacientes -> Admin, Doctor, Enfermera
            SetControlState("BtnPaciente", isAdmin || isDoctor || isNurse);

            // Lista de pacientes -> Admin, Doctor, Enfermera
            SetControlState("BtnListaPaciente", isAdmin || isDoctor || isNurse);

            // Transfusiones -> Admin, Doctor, Enfermera
            SetControlState("BtnTransfucion", isAdmin || isDoctor || isNurse);

            // Ver donantes -> permitido para los roles clínicos y admin
            SetControlState("BtnVerDonantes", isAdmin || isDoctor || isNurse);

            // Panel Principal -> SOLO Administrador
            SetControlState("BtnMainPanel", isAdmin);

            // Logout siempre accesible
            SetControlState("LogoutBtn", true);

            // Si no hay sesión, restringimos todavía más
            if (session == null)
            {
                // Opcional: dejar Logout visible para permitir volver a login
                SetControlState("BtnEmployee", false);
                SetControlState("BtnInventario", false);
                SetControlState("BtnDonar", false);
                SetControlState("BtnDonante", false);
                SetControlState("BtnPaciente", false);
                SetControlState("BtnListaPaciente", false);
                SetControlState("BtnTransfucion", false);
                SetControlState("BtnVerDonantes", false);
                SetControlState("BtnMainPanel", false);
            }
        }

        /// <summary>
        /// Busca un control por su Name (recursivamente) y establece Visible/Enabled.
        /// </summary>
        private void SetControlState(string controlName, bool visible, bool enabled = true)
        {
            var ctrl = FindControlRecursive(this, controlName);
            if (ctrl != null)
            {
                ctrl.Visible = visible;
                ctrl.Enabled = enabled && visible;
            }
        }

        /// <summary>
        /// Busca un control por nombre en la jerarquía de controles (recursivo).
        /// </summary>
        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent == null) return null;
            if (string.Equals(parent.Name, name, StringComparison.OrdinalIgnoreCase))
                return parent;

            foreach (Control child in parent.Controls)
            {
                var found = FindControlRecursive(child, name);
                if (found != null) return found;
            }
            return null;
        }

        /// <summary>
        /// Comprueba si el usuario actual tiene el rol indicado.
        /// </summary>
        private bool IsInRole(string roleName)
        {
            var r = UserSession.Current?.Role;
            return !string.IsNullOrEmpty(r) && r.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        private void BtnDonar_Click(object sender, EventArgs e)
        {
            // Seguridad adicional: comprobar permiso antes de cargar
            if (!(IsInRole("Administrador") || IsInRole("Doctor") || IsInRole("Enfermera")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var donarControl = new Donar();
            donarControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(donarControl);
        }

        private void BtnDonante_Click(object sender, EventArgs e)
        {
            if (!(IsInRole("Administrador") || IsInRole("Doctor") || IsInRole("Enfermera")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var donanteControl = new Donante();
            donanteControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(donanteControl);
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            if (!(IsInRole("Administrador") || IsInRole("Doctor")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var inventarioControl = new InventarioDeSangre();
            inventarioControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(inventarioControl);
        }

        private void BtnPaciente_Click(object sender, EventArgs e)
        {
            if (!(IsInRole("Administrador") || IsInRole("Doctor") || IsInRole("Enfermera")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            // Evitar que el PanelPrincipal (o dashboard admin) se muestre por defecto a roles no admins.
            // El control que se carga aquí es 'Mainform' en tu código; lo dejamos siempre visible
            // pero si quieres que el dashboard sea exclusivo del admin no debes cargar PanelPrincipal aquí.
            // Mantengo la carga del Mainform por defecto (imagen principal).
            if (mainPanel.Controls.Count == 0)
            {
                var MainFormControl = new Mainform();
                MainFormControl.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(MainFormControl);
            }
        }

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            if (!IsInRole("Administrador"))
            {
                MessageBox.Show("Acceso denegado. Solo administradores pueden gestionar empleados.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var empleadoControl = new Employee();
            empleadoControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(empleadoControl);
        }

        private void BtnListaPaciente_Click(object sender, EventArgs e)
        {
            if (!(IsInRole("Administrador") || IsInRole("Doctor") || IsInRole("Enfermera")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var listaPacienteControl = new ListaPacientes();
            listaPacienteControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(listaPacienteControl);
        }

        private void BtnMainPanel_Click(object sender, EventArgs e)
        {
            // PanelPrincipal: SOLO Administrador puede abrirlo
            if (!IsInRole("Administrador"))
            {
                MessageBox.Show("Acceso denegado. Panel principal solo disponible para administradores.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var mainControl = new PanelPrincipal();
            mainControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(mainControl);
        }

        private void BtnTransfucion_Click(object sender, EventArgs e)
        {
            if (!(IsInRole("Administrador") || IsInRole("Doctor") || IsInRole("Enfermera")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainPanel.Controls.Clear();
            var TransfusionDeSangreControl = new TransfusionDeSangre();
            TransfusionDeSangreControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(TransfusionDeSangreControl);
        }

        private void BtnVerDonantes_Click(object sender, EventArgs e)
        {
            // Visible para roles clínicos; si quieres que sea público, quita la comprobación
            if (!(IsInRole("Administrador") || IsInRole("Doctor") || IsInRole("Enfermera")))
            {
                MessageBox.Show("Acceso denegado.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            // Limpiar sesión actual
            UserSession.Current = null;

            // 1. Crea una nueva instancia de la ventana de Login
            Login loginForm = new Login();

            // 2. Muestra la ventana de Login
            loginForm.Show();

            // 3. Oculta la ventana actual (el panel principal)
            this.Hide();
        }
    }
}
