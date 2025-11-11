namespace BBMS
{
    partial class Paciente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Paciente));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PBGroupCb = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.PAdressTb = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.PAgeTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.PPhoneTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.PGenCb = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.PNameTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(484, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(97, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // PBGroupCb
            // 
            this.PBGroupCb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PBGroupCb.ForeColor = System.Drawing.Color.SteelBlue;
            this.PBGroupCb.FormattingEnabled = true;
            this.PBGroupCb.Items.AddRange(new object[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.PBGroupCb.Location = new System.Drawing.Point(424, 316);
            this.PBGroupCb.Name = "PBGroupCb";
            this.PBGroupCb.Size = new System.Drawing.Size(202, 29);
            this.PBGroupCb.TabIndex = 48;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label16.Location = new System.Drawing.Point(420, 284);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(183, 23);
            this.label16.TabIndex = 47;
            this.label16.Text = "Grupo sanguíneo:";
            // 
            // PAdressTb
            // 
            this.PAdressTb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PAdressTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PAdressTb.ForeColor = System.Drawing.Color.SteelBlue;
            this.PAdressTb.Location = new System.Drawing.Point(424, 413);
            this.PAdressTb.Multiline = true;
            this.PAdressTb.Name = "PAdressTb";
            this.PAdressTb.Size = new System.Drawing.Size(208, 97);
            this.PAdressTb.TabIndex = 46;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label15.Location = new System.Drawing.Point(420, 384);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 23);
            this.label15.TabIndex = 45;
            this.label15.Text = "Dirección:";
            // 
            // PAgeTb
            // 
            this.PAgeTb.AutoRoundedCorners = true;
            this.PAgeTb.BorderColor = System.Drawing.Color.Indigo;
            this.PAgeTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PAgeTb.DefaultText = "";
            this.PAgeTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PAgeTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PAgeTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PAgeTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PAgeTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PAgeTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PAgeTb.ForeColor = System.Drawing.Color.Black;
            this.PAgeTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PAgeTb.Location = new System.Drawing.Point(418, 206);
            this.PAgeTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PAgeTb.Name = "PAgeTb";
            this.PAgeTb.PlaceholderText = "";
            this.PAgeTb.SelectedText = "";
            this.PAgeTb.Size = new System.Drawing.Size(208, 33);
            this.PAgeTb.TabIndex = 44;
            this.PAgeTb.TextChanged += new System.EventHandler(this.PAgeTb_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label14.Location = new System.Drawing.Point(678, 178);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(212, 23);
            this.label14.TabIndex = 43;
            this.label14.Text = "Número de Teléfono:";
            // 
            // PPhoneTb
            // 
            this.PPhoneTb.AutoRoundedCorners = true;
            this.PPhoneTb.BorderColor = System.Drawing.Color.Indigo;
            this.PPhoneTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PPhoneTb.DefaultText = "";
            this.PPhoneTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PPhoneTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PPhoneTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PPhoneTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PPhoneTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PPhoneTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PPhoneTb.ForeColor = System.Drawing.Color.Black;
            this.PPhoneTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PPhoneTb.Location = new System.Drawing.Point(682, 206);
            this.PPhoneTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PPhoneTb.Name = "PPhoneTb";
            this.PPhoneTb.PlaceholderText = "";
            this.PPhoneTb.SelectedText = "";
            this.PPhoneTb.Size = new System.Drawing.Size(208, 33);
            this.PPhoneTb.TabIndex = 42;
            // 
            // PGenCb
            // 
            this.PGenCb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PGenCb.ForeColor = System.Drawing.Color.SteelBlue;
            this.PGenCb.FormattingEnabled = true;
            this.PGenCb.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.PGenCb.Location = new System.Drawing.Point(145, 312);
            this.PGenCb.Name = "PGenCb";
            this.PGenCb.Size = new System.Drawing.Size(160, 29);
            this.PGenCb.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label13.Location = new System.Drawing.Point(141, 280);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 23);
            this.label13.TabIndex = 40;
            this.label13.Text = "Género:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label12.Location = new System.Drawing.Point(414, 179);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 23);
            this.label12.TabIndex = 39;
            this.label12.Text = "Edad:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label11.Location = new System.Drawing.Point(141, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 23);
            this.label11.TabIndex = 38;
            this.label11.Text = "Nombre:";
            // 
            // PNameTb
            // 
            this.PNameTb.AutoRoundedCorners = true;
            this.PNameTb.BorderColor = System.Drawing.Color.Indigo;
            this.PNameTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PNameTb.DefaultText = "";
            this.PNameTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PNameTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PNameTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PNameTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PNameTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PNameTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PNameTb.ForeColor = System.Drawing.Color.Black;
            this.PNameTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PNameTb.Location = new System.Drawing.Point(145, 206);
            this.PNameTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PNameTb.Name = "PNameTb";
            this.PNameTb.PlaceholderText = "";
            this.PNameTb.SelectedText = "";
            this.PNameTb.Size = new System.Drawing.Size(208, 33);
            this.PNameTb.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label10.Location = new System.Drawing.Point(475, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 29);
            this.label10.TabIndex = 36;
            this.label10.Text = "Paciente";
            // 
            // guna2Button1
            // 
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.BorderColor = System.Drawing.Color.DarkBlue;
            this.guna2Button1.BorderRadius = 17;
            this.guna2Button1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.guna2Button1.BorderThickness = 2;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.SystemColors.InactiveCaption;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.Black;
            this.guna2Button1.Location = new System.Drawing.Point(424, 532);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(209, 37);
            this.guna2Button1.TabIndex = 50;
            this.guna2Button1.Text = "Guardar";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1073, 54);
            this.panel2.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(313, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gestión Banco de Sangre";
            // 
            // Paciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PBGroupCb);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.PAdressTb);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.PAgeTb);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.PPhoneTb);
            this.Controls.Add(this.PGenCb);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.PNameTb);
            this.Controls.Add(this.label10);
            this.Name = "Paciente";
            this.Size = new System.Drawing.Size(1073, 612);
            this.Load += new System.EventHandler(this.Paciente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox PBGroupCb;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox PAdressTb;
        private System.Windows.Forms.Label label15;
        private Guna.UI2.WinForms.Guna2TextBox PAgeTb;
        private System.Windows.Forms.Label label14;
        private Guna.UI2.WinForms.Guna2TextBox PPhoneTb;
        private System.Windows.Forms.ComboBox PGenCb;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2TextBox PNameTb;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}