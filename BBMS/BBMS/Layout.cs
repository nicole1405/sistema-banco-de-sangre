using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Layout : Form
    {
        private Button notifButton;
        private Panel notifPanel;
        private ListBox notifListBox;
        private Timer notifTimer;

        public Layout()
        {
            InitializeComponent();

            // Inicializar UI y permisos
            ApplyRolePermissions();

            // Inicializar notificaciones UI dinámicamente
            InitializeNotificationsUI();

            // Iniciar timer para actualizar contador periódicamente
            StartNotificationTimer();
        }

        private void InitializeNotificationsUI()
        {
            // Botón de notificaciones (esquina superior derecha)
            notifButton = new Button
            {
                Text = "🔔",
                AutoSize = false,
                Size = new Size(40, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 50, 8),
                BackColor = Color.LightSteelBlue,
                FlatStyle = FlatStyle.Flat
            };
            notifButton.Click += NotifButton_Click;
            this.Controls.Add(notifButton);
            notifButton.BringToFront();
            this.Resize += (s, e) => notifButton.Location = new Point(this.ClientSize.Width - 50, 8);

            // Panel desplegable para listar notificaciones
            notifPanel = new Panel
            {
                Size = new Size(320, 200),
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            notifPanel.Location = new Point(this.ClientSize.Width - notifPanel.Width - 10, notifButton.Bottom + 4);
            this.Controls.Add(notifPanel);
            notifPanel.BringToFront();
            this.Resize += (s, e) => notifPanel.Location = new Point(this.ClientSize.Width - notifPanel.Width - 10, notifButton.Bottom + 4);

            // ListBox para mostrar mensajes
            notifListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            notifPanel.Controls.Add(notifListBox);

            // Cerrar panel al hacer click fuera (simple)
            this.Click += (s, e) =>
            {
                if (notifPanel.Visible && !notifPanel.Bounds.Contains(PointToClient(Cursor.Position)) && !notifButton.Bounds.Contains(PointToClient(Cursor.Position)))
                {
                    notifPanel.Visible = false;
                }
            };

            // Inicial actualizar contador
            UpdateNotificationBadge();
        }

        private void StartNotificationTimer()
        {
            notifTimer = new Timer();
            notifTimer.Interval = 15000; // 15s
            notifTimer.Tick += (s, e) => UpdateNotificationBadge();
            notifTimer.Start();
        }

        private void NotifButton_Click(object sender, EventArgs e)
        {
            var session = GetCurrentUserId();
            if (session == null)
            {
                MessageBox.Show("Inicia sesión para ver notificaciones.");
                return;
            }

            var dt = NotificationService.GetUnreadForUser(session.Value);

            notifListBox.Items.Clear();

            // detectar columnas posibles
            string idCol = null;
            string tipoCol = null;
            string msgCol = null;
            string createdCol = null;
            foreach (DataColumn c in dt.Columns)
            {
                var name = c.ColumnName.ToLowerInvariant();
                if (idCol == null && (name.Contains("notificationid") || name.Contains("notificacionid") || name == "id")) idCol = c.ColumnName;
                if (tipoCol == null && (name.Contains("tiponotificacion") || name.Contains("tipo") || name.Contains("type"))) tipoCol = c.ColumnName;
                if (msgCol == null && (name.Contains("mensaje") || name.Contains("message") || name.Contains("descripcion"))) msgCol = c.ColumnName;
                if (createdCol == null && (name.Contains("created") || name.Contains("fecha") || name.Contains("createdat") || name.Contains("creado"))) createdCol = c.ColumnName;
            }

            foreach (DataRow row in dt.Rows)
            {
                int nid = 0;
                if (idCol != null && row[idCol] != DBNull.Value) int.TryParse(Convert.ToString(row[idCol]), out nid);
                var tipo = tipoCol != null && row[tipoCol] != DBNull.Value ? row[tipoCol].ToString() : "Notificación";
                var msg = msgCol != null && row[msgCol] != DBNull.Value ? row[msgCol].ToString() : "";
                var created = createdCol != null && row[createdCol] != DBNull.Value ? Convert.ToDateTime(row[createdCol]).ToString("g") : "";

                var display = string.IsNullOrEmpty(created) ? $"{tipo}: {msg}" : $"[{created}] {tipo}: {msg}";
                var item = new ListItem { Id = nid, Text = display };
                notifListBox.Items.Add(item);
            }

            notifPanel.Visible = true;

            // Marcar todas como leídas (ya hecho en servicio)
            try { NotificationService.MarkAllAsReadForUser(session.Value); } catch { }
            UpdateNotificationBadge();
        }
        private int? GetCurrentUserId()
        {
            var session = UserSession.Current;
            return session?.EmpId;
        }

        private void UpdateNotificationBadge()
        {
            var session = UserSession.Current;
            if (session == null)
            {
                notifButton.Text = "🔔";
                return;
            }

            int count = NotificationService.CountUnreadForUser(session.EmpId);
            notifButton.Text = count > 0 ? $"🔔 ({count})" : "🔔";
        }

        // Clase auxiliar para ListBox (almacena id y texto)
        private class ListItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }

        // -------------------------------
        // Resto del Layout: permisos, handlers y navegación (se mantiene igual)
        // -------------------------------

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

            SetControlState("BtnEmployee", isAdmin);
            SetControlState("btnInventario", isAdmin || isDoctor);
            SetControlState("BtnDonar", isAdmin || isDoctor || isNurse);
            SetControlState("btnDonante", isAdmin || isDoctor || isNurse);
            SetControlState("btnPaciente", isAdmin || isDoctor || isNurse);
            SetControlState("BtnListaPaciente", isAdmin || isDoctor || isNurse);
            SetControlState("BtnTransfucion", isAdmin || isDoctor || isNurse);
            SetControlState("BtnVerDonantes", isAdmin || isDoctor || isNurse);
            SetControlState("BtnMainPanel", isAdmin);
            SetControlState("LogoutBtn", true);

            if (session == null)
            {
                SetControlState("BtnEmployee", false);
                SetControlState("btnInventario", false);
                SetControlState("BtnDonar", false);
                SetControlState("btnDonante", false);
                SetControlState("btnPaciente", false);
                SetControlState("BtnListaPaciente", false);
                SetControlState("BtnTransfucion", false);
                SetControlState("BtnVerDonantes", false);
                SetControlState("BtnMainPanel", false);
            }

            // Reorder sidebar if you have that logic (kept elsewhere)
            ReorderSidebarButtonsSafe();
        }

        private void ReorderSidebarButtonsSafe()
        {
            try
            {
                // If you already have a FlowLayoutPanel (flowSidebar) this is not needed.
                // Kept as safe-noop to avoid removing previous logic.
            }
            catch { }
        }

        private void SetControlState(string controlName, bool visible, bool enabled = true)
        {
            var ctrl = FindControlRecursive(this, controlName);
            if (ctrl != null)
            {
                ctrl.Visible = visible;
                ctrl.Enabled = enabled && visible;
            }
        }

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

        private bool IsInRole(string roleName)
        {
            var r = UserSession.Current?.Role;
            return !string.IsNullOrEmpty(r) && r.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        // --- Handlers de navegación (idénticos a los tuyos)
        private void BtnDonar_Click(object sender, EventArgs e)
        {
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

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
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

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            UserSession.Current = null;
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void sidebarPanel_Paint(object sender, PaintEventArgs e) { }
    }
}