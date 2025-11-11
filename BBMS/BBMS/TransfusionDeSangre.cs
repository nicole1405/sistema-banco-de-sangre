using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
// using System.Data.SqlClient; // <-- 1. REMOVIDO
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BBMS.Clases; // <-- 2. AÑADIDO

namespace BBMS
{
    public partial class TransfusionDeSangre : UserControl
    {
        // 3. Instanciar la nueva clase de lógica para transfusiones.
        private cTransfusionDatos gestorTransfusion = new cTransfusionDatos();

        // 4. Variable para el stock actual del grupo sanguíneo.
        int stock = 0;

        // 5. Constructor: inicializa el formulario.
        public TransfusionDeSangre()
        {
            InitializeComponent();
        }

        // 6. Evento de carga del formulario: llena el ComboBox y resetea la UI.
        private void TransfusionDeSangre_Load(object sender, EventArgs e)
        {
            try
            {
                fillPatientCb();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar datos: " + ex.Message);
            }
        }

        // 7. Llena el ComboBox con los IDs de pacientes.
        private void fillPatientCb()
        {
            try
            {
                PatientIdCb.ValueMember = "PNum";
                PatientIdCb.DisplayMember = "PNum";
                PatientIdCb.DataSource = gestorTransfusion.ObtenerIdsPacientes();
                PatientIdCb.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pacientes: " + ex.Message);
            }
        }

        // 8. Obtiene los datos del paciente seleccionado y los muestra en la UI.
        private void GetData()
        {
            if (PatientIdCb.SelectedValue == null) return;

            try
            {
                int pacienteId = Convert.ToInt32(PatientIdCb.SelectedValue);
                PacienteTransfusionInfo info = gestorTransfusion.ObtenerDetallesPaciente(pacienteId);

                PatNameTb.Text = info.Nombre;
                BloodGroup.Text = info.GrupoSanguineo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos del paciente: " + ex.Message);
            }
        }

        // 9. Obtiene el stock actual para el grupo sanguíneo seleccionado.
        private void GetStock(string Bgroup)
        {
            stock = 0;
            if (string.IsNullOrWhiteSpace(Bgroup)) return;

            try
            {
                stock = gestorTransfusion.ObtenerStock(Bgroup);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener stock: " + ex.Message);
            }
        }

        // 10. Evento cuando cambia el valor seleccionado en el ComboBox de pacientes.
        private void PatientIdCb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PatientIdCb.SelectedValue == null)
            {
                Reset();
                return;
            }

            GetData();
            GetStock(BloodGroup.Text);

            // 11. Actualiza la UI según el stock disponible.
            if (stock > 0)
            {
                TransferBtn.Visible = true;
                AvarlableLbl.Text = "Stock Disponible (" + stock + " unidades)";
                AvarlableLbl.Visible = true;
            }
            else
            {
                TransferBtn.Visible = false;
                AvarlableLbl.Text = "Stock No Disponible";
                AvarlableLbl.Visible = true;
            }
        }

        // 12. Limpia los campos y oculta controles de la UI.
        private void Reset()
        {
            PatNameTb.Text = "";
            BloodGroup.Text = "";
            AvarlableLbl.Visible = false;
            TransferBtn.Visible = false;
        }

        // 13. Evento click del botón Transferir: realiza la transfusión.
        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatNameTb.Text))
            {
                MessageBox.Show("Información Faltante. Seleccione un paciente.");
                return;
            }

            try
            {
                bool exito = gestorTransfusion.RealizarTransfusion(PatNameTb.Text, BloodGroup.Text);

                if (exito)
                {
                    MessageBox.Show("Transfusión Exitosa");
                    Reset();
                    PatientIdCb.SelectedIndex = -1;
                }
                // Si exito == false, el gestor ya mostró el error
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al procesar la transferencia: " + Ex.Message);
            }
        }
    }
}