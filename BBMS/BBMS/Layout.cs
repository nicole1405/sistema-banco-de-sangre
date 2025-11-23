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

            // Reordenar si cambia tamaño del sidebar (mantener sin huecos)
            if (sidebarPanel != null)
                sidebarPanel.SizeChanged += (s, e) => ReorderSidebarButtons();
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
            SetControlState("btnInventario", isAdmin || isDoctor);

            // Registrar donación -> Admin, Doctor, Enfermera
            SetControlState("BtnDonar", isAdmin || isDoctor || isNurse);

            // Gestión de donantes -> Admin, Doctor, Enfermera
            SetControlState("btnDonante", isAdmin || isDoctor || isNurse);

            // Pacientes -> Admin, Doctor, Enfermera
            SetControlState("btnPaciente", isAdmin || isDoctor || isNurse);

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
                SetControlState("btnInventario", false);
                SetControlState("BtnDonar", false);
                SetControlState("btnDonante", false);
                SetControlState("btnPaciente", false);
                SetControlState("BtnListaPaciente", false);
                SetControlState("BtnTransfucion", false);
                SetControlState("BtnVerDonantes", false);
                SetControlState("BtnMainPanel", false);
                SetControlState("LogoutBtn", true);
            }

            // Reordenar los controles visibles para evitar huecos
            ReorderSidebarButtons();
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
        /// Reordena los botones del sidebar eliminando huecos dejados por controles ocultos.
        /// Mantiene LogoutBtn en la parte inferior del panel.
        /// </summary>
        private void ReorderSidebarButtons()
        {
            try
            {
                if (sidebarPanel == null) return;

                // Orden deseado (ajusta nombres si en el diseñador cambian)
                string[] order = new[]
                {
                    "BtnDonar",     // botón Donar
                    "btnDonante",   // botón Donante (nombre en diseñador)
                    "btnInventario",
                    "btnPaciente",
                    "BtnEmployee",
                    "BtnListaPaciente",
                    "BtnMainPanel",
                    "BtnTransfucion",
                    "BtnVerDonantes"
                };

                int topMargin = 12;
                int spacing = 10;
                int xDefault = 25;
                int y = topMargin;

                // Mover controles visibles en el orden indicado
                foreach (var name in order)
                {
                    var matches = sidebarPanel.Controls.Find(name, true);
                    if (matches.Length == 0) continue;
                    var c = matches[0];
                    if (!c.Visible) continue;

                    // Mantener X si ya está alineado, sino aplicar xDefault
                    int x = c.Location.X > 0 ? c.Location.X : xDefault;
                    c.Location = new System.Drawing.Point(x, y);
                    y += c.Height + spacing;
                }

                // Posicionar LogoutBtn en la parte inferior con margen
                var logoutMatches = sidebarPanel.Controls.Find("LogoutBtn", true);
                if (logoutMatches.Length > 0)
                {
                    var logout = logoutMatches[0];
                    int bottomMargin = 20;
                    int logoutX = logout.Location.X > 0 ? logout.Location.X : xDefault;
                    int logoutY = Math.Max(y + 10, sidebarPanel.Height - logout.Height - bottomMargin);
                    logout.Location = new System.Drawing.Point(logoutX, logoutY);
                }
            }
            catch
            {
                // No lanzar excepciones por UI; si necesitas logging, añade aquí.
            }
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

        private void sidebarPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}