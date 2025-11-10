using System;
using System.Data;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Donar : Form
    {
        private readonly DonarService _service;
        private int oldstock = 0;

        public Donar()
        {
            InitializeComponent();
            _service = new DonarService();

            // Inicializa UI
            DonorsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DonorsDGV.MultiSelect = false;
            DonorsDGV.ReadOnly = true;

            BloodStockDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BloodStockDGV.MultiSelect = false;
            BloodStockDGV.ReadOnly = true;

            // Carga inicial
            populate();
            bloodStock();
            DonorsDGV.SelectionChanged += DonorsDGV_SelectionChanged;
        }

        // Llena la lista de donantes
        private void populate()
        {
            try
            {
                var dt = _service.GetDonors();
                DonorsDGV.DataSource = dt;

                // Depuración: mostrar cuántas filas llegaron
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se han cargado donantes. Filas: 0. Comprueba la tabla DonorTbl o la conexión.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Opcional: ajustar cabeceras
                    if (DonorsDGV.Columns["Nombre"] != null)
                        DonorsDGV.Columns["Nombre"].HeaderText = "Nombre";
                    if (DonorsDGV.Columns["Edad"] != null)
                        DonorsDGV.Columns["Edad"].HeaderText = "Edad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar donantes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Llena la grilla de inventario de sangre
        private void bloodStock()
        {
            var dt = _service.GetBloodStock();
            BloodStockDGV.DataSource = dt;
        }

        // Obtiene y guarda el stock en memoria
        private void GetStock(string bgroup)
        {
            oldstock = _service.GetStock(bgroup);
        }

        // Maneja click en grilla de donantes (seguro)
        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PopulateFieldsFromRowIndex(e.RowIndex);
        }

        private void DonorsDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DonorsDGV.CurrentRow == null) return;
            PopulateFieldsFromRowIndex(DonorsDGV.CurrentRow.Index);
        }

        // Asigna nombre y grupo desde la fila seleccionada (usa nombres formales)
        private void PopulateFieldsFromRowIndex(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= DonorsDGV.Rows.Count) return;
                var row = DonorsDGV.Rows[rowIndex];

                // Comprobar existencia de columna en la grilla y leer el valor con seguridad
                if (DonorsDGV.Columns["Nombre"] != null && row.Cells["Nombre"].Value != null)
                    DNameTb.Text = row.Cells["Nombre"].Value.ToString();
                else
                    DNameTb.Text = "";

                if (DonorsDGV.Columns["Grupo"] != null && row.Cells["Grupo"].Value != null)
                    BGroupTb.Text = row.Cells["Grupo"].Value.ToString();
                else
                    BGroupTb.Text = "";

                // Actualiza stock en memoria
                GetStock(BGroupTb.Text);

                // Mostrar disponibilidad según stock
                //if (oldstock > 0)
                //{
                //    AvarlableLbl.Text = "Stock Disponible";
                //    TransferBtn.Visible = true;
                //}
                //else
                //{
                //    AvarlableLbl.Text = "Stock No Disponible";
                //    TransferBtn.Visible = false;
                //}
                //AvarlableLbl.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar fila: " + ex.Message);
            }
        }

        private void reset()
        {
            DNameTb.Text = "";
            BGroupTb.Text = "";
            //AvarlableLbl.Visible = false;
            //TransferBtn.Visible = false;
        }

        // Donar: incrementa stock para el grupo seleccionado y refresca grilla
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DNameTb.Text))
            {
                MessageBox.Show("Selecciona un donador");
                return;
            }

            if (string.IsNullOrWhiteSpace(BGroupTb.Text))
            {
                MessageBox.Show("Grupo sanguíneo no disponible");
                return;
            }

            if (_service.IncrementStock(BGroupTb.Text, 1, out string error))
            {
                MessageBox.Show("Donación exitosa");
                // Refrescar la grilla de stock
                bloodStock();
                // Actualizar cache
                GetStock(BGroupTb.Text);
                reset();
            }
            else
            {
                MessageBox.Show("Error al actualizar stock: " + error);
            }
        }

        private void Donar_Load(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void BloodStockDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }



        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Donante Db = new Donante();
            Db.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Verdonantes Ob = new Verdonantes();
            Ob.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Ob = new Paciente();
            Ob.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ListaPacientes Ob = new ListaPacientes();
            Ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            InventarioDeSangre Ob = new InventarioDeSangre();
            Ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            TransfusionDeSangre Ob = new TransfusionDeSangre();
            Ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            PanelPrincipal Ob = new PanelPrincipal();
            Ob.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Ob = new Login();
            Ob.Show();
            this.Hide();
        }
    }
}