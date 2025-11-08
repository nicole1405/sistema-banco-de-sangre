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
        // Constructor del formulario - se ejecuta al crear la ventana
        public ListaPacientes()
        {
            InitializeComponent();
            populate(); // Llama al método para cargar los datos al abrir el formulario
        }

        // Objeto de conexión a la base de datos SQL Server LocalDB
        // Contiene la ruta del archivo de base de datos y configuración de seguridad
        SqlConnection Con = new SqlConnection(@"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        // Método para cargar/refrescar los datos de la tabla PatientTbl en el DataGridView
        private void populate()
        {
            // Abre la conexión a la base de datos
            Con.Open();

            // Query SQL para seleccionar todos los registros de la tabla de pacientes
            string Query = "select * from PatientTbl";

            // SqlDataAdapter actúa como puente entre la base de datos y el DataSet
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);

            // SqlCommandBuilder genera automáticamente comandos INSERT, UPDATE, DELETE
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);

            // DataSet es un contenedor de datos en memoria (como una base de datos temporal)
            var ds = new DataSet();

            // Llena el DataSet con los datos obtenidos de la base de datos
            sda.Fill(ds);

            // Asigna la primera tabla del DataSet como fuente de datos del DataGridView
            // Esto hace que la tabla se muestre en la cuadrícula
            PatientsDGV.DataSource = ds.Tables[0];

            // Cierra la conexión a la base de datos
            Con.Close();
        }

        // Variable global para almacenar el ID (clave primaria) del paciente seleccionado
        // Se usa para saber qué registro actualizar o eliminar
        int key = 0;

        // Evento que se ejecuta cuando se carga el formulario
        // Actualmente vacío, pero se puede usar para inicializaciones adicionales
        private void ListaPacientes_Load(object sender, EventArgs e)
        {
        }

        // Evento que se dispara cuando el usuario hace clic en una celda del DataGridView
        private void PatientsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtiene el nombre del paciente de la celda en la columna 1 (índice 1)
            // SelectedRows[0] = primera fila seleccionada
            // Cells[1] = segunda columna (índice comienza en 0)
            PNameTb.Text = PatientsDGV.SelectedRows[0].Cells[1].Value.ToString();

            // Obtiene la edad del paciente de la celda en la columna 2
            PAgeTb.Text = PatientsDGV.SelectedRows[0].Cells[2].Value.ToString();

            // Obtiene el teléfono del paciente de la celda en la columna 3
            PphoneTb.Text = PatientsDGV.SelectedRows[0].Cells[3].Value.ToString();

            // Obtiene el género del paciente de la celda en la columna 4
            // Usa .Text en lugar de .SelectedItem para que funcione correctamente
            // .Text coloca el valor directamente sin buscar coincidencias exactas en la lista
            PGenCb.Text = PatientsDGV.SelectedRows[0].Cells[4].Value.ToString();

            // Obtiene el grupo sanguíneo del paciente de la celda en la columna 5
            // Usa .Text para evitar problemas de coincidencia exacta con los items del ComboBox
            PBGroupCb.Text = PatientsDGV.SelectedRows[0].Cells[5].Value.ToString();

            // Obtiene la dirección del paciente de la celda en la columna 6
            PAddressTb.Text = PatientsDGV.SelectedRows[0].Cells[6].Value.ToString();

            // Verifica si el campo de nombre está vacío
            if (PNameTb.Text == "")
            {
                // Si está vacío, establece key en 0 (sin paciente seleccionado)
                key = 0;
            }
            else
            {
                // Si hay datos, obtiene el ID del paciente de la celda en la columna 0
                // Este ID se usará para operaciones de actualización o eliminación
                key = Convert.ToInt32(PatientsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}