using System;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Clases
{
    // Servicio para operaciones relacionadas con donaciones / inventario
    public class DonarService
    {
        private readonly cConexion _cx;

        public DonarService()
        {
            _cx = new cConexion();
        }

        // Devuelve todos los donantes con nombres de columna formales
        public DataTable GetDonors()
        {
            var dt = new DataTable();
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                using (var da = new SqlDataAdapter(
                    @"SELECT Dnum AS Id,
                             DName   AS Nombre,
                             DAge    AS Edad,
                             DGender AS Genero,
                             DPhone  AS Telefono,
                             DAddress AS Direccion,
                             DBGroup AS Grupo
                      FROM DonorTbl", con))
                {
                    con.Open();
                    da.Fill(dt);
                }
            }
            catch
            {
                // devolver tabla vacía en caso de error
            }
            return dt;
        }

        // Devuelve todos los registros de BloodTbl con alias formales
        public DataTable GetBloodStock()
        {
            var dt = new DataTable();
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                using (var da = new SqlDataAdapter(
                    @"SELECT BGroup AS Grupo,
                             BStock AS Stock
                      FROM BloodTbl
                      ORDER BY BGroup", con))
                {
                    con.Open();
                    da.Fill(dt);
                }
            }
            catch
            {
            }
            return dt;
        }

        // Devuelve el stock de un grupo (0 si no existe)
        public int GetStock(string bgroup)
        {
            if (string.IsNullOrWhiteSpace(bgroup)) return 0;
            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                using (var cmd = new SqlCommand("SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = @bg", con))
                {
                    cmd.Parameters.AddWithValue("@bg", bgroup);
                    con.Open();
                    var obj = cmd.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                        return Convert.ToInt32(obj);
                }
            }
            catch
            {
            }
            return 0;
        }

        // Incrementa el stock; si no existe el grupo crea el registro
        public bool IncrementStock(string bgroup, out string error)
        {
            // Sobrecarga que usa el valor por defecto amount = 1
            return IncrementStock(bgroup, 1, out error);
        }
        public bool IncrementStock(string bgroup, int amount, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(bgroup))
            {
                error = "Grupo sanguíneo inválido";
                return false;
            }

            try
            {
                using (SqlConnection con = _cx.ConexionServer())
                {
                    con.Open();
                    using (var cmd = new SqlCommand("UPDATE BloodTbl SET BStock = BStock + @amt WHERE BGroup = @bg", con))
                    {
                        cmd.Parameters.AddWithValue("@amt", amount);
                        cmd.Parameters.AddWithValue("@bg", bgroup);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            using (var ins = new SqlCommand("INSERT INTO BloodTbl (BGroup, BStock) VALUES (@bg, @amt)", con))
                            {
                                ins.Parameters.AddWithValue("@bg", bgroup);
                                ins.Parameters.AddWithValue("@amt", amount);
                                ins.ExecuteNonQuery();
                            }
                        }
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