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
    public partial class Donar : Form
    {
        public Donar()
        {
            InitializeComponent();
            populate();
        }
        // Conexión a la base de datos local
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BancoDeSangreDB.mdf;Integrated Security=True;Connect Timeout=30");

        // Método para llenar el DataGridView con todos los donantes de la base de datos
        private void populate()
        {
            // Abrimos la conexión a la BD
            Con.Open();

            // Query SQL para obtener todos los registros de la tabla DonorTbl
            string Query = "select * from DonorTbl";

            // SqlDataAdapter es como un puente entre la BD y el DataSet
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);

            // SqlCommandBuilder genera automáticamente los comandos INSERT, UPDATE, DELETE
            // buena practica incluirlo aunque no lo usemos aqui
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);

            // DataSet es como una base de datos en memoria que almacena los datos
            var ds = new DataSet();

            // Fill() llena el DataSet con los datos obtenidos de la query
            sda.Fill(ds);

            // Esto hace que la tabla se muestre en la pantalla
            DonorsDGV.DataSource = ds.Tables[0];

            // Cerramos la conexión para liberar recursos
            Con.Close();
        }
        private void Donar_Load(object sender, EventArgs e)
        {

        }

        private void DonorsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DNameTb.Text = DonorsDGV.SelectedRows[0].Cells[1].Value.ToString();
            BGroupTb.Text = DonorsDGV.SelectedRows[0].Cells[6].Value.ToString();
        }
        private void reset()
        {
            DNameTb.Text = "";
            BGroupTb.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (DNameTb.Text == "")
            {
                MessageBox.Show("Selecciona un donador");
            }
            else
            {
                try
                {
                    int stock = 1;
                    string query = "update BloodTbl set BStock=" + stock + " where PNum='" + BGroupTb.Text + "' ;";

                    // Se usa using para garantizar que la conexión se cierre automáticamente
                    using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Donación exitosa");
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error al eliminar el paciente: " + Ex.Message);
                }
            }
        }
    }
}
