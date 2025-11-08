using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BBMS
{
    public partial class ListaPacientes : Form
    {
        public ListaPacientes()
        {
            InitializeComponent();
            populate();
        }

        // Conexión a la base de datos
        SqlConnection Con = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=C:\Users\DELL\Documents\BancoDeSangreDB.mdf;
            Integrated Security=True;Connect Timeout=30");

        // Método para llenar la tabla de pacientes
        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                    Con.Open();

                string Query = "SELECT * FROM PatientTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                PatientsDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de pacientes: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
        }

        int key = 0;

        private void ListaPacientes_Load(object sender, EventArgs e)
        {
        }
        
        private void PatientsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PNameTb.Text = PatientsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PAgeTb.Text = PatientsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PphoneTb.Text = PatientsDGV.SelectedRows[0].Cells[3].Value.ToString();
            PGenCb.Text = PatientsDGV.SelectedRows[0].Cells[4].Value.ToString();
            PBGroupCb.Text = PatientsDGV.SelectedRows[0].Cells[5].Value.ToString();
            PAddressTb.Text = PatientsDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (PNameTb.Text == "")
                key = 0;
            else
                key = Convert.ToInt32(PatientsDGV.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void Reset()
        {
            PNameTb.Text = "";
            PAgeTb.Text = "";
            PphoneTb.Text = "";
            PAddressTb.Text = "";
            PGenCb.SelectedIndex = -1;
            PBGroupCb.SelectedIndex = -1;
            key = 0;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Selecciona el paciente a eliminar");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM PatientTbl WHERE PNum=" + key + ";";

                    // Se usa using para garantizar que la conexión se cierre automáticamente
                    using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Paciente eliminado con éxito");
                    Reset();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error al eliminar el paciente: " + Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Pat = new Paciente();
            Pat.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" ||                    // Si el nombre está vacío
                PphoneTb.Text == "" ||                   // O el teléfono está vacío
                PAgeTb.Text == "" ||                     // O la edad está vacía
                PGenCb.SelectedIndex == -1 ||            // O no se seleccionó género (-1 = sin selección)
                PBGroupCb.SelectedIndex == -1 ||         // O no se seleccionó grupo sanguíneo
                PAddressTb.Text == "")
            {
                MessageBox.Show("Falta información");
            }
            else
            {
                try
                {
                    string query = "update PatientTbl set Pname='"+PNameTb.Text+"',Page = "+PAgeTb.Text+ ",Pphone='"+PphoneTb.Text+ "', PGender = '"+PGenCb.SelectedItem.ToString()+"',PBGroup='"+PBGroupCb.SelectedItem.ToString()+"', Padrress= '"+PAddressTb.Text +"' where PNum="+key+";";

                    // Se usa using para garantizar que la conexión se cierre automáticamente
                    using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Paciente editado con éxito");
                    Reset();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error al eliminar el paciente: " + Ex.Message);
                }
            }
        }
    }
}
