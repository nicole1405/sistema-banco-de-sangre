using System;
using System.Data.SqlClient;
using System.Security.Cryptography; // Necesario para Rfc2898DeriveBytes
using System.Windows.Forms; // Para MessageBox

namespace BBMS.Clases
{
    // 3. Clase para autenticación de usuarios.
    internal class cAutenticacion
    {
        // 4. Instancia la conexión a la base de datos.
        private cConexion conexionDB = new cConexion();

        // 5. Valida las credenciales del usuario usando el hash almacenado.
        public bool ValidarCredenciales(string usuarioId, string contrasenaPlana)
        {
            try
            {
                string storedHash = ObtenerContrasena(usuarioId);
                if (string.IsNullOrEmpty(storedHash))
                    return false;

                // 6. Compara el hash generado con el almacenado.
                return UserAuthService.VerifyPassword(contrasenaPlana, storedHash);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar credenciales: " + ex.Message);
                return false;
            }
        }

        // 7. Obtiene el hash de la contraseña almacenada en la base de datos.
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
