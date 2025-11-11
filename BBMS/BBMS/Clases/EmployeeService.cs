using System;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Clases
{
    // 1. Servicio para operaciones sobre la tabla EmployeeTbl.
    public class EmployeeService
    {
        private readonly cConexion _cx;

        // 2. Constructor: inicializa la conexión.
        public EmployeeService()
        {
            _cx = new cConexion();
        }

        // 3. Devuelve empleados con nombres de columnas formales y contraseña enmascarada.
        public DataTable GetEmployees()
        {
            var dt = new DataTable();
            string sql = @"
                    SELECT EmpNum AS Id,
                           EmpId  AS Nombre,
                           CASE WHEN LEN(ISNULL(EmpPass,'')) > 0 THEN '********' ELSE '' END AS Contraseña
                    FROM EmployeeTbl
                    ORDER BY EmpNum";
            using (SqlConnection con = _cx.ConexionServer())
            using (var da = new SqlDataAdapter(sql, con))
            {
                con.Open();
                da.Fill(dt);
            }
            return dt;
        }

        // 4. Inserta empleado con contraseña hasheada.
        public bool AddEmployee(string empId, string passwordHash, out string error)
        {
            error = null;
            try
            {
                string sql = "INSERT INTO EmployeeTbl (EmpId, EmpPass) VALUES (@EmpId, @EmpPass)";
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    cmd.Parameters.AddWithValue("@EmpPass", passwordHash ?? string.Empty);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // 5. Actualiza empleado; si newPasswordHash es null o vacío no cambia la contraseña.
        public bool UpdateEmployee(int empNum, string empId, string newPasswordHash, out string error)
        {
            error = null;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                {
                    con.Open();
                    if (!string.IsNullOrWhiteSpace(newPasswordHash))
                    {
                        string sql = "UPDATE EmployeeTbl SET EmpId = @EmpId, EmpPass = @EmpPass WHERE EmpNum = @EmpNum";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", empId);
                            cmd.Parameters.AddWithValue("@EmpPass", newPasswordHash);
                            cmd.Parameters.AddWithValue("@EmpNum", empNum);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                    else
                    {
                        string sql = "UPDATE EmployeeTbl SET EmpId = @EmpId WHERE EmpNum = @EmpNum";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", empId);
                            cmd.Parameters.AddWithValue("@EmpNum", empNum);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // 6. Elimina empleado por Id (borrado físico).
        public bool DeleteEmployee(int empNum, out string error)
        {
            error = null;
            try
            {
                string sql = "DELETE FROM EmployeeTbl WHERE EmpNum = @EmpNum";
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmpNum", empNum);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}