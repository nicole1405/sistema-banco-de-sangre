using System;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Clases
{
    // Servicio para operaciones del inventario de sangre
    public class InventarioService
    {
        private readonly cConexion _cx;

        public InventarioService()
        {
            _cx = new cConexion();
        }

        // Devuelve todas las filas de BloodTbl con alias formales
        public DataTable GetBloodStock()
        {
            var dt = new DataTable();
            string sql = @"SELECT BGroup AS Grupo, BStock AS Stock FROM BloodTbl ORDER BY BGroup";
            using (SqlConnection con = _cx.ConexionServer())
            using (var da = new SqlDataAdapter(sql, con))
            {
                con.Open();
                da.Fill(dt);
            }
            return dt;
        }

        // Devuelve los grupos únicos
        public DataTable GetGroups()
        {
            var dt = new DataTable();
            string sql = @"SELECT DISTINCT BGroup AS Grupo FROM BloodTbl ORDER BY BGroup";
            using (SqlConnection con = _cx.ConexionServer())
            using (var da = new SqlDataAdapter(sql, con))
            {
                con.Open();
                da.Fill(dt);
            }
            return dt;
        }

        // Devuelve el stock total (suma)
        public int GetTotalStock()
        {
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmd = new SqlCommand("SELECT ISNULL(SUM(BStock), 0) FROM BloodTbl", con))
                {
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return 0;
            }
        }

        // Devuelve el stock de un grupo concreto (0 si no existe)
        public int GetStockByGroup(string group)
        {
            if (string.IsNullOrWhiteSpace(group)) return 0;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmd = new SqlCommand("SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = @bg", con))
                {
                    cmd.Parameters.AddWithValue("@bg", group);
                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}