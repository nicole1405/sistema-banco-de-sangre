using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// 1. Se elimina 'using System.Data.SqlClient;'
using BBMS.Clases; // 2. Importamos la carpeta Clases

namespace BBMS
{
    public partial class Paciente : UserControl
    {
        // 3. Instanciamos la nueva clase de servicio
        private cPacienteServicio gestorPacientes = new cPacienteServicio();

        public Paciente()
        {
            InitializeComponent();
        }

        // 4. ¡Se elimina la variable 'SqlConnection Con' de aquí!

        // Método para limpiar/resetear todos los campos del formulario
        private void Reset()
        {
            PNameTb.Text = "";
            PAgeTb.Text = "";
            PPhoneTb.Text = "";
            PAdressTb.Text = "";
            PGenCb.SelectedIndex = -1;
            PBGroupCb.SelectedIndex = -1;
        }

        // Evento que se dispara cuando cambia el texto en el campo de edad
        private void PAgeTb_TextChanged(object sender, EventArgs e)
        {
        }

        // 5. Evento del botón Guardar (Refactorizado)
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // --- VALIDACIÓN (Esto se queda en el formulario) ---
            if (PNameTb.Text == "" ||
                PPhoneTb.Text == "" ||
                PAgeTb.Text == "" ||
                PGenCb.SelectedIndex == -1 ||
                PBGroupCb.SelectedIndex == -1 ||
                PAdressTb.Text == "")
            {
                MessageBox.Show("Falta Información");
                return;
            }

            // --- (MEJORA) Validación de edad más segura ---
            if (!int.TryParse(PAgeTb.Text, out int edad))
            {
                MessageBox.Show("La edad debe ser un número válido.");
                return;
            }

            // --- LÓGICA DE GUARDADO (Ahora separada) ---
            try
            {
                // 1. Recolectamos los datos de la interfaz
                string nombre = PNameTb.Text;
                string telefono = PPhoneTb.Text;
                string genero = PGenCb.SelectedItem.ToString();
                string grupoSanguineo = PBGroupCb.SelectedItem.ToString();
                string direccion = PAdressTb.Text;

                // 2. Llamamos al gestor para que haga el trabajo
                gestorPacientes.GuardarPaciente(nombre, edad, telefono, genero, grupoSanguineo, direccion);

                // 3. Limpiamos el formulario (la clase 'gestorPacientes' ya mostró el mensaje)
                Reset();
            }
            catch (Exception Ex)
            {
                // Captura errores de la UI (ej. .SelectedItem.ToString() si algo es nulo)
                MessageBox.Show("Error al preparar los datos: " + Ex.Message);
            }
        }
       

      

       

        private void Paciente_Load(object sender, EventArgs e)
        {

        }
       
    }
}