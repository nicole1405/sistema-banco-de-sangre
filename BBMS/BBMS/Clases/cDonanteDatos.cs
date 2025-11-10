using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBMS.Clases
{
    internal class cDonanteDatos
    {
        // 1. Instanciamos la conexión central
        private cConexion conexionDB = new cConexion();

        /// <summary>
        /// Obtiene una tabla con todos los donantes de la base de datos.
        /// </summary>
        /// <returns>Un DataTable con los donantes.</returns>
        public DataTable ObtenerTodosLosDonantes()
        {
            var dt = new DataTable();
            string query = "SELECT * FROM DonorTbl";
            try
            {
                // Usamos 'using' para asegurar que la conexión se cierre sola
                using (var con = conexionDB.ConexionServer())
                using (var sda = new SqlDataAdapter(query, con))
                {
                    // sda.Fill maneja la apertura y cierre de la conexión
                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de donantes: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Busca donantes cuyo nombre coincida parcialmente con el texto de búsqueda.
        /// </summary>
        /// <param name="nombre">Texto a buscar en el nombre del donante.</param>
        /// <returns>Un DataTable con los donantes filtrados.</returns>
        public DataTable BuscarDonantesPorNombre(string nombre)
        {
            var dt = new DataTable();
            // 2. Query parametrizada para evitar Inyección SQL
            // Usamos LIKE para búsquedas parciales (ej: "Juan" encuentra "Juan Perez")
            string query = "SELECT * FROM DonorTbl WHERE DName LIKE @nombre";

            try
            {
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand(query, con))
                {
                    // 3. Añadimos el parámetro con los comodines '%'
                    cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");

                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar donantes: " + ex.Message);
            }
            return dt;
        }
    }
}
