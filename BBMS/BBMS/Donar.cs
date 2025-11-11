using System;
using System.Data;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Donar : UserControl
    {
        // 1. Servicio para operaciones de donaciones/inventario.
        private readonly DonarService _service;
        // 2. Variable para almacenar el stock anterior.
        private int oldstock = 0;

        // 3. Constructor: inicializa componentes y configura controles.
        public Donar()
        {
            InitializeComponent();
            _service = new DonarService();

            // 4. Configura los DataGridView para selección y solo lectura.
            DonorsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DonorsDGV.MultiSelect = false;
            DonorsDGV.ReadOnly = true;

            BloodStockDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BloodStockDGV.MultiSelect = false;
            BloodStockDGV.ReadOnly = true;

            // 5. Carga inicial de datos.
            populate();
            bloodStock();
            DonorsDGV.SelectionChanged += DonorsDGV_SelectionChanged;
        }

        // 6. Llena la lista de donantes en el DataGridView.
        private void populate()
        {
            try
            {
                var dt = _service.GetDonors();
                DonorsDGV.DataSource = dt;

                // 7. Muestra mensaje si no hay donantes.
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se han cargado donantes. Filas: 0. Comprueba la tabla DonorTbl o la conexión.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 8. Ajusta cabeceras de columnas.
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

        // 9. Llena la grilla de inventario de sangre.
        private void bloodStock()
        {
            var dt = _service.GetBloodStock();
            BloodStockDGV.DataSource = dt;
        }

        // 10. Obtiene y guarda el stock en memoria.
        private void GetStock(string bgroup)
        {
            oldstock = _service.GetStock(bgroup);
        }

        // 11. Maneja el click en la grilla de donantes.
        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PopulateFieldsFromRowIndex(e.RowIndex);
        }

        // 12. Maneja el cambio de selección en la grilla de donantes.
        private void DonorsDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DonorsDGV.CurrentRow == null) return;
            PopulateFieldsFromRowIndex(DonorsDGV.CurrentRow.Index);
        }

        // 13. Asigna nombre y grupo desde la fila seleccionada.
        private void PopulateFieldsFromRowIndex(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= DonorsDGV.Rows.Count) return;
                var row = DonorsDGV.Rows[rowIndex];

                if (DonorsDGV.Columns["Nombre"] != null && row.Cells["Nombre"].Value != null)
                    DNameTb.Text = row.Cells["Nombre"].Value.ToString();
                else
                    DNameTb.Text = "";

                if (DonorsDGV.Columns["Grupo"] != null && row.Cells["Grupo"].Value != null)
                    BGroupTb.Text = row.Cells["Grupo"].Value.ToString();
                else
                    BGroupTb.Text = "";

                // 14. Actualiza el stock en memoria.
                GetStock(BGroupTb.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar fila: " + ex.Message);
            }
        }

        // 15. Resetea los campos del formulario.
        private void reset()
        {
            DNameTb.Text = "";
            BGroupTb.Text = "";
        }

        // 16. Evento click del botón Donar (Guna2Button): incrementa el stock.
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
                bloodStock();
                GetStock(BGroupTb.Text);
                reset();
            }
            else
            {
                MessageBox.Show("Error al actualizar stock: " + error);
            }
        }

        // 17. Eventos de UI vacíos para compatibilidad con el diseñador.
        private void Donar_Load(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void BloodStockDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}