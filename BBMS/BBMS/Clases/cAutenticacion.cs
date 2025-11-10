using System;
using System.Data.SqlClient;
using System.Security.Cryptography; // Necesario para Rfc2898DeriveBytes
using System.Windows.Forms; // Para MessageBox

namespace BBMS.Clases
{
    internal class cAutenticacion
    {
        // 1. Instanciamos la conexión central
        private cConexion conexionDB = new cConexion();

        /// <summary>
        /// Valida las credenciales del usuario contra la base de datos
        /// usando COMPARACIÓN DIRECTA DE TEXTO (sin encriptación).
        /// </summary>
        /// <param name="usuarioId">ID del empleado (EmpId)</param>
        /// <param name="contrasenaPlana">La contraseña tal como la escribió el usuario</param>
        /// <returns>True si las credenciales son válidas, False si no.</returns>
        public bool ValidarCredenciales(string usuarioId, string contrasenaPlana)
        {
            try
            {
                // 1. Obtener la contraseña almacenada en la base de datos
                string contrasenaAlmacenada = ObtenerContrasena(usuarioId);

                if (string.IsNullOrEmpty(contrasenaAlmacenada))
                {
                    // Usuario no encontrado
                    return false;
                }

                // 2. ¡CORRECCIÓN!
                // Se realiza la comparación directa de texto plano.
                return contrasenaAlmacenada == contrasenaPlana;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar credenciales: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método privado para obtener la contraseña (como texto) de la BD.
        /// </summary>
        private string ObtenerContrasena(string usuarioId)
        {
            string pass = null;
            string query = "SELECT EmpPass FROM EmployeeTbl WHERE EmpId = @id";

            using (var con = conexionDB.ConexionServer())
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", usuarioId);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    pass = result.ToString();
            }
            return pass;
        }
    }
}
