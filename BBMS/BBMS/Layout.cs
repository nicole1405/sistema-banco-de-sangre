using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BBMS.Clases;
using Guna.UI2.WinForms;
using System.Linq;

namespace BBMS
{
    public partial class Layout : Form
    {
        private Button notifButton;
        private Panel notifPanel;
        private ListBox notifListBox;
        private Timer notifTimer;
        private FlowLayoutPanel notifListPanel;
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
            // Botón campana
            notifButton = new Button
            {
                Text = "🔔",
                AutoSize = false,
                Size = new Size(40, 35),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 50, 8),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat
            };
            notifButton.FlatAppearance.BorderSize = 0;
            notifButton.Click += NotifButton_Click;
            this.Controls.Add(notifButton);
            notifButton.BringToFront();

            // Panel Guna moderno
            notifPanel = new Panel
            {
                Width = 350,
                Height = 280,
                BackColor = Color.White,
                Visible = false
            };

            notifPanel.Location = new Point(
                this.ClientSize.Width - notifPanel.Width - 20,
                notifButton.Bottom + 5
            );

            notifPanel.BorderStyle = BorderStyle.FixedSingle;
            notifPanel.Padding = new Padding(10);

            // Botón cerrar
            var closeBtn = new Button
            {
                Text = "X",
                Size = new Size(25, 25),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.DarkGray,
                Location = new Point(notifPanel.Width - 35, 5)
            };
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Click += (s, e) => notifPanel.Visible = false;
            notifPanel.Controls.Add(closeBtn);

            // FlowLayoutPanel para tarjetas
            notifListPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(5, 35, 5, 5)
            };

            notifPanel.Controls.Add(notifListPanel);

            this.Controls.Add(notifPanel);

            // Cerrar si se hace clic fuera 
            this.Click += (s, e) =>
            {
                if (notifPanel.Visible &&
                    !notifPanel.Bounds.Contains(PointToClient(Cursor.Position)) &&
                    !notifButton.Bounds.Contains(PointToClient(Cursor.Position)))
                {
                    notifPanel.Visible = false;
                }
            };

            UpdateNotificationBadge();
        }
        private Panel CreateNotificationCard(string tipo, string mensaje, string fecha, string prioridad)
        {
            Color borderColor;

            switch (prioridad)
            {
                case "Alta":
                    borderColor = Color.Red;
                    break;

                case "Media":
                    borderColor = Color.Orange;
                    break;

                default:
                    borderColor = Color.Silver;
                    break;
            }

            Panel card = new Panel
            {
                Height = 90,
                Width = notifListPanel.Width - 30,
                BackColor = Color.White,
                Padding = new Padding(10),
                Margin = new Padding(3),
                BorderStyle = BorderStyle.FixedSingle
            };

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 245, 245);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            Label lblTipo = new Label()
            {
                Text = tipo,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = borderColor,
                Dock = DockStyle.Top
            };

            Label lblMensaje = new Label()
            {
                Text = mensaje,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Dock = DockStyle.Top
            };

            Label lblFecha = new Label()
            {
                Text = fecha,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Dock = DockStyle.Bottom
            };

            card.Controls.Add(lblFecha);
            card.Controls.Add(lblMensaje);
            card.Controls.Add(lblTipo);

            return card;
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
            int? user = GetCurrentUserId();
            if (user == null)
            {
                MessageBox.Show("Inicia sesión para ver notificaciones.");
                return;
            }

            var dt = NotificationService.GetUnreadForUser(user.Value);
            notifListPanel.Controls.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string tipo = row["TipoNotificacion"]?.ToString() ?? "Notificación";
                string mensaje = row["Mensaje"]?.ToString() ?? "--";
                string prioridad = row["Prioridad"]?.ToString() ?? "Baja";
                string fecha = Convert.ToDateTime(row["CreatedAt"]).ToString("g");

                notifListPanel.Controls.Add(
                    CreateNotificationCard(tipo, mensaje, fecha, prioridad)
                );
            }

            notifPanel.Visible = true;

            NotificationService.MarkAllAsReadForUser(user.Value);
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