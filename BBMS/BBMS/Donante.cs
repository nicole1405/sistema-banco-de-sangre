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
    public partial class Donante : Form
    {
        // Constructor del formulario, esto se ejecuta al abrir la ventana
        public Donante()
        {
            InitializeComponent();
        }

        // Conexión a la base de datos local
        // LocalDB es la versión ligera de SQL Server que viene con Visual Studio
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BancoDeSangreDB.mdf;Integrated Security=True;Connect Timeout=30");

        // Evento que se ejecuta cuando le damos click al botón guardar
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Validación: verificamos que todos los campos estén llenos antes de guardar
            // SelectedIndex == -1 significa que no se ha seleccionado nada en el ComboBox
            if (DNameTb.Text == "" || DPhoneTb.Text == "" || DAgeTb.Text == "" || DGenCb.SelectedIndex == -1 || DBGroupCb.SelectedIndex == -1)
            {
                // Si falta algo, mostramos un mensaje y no continuamos
                MessageBox.Show("Falta Información");
                return; // Salimos del método sin guardar nada
            }

            // Try-catch para manejar posibles errores al guardar en la BD
            try
            {
                // Creamos la query SQL para insertar los datos
                // Concatenamos los valores de los TextBox y ComboBox
                string query = "insert into DonorTbl values('" + DNameTb.Text + "'," + DAgeTb.Text + ",'" + DGenCb.SelectedItem.ToString() + "','" + DPhoneTb.Text + "','" + DAddressTb.Text + "','" + DBGroupCb.SelectedItem.ToString() + "')";

                // Abrimos la conexión a la base de datos
                Con.Open();

                // Creamos el comando SQL con nuestra query y la conexión
                SqlCommand cmd = new SqlCommand(query, Con);

                // ExecuteNonQuery() ejecuta el INSERT y devuelve el número de filas afectadas
                cmd.ExecuteNonQuery();

                // Si todo salió bien, mostramos mensaje de éxito
                MessageBox.Show("Donante guardado con éxito");

                // Cerramos la conexión para liberar recursos
                Con.Close();
            }
            catch (Exception Ex)
            {
                // Si algo sale mal (error de BD, formato incorrecto, etc.)
                // mostramos el mensaje de error
                MessageBox.Show(Ex.Message);
            }
        }

        // Evento click de un label, por el momento no lo ocupamos
        private void label12_Click(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se carga el formulario
        private void Donante_Load(object sender, EventArgs e)
        {
            // Aquí podrías cargar datos iniciales si lo necesitas
        }

       
        // Lo agregamos para evitar el error
        private void DAgeTb_TextChanged(object sender, EventArgs e)
        {
            // lo dejamos vacío
        }
    }
}