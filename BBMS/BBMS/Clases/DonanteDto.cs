using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBMS.Clases
{
    // DTO simple para pasar datos entre UI y capa de datos
    public class DonanteDto
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string GrupoSangre { get; set; }
    }

    // Servicio que encapsula operaciones sobre DonorTbl
    public class DonanteService
    {
        private readonly cConexion cx;

        public DonanteService()
        {
            cx = new cConexion();
        }

        // Inserta un donante; devuelve true si se insertó correctamente.
        // En caso de error devuelve false y out errorMessage contiene la causa.
        public bool Insert(DonanteDto donante, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (SqlConnection conn = cx.ConexionServer())
                using (var cmd = new SqlCommand(@"INSERT INTO DonorTbl
                    (DName, DAge, DGender, DPhone, DAddress, DBGroup)
                    VALUES (@name, @age, @gender, @phone, @address, @bgroup)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", donante.Nombre ?? string.Empty);
                    cmd.Parameters.AddWithValue("@age", donante.Edad);
                    cmd.Parameters.AddWithValue("@gender", donante.Genero ?? string.Empty);
                    cmd.Parameters.AddWithValue("@phone", donante.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", donante.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@bgroup", donante.GrupoSangre ?? string.Empty);

                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                    {
                        try
                        {
                            NotificationService.Create("Nuevo Donante", $"Donante registrado: {donante.Nombre}", "Baja");
                        }
                        catch { }
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        // Opcional: obtener todos los donantes (activo o no según tu BD)
        public DataTable GetAll()
        {
            var dt = new DataTable();
            try
            {
                using (SqlConnection conn = cx.ConexionServer())
                using (var cmd = new SqlCommand("SELECT * FROM DonorTbl", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch
            {
                // en la capa UI puedes manejar errores; aquí devolvemos tabla vacía
            }
            return dt;
        }
    }
}