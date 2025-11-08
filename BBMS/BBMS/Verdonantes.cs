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
    public partial class Verdonantes : Form
    {
        // Constructor del formulario
        public Verdonantes()
        {
            InitializeComponent();
            populate(); // Llamamos a populate() para cargar los datos al abrir el formulario
        }

        // Conexión a la base de datos local
        SqlConnection Con = new SqlConnection(@"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

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

        private void label10_Click(object sender, EventArgs e)
        {
         
        }

        // Evento que se ejecuta cuando haces click en una celda del DataGridView
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vacío por ahora
            // Aquí podriamos agregar funcionalidad como editar o eliminar un donante al hacer click
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void Verdonantes_Load(object sender, EventArgs e)
        {
            // ya llamamos a populate() en el constructor
        }
    }
}