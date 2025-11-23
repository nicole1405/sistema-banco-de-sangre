namespace BBMS
{
    partial class Layout
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Layout));
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnPaciente = new Guna.UI2.WinForms.Guna2Button();
            this.btnInventario = new Guna.UI2.WinForms.Guna2Button();
            this.btnDonante = new Guna.UI2.WinForms.Guna2Button();
            this.BtnEmployee = new Guna.UI2.WinForms.Guna2Button();
            this.BtnListaPaciente = new Guna.UI2.WinForms.Guna2Button();
            this.BtnMainPanel = new Guna.UI2.WinForms.Guna2Button();
            this.BtnTransfucion = new Guna.UI2.WinForms.Guna2Button();
            this.BtnVerDonantes = new Guna.UI2.WinForms.Guna2Button();
            this.sidebarPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.flowSidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnDonar = new Guna.UI2.WinForms.Guna2Button();
            this.panelLogout = new System.Windows.Forms.Panel();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.sidebarPanel.SuspendLayout();
            this.flowSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(203, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1028, 663);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // btnPaciente
            // 
            this.btnPaciente.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnPaciente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPaciente.ForeColor = System.Drawing.Color.White;
            this.btnPaciente.Image = ((System.Drawing.Image)(resources.GetObject("btnPaciente.Image")));
            this.btnPaciente.Location = new System.Drawing.Point(15, 168);
            this.btnPaciente.Name = "btnPaciente";
            this.btnPaciente.Size = new System.Drawing.Size(150, 45);
            this.btnPaciente.TabIndex = 3;
            this.btnPaciente.Text = "Paciente";
            this.btnPaciente.Click += new System.EventHandler(this.BtnPaciente_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnInventario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInventario.ForeColor = System.Drawing.Color.White;
            this.btnInventario.Image = ((System.Drawing.Image)(resources.GetObject("btnInventario.Image")));
            this.btnInventario.Location = new System.Drawing.Point(15, 117);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(150, 45);
            this.btnInventario.TabIndex = 2;
            this.btnInventario.Text = "Inventario";
            this.btnInventario.Click += new System.EventHandler(this.BtnInventario_Click);
            // 
            // btnDonante
            // 
            this.btnDonante.BorderColor = System.Drawing.Color.IndianRed;
            this.btnDonante.BorderRadius = 3;
            this.btnDonante.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnDonante.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDonante.ForeColor = System.Drawing.Color.White;
            this.btnDonante.Image = ((System.Drawing.Image)(resources.GetObject("btnDonante.Image")));
            this.btnDonante.Location = new System.Drawing.Point(15, 15);
            this.btnDonante.Name = "btnDonante";
            this.btnDonante.Size = new System.Drawing.Size(150, 45);
            this.btnDonante.TabIndex = 1;
            this.btnDonante.Text = "Donante";
            this.btnDonante.Click += new System.EventHandler(this.BtnDonante_Click);
            // 
            // BtnEmployee
            // 
            this.BtnEmployee.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.BtnEmployee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnEmployee.ForeColor = System.Drawing.Color.White;
            this.BtnEmployee.Image = ((System.Drawing.Image)(resources.GetObject("BtnEmployee.Image")));
            this.BtnEmployee.Location = new System.Drawing.Point(15, 219);
            this.BtnEmployee.Name = "BtnEmployee";
            this.BtnEmployee.Size = new System.Drawing.Size(150, 45);
            this.BtnEmployee.TabIndex = 4;
            this.BtnEmployee.Text = "Empleados";
            this.BtnEmployee.Click += new System.EventHandler(this.BtnEmployee_Click);
            // 
            // BtnListaPaciente
            // 
            this.BtnListaPaciente.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.BtnListaPaciente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnListaPaciente.ForeColor = System.Drawing.Color.White;
            this.BtnListaPaciente.Image = ((System.Drawing.Image)(resources.GetObject("BtnListaPaciente.Image")));
            this.BtnListaPaciente.Location = new System.Drawing.Point(15, 270);
            this.BtnListaPaciente.Name = "BtnListaPaciente";
            this.BtnListaPaciente.Size = new System.Drawing.Size(150, 45);
            this.BtnListaPaciente.TabIndex = 5;
            this.BtnListaPaciente.Text = "Lista de Pacientes";
            this.BtnListaPaciente.Click += new System.EventHandler(this.BtnListaPaciente_Click);
            // 
            // BtnMainPanel
            // 
            this.BtnMainPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.BtnMainPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnMainPanel.ForeColor = System.Drawing.Color.White;
            this.BtnMainPanel.Image = ((System.Drawing.Image)(resources.GetObject("BtnMainPanel.Image")));
            this.BtnMainPanel.Location = new System.Drawing.Point(15, 321);
            this.BtnMainPanel.Name = "BtnMainPanel";
            this.BtnMainPanel.Size = new System.Drawing.Size(150, 45);
            this.BtnMainPanel.TabIndex = 6;
            this.BtnMainPanel.Text = "Panel Principal";
            this.BtnMainPanel.Click += new System.EventHandler(this.BtnMainPanel_Click);
            // 
            // BtnTransfucion
            // 
            this.BtnTransfucion.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.BtnTransfucion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnTransfucion.ForeColor = System.Drawing.Color.White;
            this.BtnTransfucion.Image = ((System.Drawing.Image)(resources.GetObject("BtnTransfucion.Image")));
            this.BtnTransfucion.Location = new System.Drawing.Point(15, 372);
            this.BtnTransfucion.Name = "BtnTransfucion";
            this.BtnTransfucion.Size = new System.Drawing.Size(150, 45);
            this.BtnTransfucion.TabIndex = 7;
            this.BtnTransfucion.Text = "Transfución de Sangre";
            this.BtnTransfucion.Click += new System.EventHandler(this.BtnTransfucion_Click);
            // 
            // BtnVerDonantes
            // 
            this.BtnVerDonantes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.BtnVerDonantes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnVerDonantes.ForeColor = System.Drawing.Color.White;
            this.BtnVerDonantes.Image = ((System.Drawing.Image)(resources.GetObject("BtnVerDonantes.Image")));
            this.BtnVerDonantes.Location = new System.Drawing.Point(15, 423);
            this.BtnVerDonantes.Name = "BtnVerDonantes";
            this.BtnVerDonantes.Size = new System.Drawing.Size(150, 45);
            this.BtnVerDonantes.TabIndex = 8;
            this.BtnVerDonantes.Text = "Ver Donantes";
            this.BtnVerDonantes.Click += new System.EventHandler(this.BtnVerDonantes_Click);
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.Controls.Add(this.flowSidebar);
            this.sidebarPanel.Controls.Add(this.panelLogout);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(203, 663);
            this.sidebarPanel.TabIndex = 1;
            this.sidebarPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebarPanel_Paint);
            // 
            // flowSidebar
            // 
            this.flowSidebar.AutoScroll = true;
            this.flowSidebar.Controls.Add(this.btnDonante);
            this.flowSidebar.Controls.Add(this.BtnDonar);
            this.flowSidebar.Controls.Add(this.btnInventario);
            this.flowSidebar.Controls.Add(this.btnPaciente);
            this.flowSidebar.Controls.Add(this.BtnEmployee);
            this.flowSidebar.Controls.Add(this.BtnListaPaciente);
            this.flowSidebar.Controls.Add(this.BtnMainPanel);
            this.flowSidebar.Controls.Add(this.BtnTransfucion);
            this.flowSidebar.Controls.Add(this.BtnVerDonantes);
            this.flowSidebar.Controls.Add(this.LogoutBtn);
            this.flowSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSidebar.Location = new System.Drawing.Point(0, 0);
            this.flowSidebar.Name = "flowSidebar";
            this.flowSidebar.Padding = new System.Windows.Forms.Padding(12);
            this.flowSidebar.Size = new System.Drawing.Size(203, 583);
            this.flowSidebar.TabIndex = 0;
            this.flowSidebar.WrapContents = false;
            // 
            // BtnDonar
            // 
            this.BtnDonar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.BtnDonar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnDonar.ForeColor = System.Drawing.Color.White;
            this.BtnDonar.Image = ((System.Drawing.Image)(resources.GetObject("BtnDonar.Image")));
            this.BtnDonar.Location = new System.Drawing.Point(15, 66);
            this.BtnDonar.Name = "BtnDonar";
            this.BtnDonar.Size = new System.Drawing.Size(150, 45);
            this.BtnDonar.TabIndex = 9;
            this.BtnDonar.Text = "Donar";
            this.BtnDonar.Click += new System.EventHandler(this.BtnDonar_Click);
            // 
            // panelLogout
            // 
            this.panelLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLogout.Location = new System.Drawing.Point(0, 583);
            this.panelLogout.Name = "panelLogout";
            this.panelLogout.Padding = new System.Windows.Forms.Padding(12);
            this.panelLogout.Size = new System.Drawing.Size(203, 80);
            this.panelLogout.TabIndex = 1;
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.BackColor = System.Drawing.Color.Lavender;
            this.LogoutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.LogoutBtn.ForeColor = System.Drawing.Color.OrangeRed;
            this.LogoutBtn.Location = new System.Drawing.Point(15, 474);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(150, 45);
            this.LogoutBtn.TabIndex = 10;
            this.LogoutBtn.Text = "Cerrar Sesión";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // Layout
            // 
            this.ClientSize = new System.Drawing.Size(1231, 663);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "Layout";
            this.Text = "Layout";
            this.sidebarPanel.ResumeLayout(false);
            this.flowSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private Guna.UI2.WinForms.Guna2Button btnPaciente;
        private Guna.UI2.WinForms.Guna2Button btnInventario;
        private Guna.UI2.WinForms.Guna2Button btnDonante;
        private Guna.UI2.WinForms.Guna2Button BtnEmployee;
        private Guna.UI2.WinForms.Guna2Button BtnListaPaciente;
        private Guna.UI2.WinForms.Guna2Button BtnMainPanel;
        private Guna.UI2.WinForms.Guna2Button BtnTransfucion;
        private Guna.UI2.WinForms.Guna2Button BtnVerDonantes;
        private Guna.UI2.WinForms.Guna2Panel sidebarPanel;
        private System.Windows.Forms.FlowLayoutPanel flowSidebar;
        private Guna.UI2.WinForms.Guna2Button BtnDonar;
        private System.Windows.Forms.Panel panelLogout;
        private System.Windows.Forms.Button LogoutBtn;
    }
}