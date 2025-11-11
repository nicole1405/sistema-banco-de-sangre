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
        // 2. Instanciamos la nueva clase de servicio para pacientes.
        private cPacienteServicio gestorPacientes = new cPacienteServicio();

        // 3. Constructor: inicializa el formulario.
        public Paciente()
        {
            InitializeComponent();
        }

        // 4. Limpia todos los campos del formulario.
        private void Reset()
        {
            PNameTb.Text = "";
            PAgeTb.Text = "";
            PPhoneTb.Text = "";
            PAdressTb.Text = "";
            PGenCb.SelectedIndex = -1;
            PBGroupCb.SelectedIndex = -1;
        }

        // 5. Evento cuando cambia el texto en el campo de edad (sin lógica).
        private void PAgeTb_TextChanged(object sender, EventArgs e)
        {
        }

        // 6. Evento click del botón Guardar (Guna2Button): valida y guarda el paciente.
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // 7. Validación de campos obligatorios.
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

            // 8. Validación de edad.
            if (!int.TryParse(PAgeTb.Text, out int edad))
            {
                MessageBox.Show("La edad debe ser un número válido.");
                return;
            }

            // 9. Lógica de guardado.
            try
            {
                string nombre = PNameTb.Text;
                string telefono = PPhoneTb.Text;
                string genero = PGenCb.SelectedItem.ToString();
                string grupoSanguineo = PBGroupCb.SelectedItem.ToString();
                string direccion = PAdressTb.Text;

                gestorPacientes.GuardarPaciente(nombre, edad, telefono, genero, grupoSanguineo, direccion);

                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al preparar los datos: " + Ex.Message);
            }
        }

        // 10. Evento de carga del formulario (sin lógica).
        private void Paciente_Load(object sender, EventArgs e)
        {

        }
    }
}