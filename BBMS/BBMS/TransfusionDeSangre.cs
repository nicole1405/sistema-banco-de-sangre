using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
// using System.Data.SqlClient; // <-- 1. REMOVIDO
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BBMS.Clases; // <-- 2. AÑADIDO
using System.Text.RegularExpressions;

namespace BBMS
{
    public partial class TransfusionDeSangre : UserControl
    {
        private cTransfusionDatos gestorTransfusion = new cTransfusionDatos();
        private cPacienteDatos gestorPacientes = new cPacienteDatos(); // para obtener todos los campos
        int stock = 0;
        private DataTable patientsTable = null;

        public TransfusionDeSangre()
        {
            InitializeComponent();
        }

        private void TransfusionDeSangre_Load(object sender, EventArgs e)
        {
            try
            {
                SearchTb?.BringToFront();
                SearchTb?.Focus();

                fillPatientsGrid();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar datos: " + ex.Message);
            }
        }

        private void fillPatientsGrid()
        {
            try
            {
                // Obtenemos la tabla completa de pacientes para permitir filtrar por todas las columnas
                DataTable dt = gestorPacientes.ObtenerPacientes();

                if (dt == null)
                {
                    patientsTable = new DataTable();
                    patientsTable.Columns.Add("PNum", typeof(int));
                    patientsTable.Columns.Add("PName", typeof(string));
                    patientsTable.Columns.Add("PBGroup", typeof(string));
                }
                else
                {
                    // Trabajamos sobre una copia para poder ajustar CaseSensitive sin afectar al origen
                    patientsTable = dt.Copy();
                }

                patientsTable.CaseSensitive = false;
                PatientsGrid.DataSource = patientsTable.DefaultView;

                // Si existen columnas esperadas, renombramos encabezados (opcional)
                if (PatientsGrid.Columns.Contains("PNum")) PatientsGrid.Columns["PNum"].HeaderText = "ID";
                if (PatientsGrid.Columns.Contains("PName")) PatientsGrid.Columns["PName"].HeaderText = "Nombre";
                if (PatientsGrid.Columns.Contains("PBGroup")) PatientsGrid.Columns["PBGroup"].HeaderText = "Grupo";
                PatientsGrid.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de pacientes: " + ex.Message);
            }
        }

        private void FilterPatients(string query)
        {
            if (patientsTable == null) return;

            try
            {
                string safe = query.Replace("'", "''").Trim();
                if (string.IsNullOrWhiteSpace(safe))
                {
                    patientsTable.DefaultView.RowFilter = "";
                    PatientsGrid.DataSource = patientsTable.DefaultView;
                    return;
                }

                // Construimos un filtro OR sobre todas las columnas disponibles
                var parts = new List<string>();
                foreach (DataColumn col in patientsTable.Columns)
                {
                    // Evitamos columnas binarias o complejas si las hubiera
                    if (col.DataType == typeof(byte[]) || col.DataType == typeof(Guid)) continue;

                    string colNameEscaped = "[" + col.ColumnName + "]";

                    // Convertir a string para tipos no textuales para que la búsqueda funcione
                    if (col.DataType == typeof(string))
                    {
                        parts.Add(string.Format("{0} LIKE '%{1}%'", colNameEscaped, safe));
                    }
                    else
                    {
                        parts.Add(string.Format("Convert({0}, 'System.String') LIKE '%{1}%'", colNameEscaped, safe));
                    }
                }

                string rowFilter = string.Join(" OR ", parts);
                patientsTable.DefaultView.RowFilter = rowFilter;
                PatientsGrid.DataSource = patientsTable.DefaultView;
            }
            catch (Exception ex)
            {
                // Restauramos la vista completa si algo falla
                try { patientsTable.DefaultView.RowFilter = ""; } catch { }
                PatientsGrid.DataSource = patientsTable.DefaultView;
                Console.WriteLine("FilterPatients error: " + ex.Message);
            }
        }

        private void GetStock(string Bgroup)
        {
            stock = 0;
            if (string.IsNullOrWhiteSpace(Bgroup)) return;

            try
            {
                stock = gestorTransfusion.ObtenerStock(Bgroup);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener stock: " + ex.Message);
            }
        }

        private void PatientsGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (PatientsGrid.SelectedRows == null || PatientsGrid.SelectedRows.Count == 0)
                {
                    PatNameTb.Text = "";
                    BloodGroup.Text = "";
                    AvarlableLbl.Visible = false;
                    TransferBtn.Visible = false;
                    return;
                }

                DataGridViewRow row = PatientsGrid.SelectedRows[0];

                // Sacamos valores directamente de las columnas si existen
                string nombre = "";
                string grupo = "";
                int pacienteId = 0;

                if (PatientsGrid.Columns.Contains("PName") && row.Cells["PName"].Value != null)
                    nombre = row.Cells["PName"].Value.ToString();

                if (PatientsGrid.Columns.Contains("PBGroup") && row.Cells["PBGroup"].Value != null)
                    grupo = row.Cells["PBGroup"].Value.ToString();
                else if (PatientsGrid.Columns.Contains("PGroup") && row.Cells["PGroup"].Value != null)
                    grupo = row.Cells["PGroup"].Value.ToString(); // por si la columna se llama diferente

                if (PatientsGrid.Columns.Contains("PNum") && row.Cells["PNum"].Value != null)
                    int.TryParse(row.Cells["PNum"].Value.ToString(), out pacienteId);

                PatNameTb.Text = nombre;
                BloodGroup.Text = grupo;

                GetStock(BloodGroup.Text);

                if (stock > 0)
                {
                    TransferBtn.Visible = true;
                    AvarlableLbl.Text = "Stock Disponible (" + stock + " unidades)";
                    AvarlableLbl.Visible = true;
                }
                else
                {
                    TransferBtn.Visible = false;
                    AvarlableLbl.Text = "Stock No Disponible";
                    AvarlableLbl.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error procesando selección: " + ex.Message);
            }
        }

        private void SearchTb_TextChanged(object sender, EventArgs e)
        {
            FilterPatients(SearchTb.Text);
        }

        private void PatientsGrid_DoubleClick(object sender, EventArgs e)
        {
            if (TransferBtn.Visible)
            {
                TransferBtn_Click(TransferBtn, EventArgs.Empty);
            }
        }

        private void Reset()
        {
            PatNameTb.Text = "";
            BloodGroup.Text = "";
            AvarlableLbl.Visible = false;
            TransferBtn.Visible = false;
            if (PatientsGrid != null) PatientsGrid.ClearSelection();
            // No borramos SearchTb.Text para no perder la búsqueda del usuario
        }

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatNameTb.Text))
            {
                MessageBox.Show("Información Faltante. Seleccione un paciente.");
                return;
            }

            try
            {
                bool exito = gestorTransfusion.RealizarTransfusion(PatNameTb.Text, BloodGroup.Text);

                if (exito)
                {
                    MessageBox.Show("Transfusión Exitosa");
                    Reset();
                    fillPatientsGrid();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al procesar la transferencia: " + Ex.Message);
            }
        }
    }
}