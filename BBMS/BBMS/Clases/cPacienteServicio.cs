using System;
using System.Data.SqlClient;
using System.Windows.Forms; // Para el MessageBox

namespace BBMS.Clases
{
    internal class cPacienteServicio
    {
        // 1. Instanciamos la clase de conexión
        private cConexion conexionDB = new cConexion();

        /// <summary>
        /// Guarda un nuevo paciente en la base de datos.
        /// Muestra un mensaje de éxito o error.
        /// </summary>
        public void GuardarPaciente(string nombre, int edad, string telefono, string genero, string grupoSanguineo, string direccion)
        {
            try
            {
                // 2. La consulta SQL parametrizada
                string query = "INSERT INTO PatientTbl VALUES (@Name, @Age, @Phone, @Gender, @BloodGroup, @Address)";

                // 3. Usamos el método de nuestra clase cConexion
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand(query, con))
                {
                    // Asignamos los parámetros
                    cmd.Parameters.AddWithValue("@Name", nombre);
                    cmd.Parameters.AddWithValue("@Age", edad);
                    cmd.Parameters.AddWithValue("@Phone", telefono);
                    cmd.Parameters.AddWithValue("@Gender", genero);
                    cmd.Parameters.AddWithValue("@BloodGroup", grupoSanguineo);
                    cmd.Parameters.AddWithValue("@Address", direccion);

                    con.Open();
                    cmd.ExecuteNonQuery(); // Ejecutamos la inserción

                    MessageBox.Show("Paciente guardado con éxito");
                }
            }
            catch (Exception ex)
            {
                // Mostramos el error si algo falla
                MessageBox.Show("Error al guardar el paciente: " + ex.Message);
            }
        }
    }
}