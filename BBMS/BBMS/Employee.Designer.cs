namespace BBMS
{
    partial class Employee
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
            this.label12 = new System.Windows.Forms.Label();
            this.EmployeeDGV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.UpdateEmpBtn = new Guna.UI2.WinForms.Guna2Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.AddEmpBtn = new Guna.UI2.WinForms.Guna2Button();
            this.DeleteEmpBtn = new Guna.UI2.WinForms.Guna2Button();
            this.EmpNameTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.EmpPassTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRole = new System.Windows.Forms.Label();
            this.RoleCb = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeDGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label12.Location = new System.Drawing.Point(456, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 29);
            this.label12.TabIndex = 60;
            this.label12.Text = "Empleados";
            // 
            // EmployeeDGV
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.EmployeeDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmployeeDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.EmployeeDGV.ColumnHeadersHeight = 25;
            this.EmployeeDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EmployeeDGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.EmployeeDGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.EmployeeDGV.Location = new System.Drawing.Point(184, 90);
            this.EmployeeDGV.Name = "EmployeeDGV";
            this.EmployeeDGV.RowHeadersVisible = false;
            this.EmployeeDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.EmployeeDGV.Size = new System.Drawing.Size(689, 344);
            this.EmployeeDGV.TabIndex = 59;
            this.EmployeeDGV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.EmployeeDGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.EmployeeDGV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.EmployeeDGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.EmployeeDGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.EmployeeDGV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.EmployeeDGV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.EmployeeDGV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.Navy;
            this.EmployeeDGV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.EmployeeDGV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeDGV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.EmployeeDGV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.EmployeeDGV.ThemeStyle.HeaderStyle.Height = 25;
            this.EmployeeDGV.ThemeStyle.ReadOnly = false;
            this.EmployeeDGV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.EmployeeDGV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.EmployeeDGV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeDGV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.EmployeeDGV.ThemeStyle.RowsStyle.Height = 22;
            this.EmployeeDGV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.EmployeeDGV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.EmployeeDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DonorsDGV_CellContentClick);
            // 
            // UpdateEmpBtn
            // 
            this.UpdateEmpBtn.AutoRoundedCorners = true;
            this.UpdateEmpBtn.BorderColor = System.Drawing.Color.DarkBlue;
            this.UpdateEmpBtn.BorderRadius = 17;
            this.UpdateEmpBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.UpdateEmpBtn.BorderThickness = 2;
            this.UpdateEmpBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.UpdateEmpBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.UpdateEmpBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.UpdateEmpBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.UpdateEmpBtn.FillColor = System.Drawing.Color.Aqua;
            this.UpdateEmpBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateEmpBtn.ForeColor = System.Drawing.Color.Black;
            this.UpdateEmpBtn.Location = new System.Drawing.Point(423, 571);
            this.UpdateEmpBtn.Name = "UpdateEmpBtn";
            this.UpdateEmpBtn.Size = new System.Drawing.Size(209, 37);
            this.UpdateEmpBtn.TabIndex = 50;
            this.UpdateEmpBtn.Text = "Editar";
            this.UpdateEmpBtn.Click += new System.EventHandler(this.EditEmpBtn_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label16.Location = new System.Drawing.Point(660, 437);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(117, 23);
            this.label16.TabIndex = 57;
            this.label16.Text = "Contraseña";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label11.Location = new System.Drawing.Point(180, 437);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 23);
            this.label11.TabIndex = 55;
            this.label11.Text = "Nombre:";
            // 
            // AddEmpBtn
            // 
            this.AddEmpBtn.AutoRoundedCorners = true;
            this.AddEmpBtn.BorderColor = System.Drawing.Color.DarkBlue;
            this.AddEmpBtn.BorderRadius = 17;
            this.AddEmpBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.AddEmpBtn.BorderThickness = 2;
            this.AddEmpBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AddEmpBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AddEmpBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AddEmpBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AddEmpBtn.FillColor = System.Drawing.Color.Lime;
            this.AddEmpBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddEmpBtn.ForeColor = System.Drawing.Color.Black;
            this.AddEmpBtn.Location = new System.Drawing.Point(184, 571);
            this.AddEmpBtn.Name = "AddEmpBtn";
            this.AddEmpBtn.Size = new System.Drawing.Size(209, 37);
            this.AddEmpBtn.TabIndex = 61;
            this.AddEmpBtn.Text = "Guardar";
            this.AddEmpBtn.Click += new System.EventHandler(this.AddEmpBtn_Click);
            // 
            // DeleteEmpBtn
            // 
            this.DeleteEmpBtn.AutoRoundedCorners = true;
            this.DeleteEmpBtn.BorderColor = System.Drawing.Color.DarkBlue;
            this.DeleteEmpBtn.BorderRadius = 17;
            this.DeleteEmpBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.DeleteEmpBtn.BorderThickness = 2;
            this.DeleteEmpBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.DeleteEmpBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.DeleteEmpBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DeleteEmpBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.DeleteEmpBtn.FillColor = System.Drawing.Color.Red;
            this.DeleteEmpBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteEmpBtn.ForeColor = System.Drawing.Color.Black;
            this.DeleteEmpBtn.Location = new System.Drawing.Point(664, 571);
            this.DeleteEmpBtn.Name = "DeleteEmpBtn";
            this.DeleteEmpBtn.Size = new System.Drawing.Size(209, 37);
            this.DeleteEmpBtn.TabIndex = 62;
            this.DeleteEmpBtn.Text = "Eliminar";
            this.DeleteEmpBtn.Click += new System.EventHandler(this.DeleteEmpBtn_Click);
            // 
            // EmpNameTb
            // 
            this.EmpNameTb.AutoRoundedCorners = true;
            this.EmpNameTb.BorderColor = System.Drawing.Color.Indigo;
            this.EmpNameTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.EmpNameTb.DefaultText = "";
            this.EmpNameTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.EmpNameTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EmpNameTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmpNameTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmpNameTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmpNameTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.EmpNameTb.ForeColor = System.Drawing.Color.Black;
            this.EmpNameTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmpNameTb.Location = new System.Drawing.Point(184, 467);
            this.EmpNameTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EmpNameTb.Name = "EmpNameTb";
            this.EmpNameTb.PlaceholderText = "";
            this.EmpNameTb.SelectedText = "";
            this.EmpNameTb.Size = new System.Drawing.Size(208, 33);
            this.EmpNameTb.TabIndex = 63;
            this.EmpNameTb.TextChanged += new System.EventHandler(this.EmpNameTb_TextChanged_1);
            // 
            // EmpPassTb
            // 
            this.EmpPassTb.AutoRoundedCorners = true;
            this.EmpPassTb.BorderColor = System.Drawing.Color.Indigo;
            this.EmpPassTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.EmpPassTb.DefaultText = "";
            this.EmpPassTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.EmpPassTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EmpPassTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmpPassTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmpPassTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmpPassTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.EmpPassTb.ForeColor = System.Drawing.Color.Black;
            this.EmpPassTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmpPassTb.Location = new System.Drawing.Point(664, 470);
            this.EmpPassTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EmpPassTb.Name = "EmpPassTb";
            this.EmpPassTb.PlaceholderText = "";
            this.EmpPassTb.SelectedText = "";
            this.EmpPassTb.Size = new System.Drawing.Size(208, 33);
            this.EmpPassTb.TabIndex = 64;
            this.EmpPassTb.TextChanged += new System.EventHandler(this.EmpPassTb_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1073, 54);
            this.panel2.TabIndex = 65;
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
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.labelRole.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelRole.Location = new System.Drawing.Point(419, 437);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(50, 23);
            this.labelRole.TabIndex = 66;
            this.labelRole.Text = "Rol:";
            // 
            // RoleCb
            // 
            this.RoleCb.BackColor = System.Drawing.Color.Transparent;
            this.RoleCb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RoleCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoleCb.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RoleCb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RoleCb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RoleCb.ForeColor = System.Drawing.Color.Black;
            this.RoleCb.ItemHeight = 30;
            this.RoleCb.Items.AddRange(new object[] {
            "Administrador",
            "Doctor",
            "Enfermera"});
            this.RoleCb.Location = new System.Drawing.Point(423, 467);
            this.RoleCb.Name = "RoleCb";
            this.RoleCb.Size = new System.Drawing.Size(160, 36);
            this.RoleCb.TabIndex = 67;
            // 
            // Employee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RoleCb);
            this.Controls.Add(this.labelRole);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.EmpPassTb);
            this.Controls.Add(this.EmpNameTb);
            this.Controls.Add(this.DeleteEmpBtn);
            this.Controls.Add(this.AddEmpBtn);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.EmployeeDGV);
            this.Controls.Add(this.UpdateEmpBtn);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label11);
            this.Name = "Employee";
            this.Size = new System.Drawing.Size(1073, 630);
            this.Load += new System.EventHandler(this.Employee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeDGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label12;
        private Guna.UI2.WinForms.Guna2DataGridView EmployeeDGV;
        private Guna.UI2.WinForms.Guna2Button UpdateEmpBtn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2Button AddEmpBtn;
        private Guna.UI2.WinForms.Guna2Button DeleteEmpBtn;
        private Guna.UI2.WinForms.Guna2TextBox EmpNameTb;
        private Guna.UI2.WinForms.Guna2TextBox EmpPassTb;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRole;
        private Guna.UI2.WinForms.Guna2ComboBox RoleCb;
    }
}