using System;
using System.Data.SqlClient;
using System.Security.Cryptography; // Necesario para Rfc2898DeriveBytes
using System.Windows.Forms;

namespace BBMS.Clases
{
    internal class cAutenticacion
    {
        private cConexion conexionDB = new cConexion();

        // ahora recibe el nombre de usuario (EmpName) tal como lo introduce el usuario en UI
        public bool ValidarCredenciales(string usuarioNombre, string contrasenaPlana)
        {
            try
            {
                string storedHash = ObtenerContrasenaPorNombre(usuarioNombre);
                if (string.IsNullOrEmpty(storedHash))
                    return false;

                bool ok = UserAuthService.VerifyPassword(contrasenaPlana, storedHash);
                if (!ok) return false;

                // Obtener id (entero) del usuario por su nombre
                int userId = ObtenerIdPorNombre(usuarioNombre);
                string rol = ObtenerRolUsuarioPorId(userId);

                UserSession.Current = new UserSession
                {
                    EmpId = userId,
                    Role = string.IsNullOrEmpty(rol) ? "Usuario" : rol,
                    EmpName = usuarioNombre
                };

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar credenciales: " + ex.Message);
                return false;
            }
        }

        private string ObtenerContrasenaPorNombre(string usuarioNombre)
        {
            string pass = null;
            string query = "SELECT EmpPass FROM EmployeeTbl WHERE EmpName = @name";

            using (var con = conexionDB.ConexionServer())
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", usuarioNombre);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    pass = result.ToString();
            }
            return pass;
        }

        private int ObtenerIdPorNombre(string usuarioNombre)
        {
            int id = 0;
            string query = "SELECT EmpId FROM EmployeeTbl WHERE EmpName = @name";

            using (var con = conexionDB.ConexionServer())
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", usuarioNombre);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    int.TryParse(result.ToString(), out id);
            }
            return id;
        }

        private string ObtenerRolUsuarioPorId(int usuarioId)
        {
            if (usuarioId == 0) return null;

            string rol = null;
            string query = @"
                SELECT TOP(1) r.RoleName
                FROM EmployeeRoles er
                INNER JOIN Roles r ON er.RoleId = r.RoleId
                WHERE er.EmpId = @id
                ORDER BY r.RoleId";

            try
            {
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", usuarioId);
                    con.Open();
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                        rol = res.ToString();
                }
            }
            catch (Exception ex)
            {
                // No bloqueamos el login por fallo al leer rol; poner log si se desea
                MessageBox.Show("Advertencia al obtener rol del usuario: " + ex.Message);
            }

            return rol;
        }
    }
}
