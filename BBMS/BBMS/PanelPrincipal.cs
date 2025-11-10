using System;
using System.Data;
//using System.Data.SqlClient; // <-- 1. REMOVIDO
using System.Windows.Forms;
using BBMS.Clases; // <-- 2. AÑADIDO

namespace BBMS
{
    public partial class PanelPrincipal : Form
    {
        // 3. Instanciar la nueva clase de lógica de datos
        private cDashboardDatos gestorDashboard = new cDashboardDatos();

        public PanelPrincipal()
        {
            InitializeComponent();
        }

        // 4. 'SqlConnection Con' ha sido REMOVIDA

        // 5. GetData() COMPLETAMENTE REFACTORIZADO
        private void GetData()
        {
            try
            {
                // 1. Obtener TODOS los datos en una sola llamada eficiente
                EstadisticasDashboard datos = gestorDashboard.ObtenerEstadisticas();

                // 2. Actualizar los contadores simples (Labels)
                DonorLbl.Text = datos.ConteoDonantes.ToString();
                TransferLbl.Text = datos.ConteoTransferencias.ToString();
                EmployeeLbl.Text = datos.ConteoEmpleados.ToString();
                TotalLbl.Text = datos.StockTotalSangre.ToString();

                // 3. Lógica de UI (cálculo de porcentajes)
                int totalStock = datos.StockTotalSangre;

                // Helper local para actualizar un grupo (Label + ProgressBar)
                // Esta lógica es de UI, por lo que se queda en el formulario.
                void ActualizarGrupoUI(int stockGrupo, Label label, Guna.UI2.WinForms.Guna2CircleProgressBar progress)
                {
                    label.Text = stockGrupo.ToString();

                    int pct = 0;
                    if (totalStock > 0) // Evitar división por cero
                        pct = (int)Math.Round((stockGrupo / (double)totalStock) * 100);

                    // Asegurar que el porcentaje esté entre 0 y 100
                    if (pct < 0) pct = 0;
                    if (pct > 100) pct = 100;

                    if (progress != null)
                        progress.Value = pct;
                }

                // 4. Actualizar todos los grupos de sangre en la UI
                ActualizarGrupoUI(datos.StockO_Pos, OplusNumLbl, OplusProgress);
                ActualizarGrupoUI(datos.StockAB_Pos, ABplusLabel, ABplusProgress);
                ActualizarGrupoUI(datos.StockO_Neg, OminusLabel, OminusProgress);
                ActualizarGrupoUI(datos.StockAB_Neg, ABminuslbl, ABminusProgress);
                // Si agregaste más grupos en la query, llámalos aquí
            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado al actualizar la UI
                MessageBox.Show("Error al mostrar datos en el panel: " + ex.Message);
            }
        }

        private void PanelPrincipal_Load(object sender, EventArgs e)
        {
            GetData();
        }

        
        private void EmployeeLbl_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Donante Ob = new Donante();
            Ob.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Verdonantes Ob = new Verdonantes();
            Ob.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Donar Ob = new Donar();
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

        }
    }
}
