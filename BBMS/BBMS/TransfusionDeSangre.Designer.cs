using System;
namespace BBMS
{
    partial class TransfusionDeSangre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransfusionDeSangre));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BloodGroup = new Guna.UI2.WinForms.Guna2TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.PatNameTb = new Guna.UI2.WinForms.Guna2TextBox();
            this.SearchTb = new System.Windows.Forms.TextBox();
            this.PatientsGrid = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.AvarlableLbl = new System.Windows.Forms.Label();
            this.TransferBtn = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(505, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label10.Location = new System.Drawing.Point(411, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(276, 29);
            this.label10.TabIndex = 37;
            this.label10.Text = "Transfusión de sangre";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1073, 54);
            this.panel2.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(342, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gestión Banco de Sangre";
            // 
            // BloodGroup
            // 
            this.BloodGroup.AutoRoundedCorners = true;
            this.BloodGroup.BorderColor = System.Drawing.Color.Indigo;
            this.BloodGroup.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BloodGroup.DefaultText = "";
            this.BloodGroup.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.BloodGroup.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BloodGroup.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BloodGroup.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BloodGroup.Enabled = false;
            this.BloodGroup.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BloodGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BloodGroup.ForeColor = System.Drawing.Color.Black;
            this.BloodGroup.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BloodGroup.Location = new System.Drawing.Point(712, 235);
            this.BloodGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BloodGroup.Name = "BloodGroup";
            this.BloodGroup.PlaceholderText = "";
            this.BloodGroup.SelectedText = "";
            this.BloodGroup.Size = new System.Drawing.Size(179, 29);
            this.BloodGroup.TabIndex = 77;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label16.Location = new System.Drawing.Point(708, 205);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(183, 23);
            this.label16.TabIndex = 76;
            this.label16.Text = "Grupo sanguíneo:";
            // 
            // PatNameTb
            // 
            this.PatNameTb.AutoRoundedCorners = true;
            this.PatNameTb.BorderColor = System.Drawing.Color.Indigo;
            this.PatNameTb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PatNameTb.DefaultText = "";
            this.PatNameTb.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PatNameTb.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PatNameTb.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PatNameTb.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PatNameTb.Enabled = false;
            this.PatNameTb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PatNameTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PatNameTb.ForeColor = System.Drawing.Color.Black;
            this.PatNameTb.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PatNameTb.Location = new System.Drawing.Point(433, 236);
            this.PatNameTb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PatNameTb.Name = "PatNameTb";
            this.PatNameTb.PlaceholderText = "";
            this.PatNameTb.SelectedText = "";
            this.PatNameTb.Size = new System.Drawing.Size(213, 29);
            this.PatNameTb.TabIndex = 73;
            // 
            // SearchTb
            // 
            this.SearchTb.BackColor = System.Drawing.Color.White;
            this.SearchTb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTb.ForeColor = System.Drawing.Color.Black;
            this.SearchTb.Location = new System.Drawing.Point(208, 239);
            this.SearchTb.Name = "SearchTb";
            this.SearchTb.Size = new System.Drawing.Size(200, 29);
            this.SearchTb.TabIndex = 90;
            this.SearchTb.TextChanged += new System.EventHandler(this.SearchTb_TextChanged);
            // 
            // PatientsGrid
            // 
            this.PatientsGrid.AllowUserToAddRows = false;
            this.PatientsGrid.AllowUserToDeleteRows = false;
            this.PatientsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PatientsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientsGrid.Location = new System.Drawing.Point(208, 274);
            this.PatientsGrid.MultiSelect = false;
            this.PatientsGrid.Name = "PatientsGrid";
            this.PatientsGrid.ReadOnly = true;
            this.PatientsGrid.RowHeadersVisible = false;
            this.PatientsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PatientsGrid.Size = new System.Drawing.Size(683, 160);
            this.PatientsGrid.TabIndex = 91;
            this.PatientsGrid.SelectionChanged += new System.EventHandler(this.PatientsGrid_SelectionChanged);
            this.PatientsGrid.DoubleClick += new System.EventHandler(this.PatientsGrid_DoubleClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label13.Location = new System.Drawing.Point(204, 207);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(170, 23);
            this.label13.TabIndex = 70;
            this.label13.Text = "Buscar paciente:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label12.Location = new System.Drawing.Point(429, 204);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(217, 23);
            this.label12.TabIndex = 69;
            this.label12.Text = "Nombre del paciente:";
            // 
            // AvarlableLbl
            // 
            this.AvarlableLbl.AutoSize = true;
            this.AvarlableLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvarlableLbl.ForeColor = System.Drawing.Color.MidnightBlue;
            this.AvarlableLbl.Location = new System.Drawing.Point(379, 456);
            this.AvarlableLbl.Name = "AvarlableLbl";
            this.AvarlableLbl.Size = new System.Drawing.Size(291, 25);
            this.AvarlableLbl.TabIndex = 78;
            this.AvarlableLbl.Text = "Disponible o no disponible";
            this.AvarlableLbl.Visible = false;
            // 
            // TransferBtn
            // 
            this.TransferBtn.AutoRoundedCorners = true;
            this.TransferBtn.BorderColor = System.Drawing.Color.DarkBlue;
            this.TransferBtn.BorderRadius = 17;
            this.TransferBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.TransferBtn.BorderThickness = 2;
            this.TransferBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.TransferBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.TransferBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TransferBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.TransferBtn.FillColor = System.Drawing.Color.SpringGreen;
            this.TransferBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransferBtn.ForeColor = System.Drawing.Color.Black;
            this.TransferBtn.Location = new System.Drawing.Point(433, 500);
            this.TransferBtn.Name = "TransferBtn";
            this.TransferBtn.Size = new System.Drawing.Size(209, 37);
            this.TransferBtn.TabIndex = 79;
            this.TransferBtn.Text = "Transferir";
            this.TransferBtn.Visible = false;
            this.TransferBtn.Click += new System.EventHandler(this.TransferBtn_Click);
            // 
            // TransfusionDeSangre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TransferBtn);
            this.Controls.Add(this.AvarlableLbl);
            this.Controls.Add(this.BloodGroup);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.PatNameTb);
            this.Controls.Add(this.SearchTb);
            this.Controls.Add(this.PatientsGrid);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Name = "TransfusionDeSangre";
            this.Size = new System.Drawing.Size(1073, 612);
            this.Load += new System.EventHandler(this.TransfusionDeSangre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox BloodGroup;
        private System.Windows.Forms.Label label16;
        private Guna.UI2.WinForms.Guna2TextBox PatNameTb;
        private System.Windows.Forms.TextBox SearchTb;
        private System.Windows.Forms.DataGridView PatientsGrid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label AvarlableLbl;
        private Guna.UI2.WinForms.Guna2Button TransferBtn;
    }
}