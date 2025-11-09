using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBMS
{
    public partial class TransfusionDeSangre : Form
    {
        public TransfusionDeSangre()
        {
            InitializeComponent();
            // fillPatientCb();  <-- moved to Load event to avoid SelectedValue firing antes de inicialización
        }

        // Mantén la cadena igual que tenías (puedes moverla a App.config y leer con ConfigurationManager)
        private readonly string connStr = "Data Source=FIDEV;Initial Catalog=BancoDeSangre;Persist Security Info=True;User ID=sa;Password=Delta92_$1911;TrustServerCertificate=True";
        int stock = 0;

        private void TransfusionDeSangre_Load(object sender, EventArgs e)
        {
            try
            {
                fillPatientCb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar datos: " + ex.Message);
            }
        }

        private void fillPatientCb()
        {
            var dt = new DataTable();
            dt.Columns.Add("PNum", typeof(string));

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("SELECT PNum FROM PatientTbl", con))
                {
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                }

                PatientIdCb.ValueMember = "PNum";
                PatientIdCb.DisplayMember = "PNum";
                PatientIdCb.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pacientes: " + ex.Message);
            }
        }

        private void GetData()
        {
            if (PatientIdCb.SelectedValue == null) return;

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("SELECT PName, PBGroup FROM PatientTbl WHERE PNum = @pnum", con))
                {
                    cmd.Parameters.AddWithValue("@pnum", PatientIdCb.SelectedValue.ToString());
                    var dt = new DataTable();
                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        var dr = dt.Rows[0];
                        PatNameTb.Text = dr["PName"].ToString();
                        BloodGroup.Text = dr["PBGroup"].ToString();
                    }
                    else
                    {
                        PatNameTb.Text = "";
                        BloodGroup.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos del paciente: " + ex.Message);
            }
        }

        private void GetStock(string Bgroup)
        {
            stock = 0;
            if (string.IsNullOrWhiteSpace(Bgroup)) return;

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("SELECT BStock FROM BloodTbl WHERE BGroup = @bg", con))
                {
                    cmd.Parameters.AddWithValue("@bg", Bgroup);
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        stock = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener stock: " + ex.Message);
            }
        }

        private void PatientIdCb_SelectedValueChanged(object sender, EventArgs e)
        {
            // Evitar NRE si el datasource aún no está establecido.
            if (PatientIdCb.SelectedValue == null) return;

            GetData();
            GetStock(BloodGroup.Text);
            if (stock > 0)
            {
                TransferBtn.Visible = true;
                AvarlableLbl.Text = "Stock Disponible";
                AvarlableLbl.Visible = true;
            }
            else
            {
                AvarlableLbl.Text = "Stock No Disponible";
                AvarlableLbl.Visible = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Pat = new Paciente();
            Pat.Show();
            this.Hide();
        }

        private void Reset()
        {
            PatNameTb.Text = "";
            //PatientIdCb.SelectedIndex = -1;
            BloodGroup.Text = "";
            AvarlableLbl.Visible = false;
            TransferBtn.Visible = false;
        }

        private void updateStock(string bgroup)
        {
            int newstock = stock - 1;
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("UPDATE BloodTbl SET BStock = @newstock WHERE BGroup = @bg", con))
                {
                    cmd.Parameters.AddWithValue("@newstock", newstock);
                    cmd.Parameters.AddWithValue("@bg", bgroup);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al actualizar stock: " + Ex.Message);
            }
        }

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatNameTb.Text))
            {
                MessageBox.Show("Información Faltante");
                return;
            }

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("INSERT INTO TransferTbl (PName, BGroup) VALUES (@pname, @bgroup)", con))
                {
                    cmd.Parameters.AddWithValue("@pname", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@bgroup", BloodGroup.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transfusión Exitosa");

                // Actualizar stock y limpiar
                GetStock(BloodGroup.Text);
                updateStock(BloodGroup.Text);
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al realizar la transferencia: " + Ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            InventarioDeSangre Bstock = new InventarioDeSangre();
            Bstock.Show();
            this.Hide();
        }
    }
}