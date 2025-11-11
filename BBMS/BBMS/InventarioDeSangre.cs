using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class InventarioDeSangre : UserControl
    {
        // 1. Servicio para operaciones de inventario de sangre.
        private readonly InventarioService _service;

        // 2. Constructor: inicializa componentes y configura controles.
        public InventarioDeSangre()
        {
            InitializeComponent();

            _service = new InventarioService();

            // 3. Configura el DataGridView para selección y solo lectura.
            BloodStockDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BloodStockDGV.MultiSelect = false;
            BloodStockDGV.ReadOnly = true;

            // 4. Configura el ComboBox de filtro si existe en el diseñador.
            if (this.Controls.Find("BGroupFilterCb", true).FirstOrDefault() is ComboBox cb)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.SelectedIndexChanged -= BGroupFilterCb_SelectedIndexChanged;
                cb.SelectedIndexChanged += BGroupFilterCb_SelectedIndexChanged;
            }

            // 5. Carga inicial de filtros y datos.
            LoadFilterValues();
            LoadBloodStock(null);
        }

        // 6. Carga datos del inventario; si bgroup == null o "Todos" carga todo.
        private void LoadBloodStock(string bgroup = null)
        {
            try
            {
                DataTable dt;
                if (string.IsNullOrWhiteSpace(bgroup) || bgroup == "Todos")
                {
                    dt = _service.GetBloodStock();
                }
                else
                {
                    // 7. Construye una tabla con un solo registro para el grupo filtrado.
                    dt = new DataTable();
                    dt.Columns.Add("Grupo", typeof(string));
                    dt.Columns.Add("Stock", typeof(int));
                    int stock = _service.GetStockByGroup(bgroup);
                    var row = dt.NewRow();
                    row["Grupo"] = bgroup;
                    row["Stock"] = stock;
                    dt.Rows.Add(row);
                }

                BloodStockDGV.DataSource = dt;

                // 8. Ajustes visuales de las columnas.
                if (BloodStockDGV.Columns.Contains("Grupo"))
                    BloodStockDGV.Columns["Grupo"].HeaderText = "Grupo";
                if (BloodStockDGV.Columns.Contains("Stock"))
                    BloodStockDGV.Columns["Stock"].HeaderText = "Stock";

                BloodStockDGV.AutoResizeColumns();
                BloodStockDGV.ClearSelection();
                BloodStockDGV.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar inventario: " + ex.Message);
            }
        }

        // 9. Llena ComboBox de grupos (añade "Todos").
        private void LoadFilterValues()
        {
            try
            {
                var dt = _service.GetGroups();
                if (this.Controls.Find("BGroupFilterCb", true).FirstOrDefault() is ComboBox cb)
                {
                    cb.Items.Clear();
                    cb.Items.Add("Todos");
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r[0] != DBNull.Value)
                            cb.Items.Add(r[0].ToString());
                    }
                    cb.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar filtros: " + ex.Message);
            }
        }

        // 10. Evento: cuando cambia el filtro en el ComboBox.
        private void BGroupFilterCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox cb)
            {
                var sel = cb.SelectedItem != null ? cb.SelectedItem.ToString() : "Todos";
                if (sel == "Todos")
                    LoadBloodStock(null);
                else
                    LoadBloodStock(sel);
            }
        }

        // 11. Evento de click en la grilla (opcional, sin lógica).
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Opcional
        }

        // 12. Evento de carga del control (sin lógica).
        private void InventarioDeSangre_Load(object sender, EventArgs e)
        {

        }

        // 13. Eventos de pintado y click en labels (sin lógica relevante).
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}