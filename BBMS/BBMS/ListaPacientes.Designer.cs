namespace BBMS
{
    partial class ListaPacientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PBGroupCb = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.PAddressTb = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.PAgeTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.PGenCb = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.PNameTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.PphoneTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.PatientsDGV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1073, 54);
            this.panel2.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(333, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gestión Banco de Sangre";
            // 
            // PBGroupCb
            // 
            this.PBGroupCb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
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
            this.PBGroupCb.Location = new System.Drawing.Point(332, 213);
            this.PBGroupCb.Name = "PBGroupCb";
            this.PBGroupCb.Size = new System.Drawing.Size(179, 29);
            this.PBGroupCb.TabIndex = 62;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label16.Location = new System.Drawing.Point(328, 181);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(183, 23);
            this.label16.TabIndex = 61;
            this.label16.Text = "Grupo sanguíneo:";
            // 
            // PAddressTb
            // 
            this.PAddressTb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PAddressTb.ForeColor = System.Drawing.Color.Black;
            this.PAddressTb.Location = new System.Drawing.Point(558, 213);
            this.PAddressTb.Multiline = true;
            this.PAddressTb.Name = "PAddressTb";
            this.PAddressTb.Size = new System.Drawing.Size(169, 29);
            this.PAddressTb.TabIndex = 60;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label15.Location = new System.Drawing.Point(554, 184);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 23);
            this.label15.TabIndex = 59;
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
            this.PAgeTb.Location = new System.Drawing.Point(326, 139);
            this.PAgeTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PAgeTb.Name = "PAgeTb";
            this.PAgeTb.PlaceholderText = "";
            this.PAgeTb.SelectedText = "";
            this.PAgeTb.Size = new System.Drawing.Size(185, 33);
            this.PAgeTb.TabIndex = 58;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label14.Location = new System.Drawing.Point(536, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(212, 23);
            this.label14.TabIndex = 57;
            this.label14.Text = "Número de Teléfono:";
            // 
            // PGenCb
            // 
            this.PGenCb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PGenCb.FormattingEnabled = true;
            this.PGenCb.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.PGenCb.Location = new System.Drawing.Point(761, 143);
            this.PGenCb.Name = "PGenCb";
            this.PGenCb.Size = new System.Drawing.Size(150, 29);
            this.PGenCb.TabIndex = 55;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label13.Location = new System.Drawing.Point(757, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 23);
            this.label13.TabIndex = 54;
            this.label13.Text = "Género:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label12.Location = new System.Drawing.Point(322, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 23);
            this.label12.TabIndex = 53;
            this.label12.Text = "Edad:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label11.Location = new System.Drawing.Point(108, 111);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 23);
            this.label11.TabIndex = 52;
            this.label11.Text = "Nombre:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label10.Location = new System.Drawing.Point(428, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(226, 29);
            this.label10.TabIndex = 50;
            this.label10.Text = "Lista de pacientes";
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
            this.guna2Button1.FillColor = System.Drawing.Color.SpringGreen;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.Black;
            this.guna2Button1.Location = new System.Drawing.Point(326, 252);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(209, 37);
            this.guna2Button1.TabIndex = 64;
            this.guna2Button1.Text = "Editar";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2Button2
            // 
            this.guna2Button2.AutoRoundedCorners = true;
            this.guna2Button2.BorderColor = System.Drawing.Color.DarkBlue;
            this.guna2Button2.BorderRadius = 17;
            this.guna2Button2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.guna2Button2.BorderThickness = 2;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.LightCoral;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button2.ForeColor = System.Drawing.Color.Black;
            this.guna2Button2.Location = new System.Drawing.Point(558, 252);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(209, 37);
            this.guna2Button2.TabIndex = 65;
            this.guna2Button2.Text = "Borrar";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
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
            this.PNameTb.Location = new System.Drawing.Point(112, 138);
            this.PNameTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PNameTb.Name = "PNameTb";
            this.PNameTb.PlaceholderText = "";
            this.PNameTb.SelectedText = "";
            this.PNameTb.Size = new System.Drawing.Size(185, 33);
            this.PNameTb.TabIndex = 66;
            // 
            // PphoneTb
            // 
            this.PphoneTb.AutoRoundedCorners = true;
            this.PphoneTb.BorderColor = System.Drawing.Color.Indigo;
            this.PphoneTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PphoneTb.DefaultText = "";
            this.PphoneTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PphoneTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PphoneTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PphoneTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PphoneTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PphoneTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PphoneTb.ForeColor = System.Drawing.Color.Black;
            this.PphoneTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PphoneTb.Location = new System.Drawing.Point(540, 138);
            this.PphoneTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PphoneTb.Name = "PphoneTb";
            this.PphoneTb.PlaceholderText = "";
            this.PphoneTb.SelectedText = "";
            this.PphoneTb.Size = new System.Drawing.Size(185, 33);
            this.PphoneTb.TabIndex = 67;
            // 
            // PatientsDGV
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.PatientsDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatientsDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PatientsDGV.ColumnHeadersHeight = 25;
            this.PatientsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PatientsDGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.PatientsDGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.PatientsDGV.Location = new System.Drawing.Point(112, 312);
            this.PatientsDGV.Name = "PatientsDGV";
            this.PatientsDGV.RowHeadersVisible = false;
            this.PatientsDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PatientsDGV.Size = new System.Drawing.Size(811, 284);
            this.PatientsDGV.TabIndex = 68;
            this.PatientsDGV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.PatientsDGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.PatientsDGV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.PatientsDGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.PatientsDGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.PatientsDGV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.PatientsDGV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.PatientsDGV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.Navy;
            this.PatientsDGV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.PatientsDGV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatientsDGV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.PatientsDGV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.PatientsDGV.ThemeStyle.HeaderStyle.Height = 25;
            this.PatientsDGV.ThemeStyle.ReadOnly = false;
            this.PatientsDGV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.PatientsDGV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.PatientsDGV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatientsDGV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.PatientsDGV.ThemeStyle.RowsStyle.Height = 22;
            this.PatientsDGV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.PatientsDGV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.PatientsDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatientsDGV_CellContentClick);
            // 
            // ListaPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1073, 612);
            this.Controls.Add(this.PatientsDGV);
            this.Controls.Add(this.PphoneTb);
            this.Controls.Add(this.PNameTb);
            this.Controls.Add(this.guna2Button2);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.PBGroupCb);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.PAddressTb);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.PAgeTb);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.PGenCb);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel2);
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ListaPacientes";
            //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListaPacientes";
            this.Load += new System.EventHandler(this.ListaPacientes_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PBGroupCb;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox PAddressTb;
        private System.Windows.Forms.Label label15;
        private Guna.UI2.WinForms.Guna2TextBox PAgeTb;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox PGenCb;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2TextBox PNameTb;
        private Guna.UI2.WinForms.Guna2TextBox PphoneTb;
        private Guna.UI2.WinForms.Guna2DataGridView PatientsDGV;
    }
}