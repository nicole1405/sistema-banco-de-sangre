using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBMS
{
    public partial class PanelPrincipal : Form
    {
        public PanelPrincipal()
        {
            InitializeComponent();
            // No ejecutar GetData aqui para evitar usos antes de inicializar la UI.
            // Se llama en Load.
        }

        SqlConnection Con = new SqlConnection(@"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        private void GetData()
        {
            try
            {
                // Donantes
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM DonorTbl", con))
                {
                    con.Open();
                    var donorCount = cmd.ExecuteScalar();
                    DonorLbl.Text = (donorCount != DBNull.Value && donorCount != null) ? donorCount.ToString() : "0";
                }

                // Transfisiones
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM TransferTbl", con))
                {
                    con.Open();
                    var transCount = cmd.ExecuteScalar();
                    TransferLbl.Text = (transCount != DBNull.Value && transCount != null) ? transCount.ToString() : "0";
                }

                // Empleados
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeTbl", con))
                {
                    con.Open();
                    var empCount = cmd.ExecuteScalar();
                    EmployeeLbl.Text = (empCount != DBNull.Value && empCount != null) ? empCount.ToString() : "0";
                }

                // Total de unidades en inventario (suma de BStock)
                int totalStock = 0;
                using (var con = new SqlConnection(Con.ConnectionString))
                using (var cmd = new SqlCommand("SELECT ISNULL(SUM(BStock), 0) FROM BloodTbl", con))
                {
                    con.Open();
                    var totalObj = cmd.ExecuteScalar();
                    totalStock = (totalObj != DBNull.Value && totalObj != null) ? Convert.ToInt32(totalObj) : 0;
                    TotalLbl.Text = totalStock.ToString();
                }

                // Helper para leer stock por grupo y actualizar label + progress sin lanzar excepción
                void ReadGroup(string group, Label valueLabel, Guna.UI2.WinForms.Guna2CircleProgressBar progress)
                {
                    int groupStock = 0;
                    using (var con = new SqlConnection(Con.ConnectionString))
                    using (var cmd = new SqlCommand("SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = @bg", con))
                    {
                        cmd.Parameters.AddWithValue("@bg", group);
                        con.Open();
                        var res = cmd.ExecuteScalar();
                        groupStock = (res != DBNull.Value && res != null) ? Convert.ToInt32(res) : 0;
                    }

                    valueLabel.Text = groupStock.ToString();

                    int pct = 0;
                    if (totalStock > 0)
                        pct = (int)Math.Round((groupStock / (double)totalStock) * 100);

                    // defender contra valores fuera de rango
                    if (pct < 0) pct = 0;
                    if (pct > 100) pct = 100;

                    if (progress != null)
                        progress.Value = pct;
                }

                // Actualiza todos los grupos que se muestran en la UI
                ReadGroup("O+", OplusNumLbl, OplusProgress);
                ReadGroup("AB+", ABplusLabel, ABplusProgress);
                ReadGroup("O-", OminusLabel, OminusProgress);
                ReadGroup("AB-", ABminuslbl, ABminusProgress);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de SQL al cargar datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void PanelPrincipal_Load(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
