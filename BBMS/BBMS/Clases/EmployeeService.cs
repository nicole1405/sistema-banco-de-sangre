using System;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Clases
{
    // Servicio para operaciones sobre EmployeeTbl
    public class EmployeeService
    {
        private readonly cConexion _cx;

        public EmployeeService()
        {
            _cx = new cConexion();
        }

        // Devuelve empleados con nombres de columnas formales, contraseña enmascarada y rol (si existe)
        public DataTable GetEmployees()
        {
            var dt = new DataTable();
            string sql = @"
                SELECT 
                    e.EmpId AS Id,
                    e.EmpName AS Nombre,
                    CASE WHEN LEN(ISNULL(e.EmpPass,'')) > 0 THEN '********' ELSE '' END AS Contraseña,
                    -- Subconsulta para obtener un rol (si hay varios, toma el primero)
                    (SELECT TOP(1) r.RoleName 
                     FROM EmployeeRoles er 
                     JOIN Roles r ON er.RoleId = r.RoleId
                     WHERE er.EmpId = e.EmpId
                     ORDER BY r.RoleId) AS Rol
                FROM EmployeeTbl e
                ORDER BY e.EmpId";
            using (SqlConnection con = _cx.ConexionServer())
            using (var da = new SqlDataAdapter(sql, con))
            {
                con.Open();
                da.Fill(dt);
            }
            return dt;
        }

        // Inserta empleado (almacena EmpName; EmpId es identity) y asigna rol.
        // Devuelve el nuevo EmpId en out newEmpId.
        public bool AddEmployee(string empName, string passwordHash, string roleName, out int newEmpId, out string error)
        {
            error = null;
            newEmpId = 0;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                {
                    con.Open();
                    using (var tran = con.BeginTransaction())
                    {
                        try
                        {
                            // 1) Insertar empleado y obtener id
                            string insertEmp = "INSERT INTO EmployeeTbl (EmpName, EmpPass) VALUES (@EmpName, @EmpPass); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                            using (var cmd = new SqlCommand(insertEmp, con, tran))
                            {
                                cmd.Parameters.AddWithValue("@EmpName", empName ?? string.Empty);
                                cmd.Parameters.AddWithValue("@EmpPass", passwordHash ?? string.Empty);
                                var obj = cmd.ExecuteScalar();
                                if (obj == null || obj == DBNull.Value)
                                    throw new Exception("No se pudo obtener el Id del empleado insertado.");
                                newEmpId = Convert.ToInt32(obj);
                            }

                            // 2) Obtener RoleId (crear rol si no existe)
                            int roleId = 0;
                            using (var cmdRole = new SqlCommand("SELECT RoleId FROM Roles WHERE RoleName = @RoleName", con, tran))
                            {
                                cmdRole.Parameters.AddWithValue("@RoleName", roleName ?? string.Empty);
                                var r = cmdRole.ExecuteScalar();
                                if (r != null && r != DBNull.Value)
                                    roleId = Convert.ToInt32(r);
                            }

                            if (roleId == 0)
                            {
                                // Crear rol si no existe
                                using (var cmdInsertRole = new SqlCommand("INSERT INTO Roles (RoleName) VALUES (@RoleName); SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tran))
                                {
                                    cmdInsertRole.Parameters.AddWithValue("@RoleName", roleName ?? string.Empty);
                                    var r2 = cmdInsertRole.ExecuteScalar();
                                    if (r2 != null && r2 != DBNull.Value)
                                        roleId = Convert.ToInt32(r2);
                                }
                            }

                            // 3) Insertar en EmployeeRoles
                            using (var cmdER = new SqlCommand("INSERT INTO EmployeeRoles (EmpId, RoleId) VALUES (@EmpId, @RoleId)", con, tran))
                            {
                                cmdER.Parameters.AddWithValue("@EmpId", newEmpId);
                                cmdER.Parameters.AddWithValue("@RoleId", roleId);
                                cmdER.ExecuteNonQuery();
                            }

                            tran.Commit();
                            return true;
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                newEmpId = 0;
                return false;
            }
        }

        // Actualiza empleado; si newPasswordHash es null o vacío no cambia la contraseña
        public bool UpdateEmployee(int empId, string empName, string newPasswordHash, out string error)
        {
            error = null;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                {
                    con.Open();
                    if (!string.IsNullOrWhiteSpace(newPasswordHash))
                    {
                        string sql = "UPDATE EmployeeTbl SET EmpName = @EmpName, EmpPass = @EmpPass WHERE EmpId = @EmpId";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpName", empName);
                            cmd.Parameters.AddWithValue("@EmpPass", newPasswordHash);
                            cmd.Parameters.AddWithValue("@EmpId", empId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                    else
                    {
                        string sql = "UPDATE EmployeeTbl SET EmpName = @EmpName WHERE EmpId = @EmpId";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@EmpName", empName);
                            cmd.Parameters.AddWithValue("@EmpId", empId);
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

        // Elimina empleado por Id (borrado físico).
        // Ahora se apoya en la FK con ON DELETE CASCADE en la BD para eliminar dependencias en EmployeeRoles.
        public bool DeleteEmployee(int empId, out string error)
        {
            error = null;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmdDelEmp = new SqlCommand("DELETE FROM EmployeeTbl WHERE EmpId = @EmpId", con))
                {
                    cmdDelEmp.Parameters.AddWithValue("@EmpId", empId);
                    con.Open();
                    int affected = cmdDelEmp.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        error = "Empleado no encontrado.";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}