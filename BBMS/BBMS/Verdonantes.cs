using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// 1. REMOVIDO: using System.Data.SqlClient;
using BBMS.Clases; // 2. AÑADIDO

namespace BBMS
{
    public partial class Verdonantes : UserControl
    {
        // 3. Instanciamos la nueva clase de lógica
        private cDonanteDatos gestorDonantes = new cDonanteDatos();

        public Verdonantes()
        {
            InitializeComponent();
            populate(); // Cargar datos iniciales

            // 4. Renombrar columnas después de cargar datos
            ConfigurarNombresColumnas();
        }

        // 5. REMOVIDA: La variable 'SqlConnection Con'

        /// <summary>
        /// Carga todos los donantes en el DataGridView.
        /// </summary>
        private void populate()
        {
            try
            {
                // 6. Lógica de BD movida al gestor
                DonorsDGV.DataSource = gestorDonantes.ObtenerTodosLosDonantes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al popular la tabla: " + ex.Message);
            }
        }

        /// <summary>
        /// Asigna nombres formales a las cabeceras de las columnas.
        /// </summary>
        private void ConfigurarNombresColumnas()
        {
            // Asumo los nombres de columna de tu BD (ej. DNum, DName)
            // Ajústalos si se llaman diferente.
            try
            {
                if (DonorsDGV.Columns.Contains("DNum"))
                    DonorsDGV.Columns["DNum"].HeaderText = "ID Donante";

                if (DonorsDGV.Columns.Contains("DName"))
                    DonorsDGV.Columns["DName"].HeaderText = "Nombre Completo";

                if (DonorsDGV.Columns.Contains("DAge"))
                    DonorsDGV.Columns["DAge"].HeaderText = "Edad";

                if (DonorsDGV.Columns.Contains("DGender"))
                    DonorsDGV.Columns["DGender"].HeaderText = "Género";

                if (DonorsDGV.Columns.Contains("DPhone"))
                    DonorsDGV.Columns["DPhone"].HeaderText = "Teléfono";

                if (DonorsDGV.Columns.Contains("DBGroup"))
                    DonorsDGV.Columns["DBGroup"].HeaderText = "Grupo Sanguíneo";

                if (DonorsDGV.Columns.Contains("DAddress"))
                    DonorsDGV.Columns["DAddress"].HeaderText = "Dirección";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar columnas: " + ex.Message);
            }
        }

        // 7. ¡ARREGLO DE LA BÚSQUEDA!
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = guna2TextBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Si la barra está vacía, muestra todos los donantes
                populate();
            }
            else
            {
                // Si hay texto, llama al método de búsqueda
                DonorsDGV.DataSource = gestorDonantes.BuscarDonantesPorNombre(textoBusqueda);
            }

            // Es posible que necesites re-aplicar los nombres si el DataSource los borra
            // ConfigurarNombresColumnas(); 
        }


        // --- (Eventos vacíos y de navegación) ---

        private void Verdonantes_Load(object sender, EventArgs e)
        {
            // ya llamamos a populate() en el constructor
        }

        private void label10_Click(object sender, EventArgs e) { }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

       
    }
}