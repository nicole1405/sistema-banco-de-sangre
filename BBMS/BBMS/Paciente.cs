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
    public partial class Paciente : Form
    {
        // Constructor del formulario - se ejecuta al crear la ventana
        public Paciente()
        {
            InitializeComponent();
        }

        // Objeto de conexión a la base de datos SQL Server LocalDB
        // Contiene la ruta del archivo de base de datos y configuración de seguridad
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\BancoDeSangreDB.mdf;Integrated Security=True;Connect Timeout=30");

        // Método para limpiar/resetear todos los campos del formulario
        private void Reset()
        {
            PNameTb.Text = "";              // Limpia el campo de nombre
            PAgeTb.Text = "";               // Limpia el campo de edad
            PPhoneTb.Text = "";             // Limpia el campo de teléfono
            PAdressTb.Text = "";            // Limpia el campo de dirección
            PGenCb.SelectedIndex = -1;      // Resetea el ComboBox de género (sin selección)
            PBGroupCb.SelectedIndex = -1;   // Resetea el ComboBox de grupo sanguíneo (sin selección)
        }

        // Evento que se dispara cuando cambia el texto en el campo de edad
        // Actualmente vacío, pero se puede usar para validaciones en tiempo real
        private void PAgeTb_TextChanged(object sender, EventArgs e)
        {
        }

        // Evento del botón Guardar - se ejecuta al hacer clic en el botón
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN: Verificar que todos los campos estén completos antes de guardar
            // Si algún campo está vacío o sin selección, muestra mensaje y sale del método
            if (PNameTb.Text == "" ||                    // Si el nombre está vacío
                PPhoneTb.Text == "" ||                   // O el teléfono está vacío
                PAgeTb.Text == "" ||                     // O la edad está vacía
                PGenCb.SelectedIndex == -1 ||            // O no se seleccionó género (-1 = sin selección)
                PBGroupCb.SelectedIndex == -1 ||         // O no se seleccionó grupo sanguíneo
                PAdressTb.Text == "")                    // O la dirección está vacía
            {
                MessageBox.Show("Falta Información");    // Muestra mensaje de error
                return;                                   // Sale del método sin guardar
            }

            // Si pasa la validación, intenta guardar los datos en la base de datos
            try
            {
                // Query SQL con parámetros (@Name, @Age, etc.) para mayor seguridad
                // Esto previene ataques de SQL Injection
                string query = "INSERT INTO PatientTbl VALUES (@Name, @Age, @Phone, @Gender, @BloodGroup, @Address)";

                // Abre la conexión a la base de datos
                Con.Open();

                // Crea el comando SQL con la query y la conexión
                SqlCommand cmd = new SqlCommand(query, Con);

                // Agrega los parámetros al comando SQL con los valores de los campos del formulario
                cmd.Parameters.AddWithValue("@Name", PNameTb.Text);                          // Parámetro nombre
                cmd.Parameters.AddWithValue("@Age", int.Parse(PAgeTb.Text));                 // Parámetro edad (convertido a entero)
                cmd.Parameters.AddWithValue("@Phone", PPhoneTb.Text);                        // Parámetro teléfono
                cmd.Parameters.AddWithValue("@Gender", PGenCb.SelectedItem.ToString());      // Parámetro género (del ComboBox)
                cmd.Parameters.AddWithValue("@BloodGroup", PBGroupCb.SelectedItem.ToString()); // Parámetro grupo sanguíneo
                cmd.Parameters.AddWithValue("@Address", PAdressTb.Text);                     // Parámetro dirección

                // Ejecuta el comando INSERT en la base de datos
                cmd.ExecuteNonQuery();

                // Muestra mensaje de éxito
                MessageBox.Show("Paciente guardado con éxito");

                // Cierra la conexión a la base de datos
                Con.Close();

                // Limpia todos los campos del formulario para un nuevo registro
                Reset();
            }
            catch (Exception Ex)
            {
                // Si ocurre cualquier error (ej: error de conexión, formato incorrecto, etc.)
                // Muestra el mensaje de error
                MessageBox.Show(Ex.Message);

                // Verifica si la conexión quedó abierta y la cierra para evitar problemas
                if (Con.State == System.Data.ConnectionState.Open)
                    Con.Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ListaPacientes VP = new ListaPacientes();
            VP.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Donante Ob = new Donante();
            Ob.Show();
            this.Hide();
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