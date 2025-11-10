using System;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Clases
{
    // Servicio para operaciones sobre EmployeeTbl (sin encriptación de contraseñas)
    public class EmployeeService
    {
        private readonly cConexion _cx;

        public EmployeeService()
        {
            _cx = new cConexion();
        }

        // Devuelve empleados con nombres de columnas formales y contraseña enmascarada
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

        // Inserta empleado (contraseña en texto plano)
        public bool AddEmployee(string empId, string plainPassword, out string error)
        {
            error = null;
            try
            {
                string sql = "INSERT INTO EmployeeTbl (EmpId, EmpPass) VALUES (@EmpId, @EmpPass)";
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    cmd.Parameters.AddWithValue("@EmpPass", plainPassword ?? string.Empty);
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

        // Actualiza empleado; si newPlainPassword es null o vacío no cambia la contraseña
        public bool UpdateEmployee(int empNum, string empId, string newPlainPassword, out string error)
        {
            error = null;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                {
                    con.Open();
                    if (!string.IsNullOrWhiteSpace(newPlainPassword))
                    {
                        string sql = "UPDATE EmployeeTbl SET EmpId = @EmpId, EmpPass = @EmpPass WHERE EmpNum = @EmpNum";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", empId);
                            cmd.Parameters.AddWithValue("@EmpPass", newPlainPassword);
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

        // Elimina empleado por Id (borrado físico)
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