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
        // 3. Instanciar la nueva clase de lógica
        private cTransfusionDatos gestorTransfusion = new cTransfusionDatos();

        // 4. 'connStr' REMOVIDA

        // Mantenemos 'stock' como variable de estado de la UI
        int stock = 0;

        public TransfusionDeSangre()
        {
            InitializeComponent();
        }

        private void TransfusionDeSangre_Load(object sender, EventArgs e)
        {
            try
            {
                fillPatientCb();
                // Asegurarse de que el estado inicial esté limpio
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar datos: " + ex.Message);
            }
        }

        private void fillPatientCb()
        {
            try
            {
                // 5. Lógica de BD movida al gestor
                PatientIdCb.ValueMember = "PNum";
                PatientIdCb.DisplayMember = "PNum";
                PatientIdCb.DataSource = gestorTransfusion.ObtenerIdsPacientes();
                PatientIdCb.SelectedIndex = -1; // Empezar sin selección
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pacientes: " + ex.Message);
            }
        }

        private void GetData()
        {
            if (PatientIdCb.SelectedValue == null) return;

            try
            {
                // 6. Lógica de BD movida al gestor
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

        private void GetStock(string Bgroup)
        {
            stock = 0; // Reiniciar
            if (string.IsNullOrWhiteSpace(Bgroup)) return;

            try
            {
                // 7. Lógica de BD movida al gestor
                stock = gestorTransfusion.ObtenerStock(Bgroup);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener stock: " + ex.Message);
            }
        }

        private void PatientIdCb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PatientIdCb.SelectedValue == null)
            {
                Reset(); // Limpiar si no hay nada seleccionado
                return;
            }

            GetData(); // Obtiene nombre y grupo
            GetStock(BloodGroup.Text); // Obtiene stock para ese grupo

            // Lógica de UI (esto se queda en el formulario)
            if (stock > 0)
            {
                TransferBtn.Visible = true;
                AvarlableLbl.Text = "Stock Disponible (" + stock + " unidades)"; // Más informativo
                AvarlableLbl.Visible = true;
            }
            else
            {
                TransferBtn.Visible = false;
                AvarlableLbl.Text = "Stock No Disponible";
                AvarlableLbl.Visible = true;
            }
        }

        private void Reset()
        {
            PatNameTb.Text = "";
            BloodGroup.Text = "";
            AvarlableLbl.Visible = false;
            TransferBtn.Visible = false;
            // Opcional: deseleccionar el ComboBox
            // PatientIdCb.SelectedIndex = -1; 
        }

        // 8. El método 'updateStock' se ELIMINA.
        // Su lógica ahora está dentro de 'RealizarTransfusion'

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatNameTb.Text))
            {
                MessageBox.Show("Información Faltante. Seleccione un paciente.");
                return;
            }

            try
            {
                // 9. Lógica de Transacción movida al gestor
                // Esta única llamada hace la verificación, inserción y actualización
                // de forma segura.
                bool exito = gestorTransfusion.RealizarTransfusion(PatNameTb.Text, BloodGroup.Text);

                if (exito)
                {
                    MessageBox.Show("Transfusión Exitosa");
                    Reset();
                    // Limpiamos la selección para forzar al usuario a elegir de nuevo
                    PatientIdCb.SelectedIndex = -1;
                }
                // Si exito == false, el gestor ya mostró el error (ej. "Stock agotado")
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al procesar la transferencia: " + Ex.Message);
            }
        }

      
       
    }
}