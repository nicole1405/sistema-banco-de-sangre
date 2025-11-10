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
        SqlConnection Con = new SqlConnection(@"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        // Lo llamamos luego de guardar un donante, con esto reseteamos todos los campos del formulario
        private void Reset()
        {
            DNameTb.Text = ""; // limpia el campo para el Name
            DAgeTb.Text = ""; // limpia el campo para Age
            DPhoneTb.Text = ""; // limpia el campo para el Phone
            DAddressTb.Text = ""; // limpia el campo para direccion
            DGenCb.SelectedIndex = -1; // Deselecciona el Combox de Genero
            DBGroupCb.SelectedIndex = -1; // Lo mismo que el anterior pero el de Grupo de sangre
        }

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

                // Limpia si el guardado fue exitoso
                Reset();
            }
            catch (Exception Ex)
            {
                // Si algo sale mal (error de BD, formato incorrecto, etc.)
                // mostramos el mensaje de error
                MessageBox.Show(Ex.Message);
            }
        }

        // Evento click de un label
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void DNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void DPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {
            Donar Ob = new Donar();
            Ob.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Verdonantes Ob = new Verdonantes();
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
            PanelPrincipal Ob = new PanelPrincipal();
            Ob.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Ob = new Login();
            Ob.Show();
            this.Hide();
        }
    }
}