using System;
using System.Data.SqlClient;
using System.Security.Cryptography; // Necesario para Rfc2898DeriveBytes
using System.Windows.Forms; // Para MessageBox

namespace BBMS.Clases
{
    internal class cAutenticacion
    {
        private cConexion conexionDB = new cConexion();
        public bool ValidarCredenciales(string usuarioId, string contrasenaPlana)
        {
            try
            {
                string storedHash = ObtenerContrasena(usuarioId);
                if (string.IsNullOrEmpty(storedHash))
                    return false;

                // Aquí debe ir la comparación de hash
                return UserAuthService.VerifyPassword(contrasenaPlana, storedHash);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar credenciales: " + ex.Message);
                return false;
            }
        }

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
