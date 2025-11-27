using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BBMS.Clases
{
    // DTO simple para pasar datos entre UI y capa de datos
    public class DonanteDto
    {
        // Id de la tabla (asumo columna DNum en DonorTbl)
        public int Id { get; set; }

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

        private void Log(string message)
        {
            Debug.WriteLine($"[DonanteService] {DateTime.Now:O} - {message}");
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
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(donante.Telefono) ? (object)DBNull.Value : donante.Telefono);
                    cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(donante.Direccion) ? (object)DBNull.Value : donante.Direccion);
                    cmd.Parameters.AddWithValue("@bgroup", donante.GrupoSangre ?? string.Empty);

                    Log("Insert: abrir conexión.");
                    conn.Open();
                    Log("Insert: Database: " + conn.Database + " DataSource: " + conn.DataSource);
                    Log("Insert: Ejecutando INSERT DonorTbl");

                    int affected = cmd.ExecuteNonQuery();
                    Log("Insert: Filas afectadas = " + affected);

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
                Log("Insert Error: " + ex.ToString());
                errorMessage = ex.Message;
                return false;
            }
        }

        // Actualiza un donante por id (usa DNum como PK)
        public bool Update(DonanteDto donante, int id, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (SqlConnection conn = cx.ConexionServer())
                using (var cmd = new SqlCommand(@"UPDATE DonorTbl SET
                        DName = @name,
                        DAge = @age,
                        DGender = @gender,
                        DPhone = @phone,
                        DAddress = @address,
                        DBGroup = @bgroup
                    WHERE DNum = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", donante.Nombre ?? string.Empty);
                    cmd.Parameters.AddWithValue("@age", donante.Edad);
                    cmd.Parameters.AddWithValue("@gender", donante.Genero ?? string.Empty);
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(donante.Telefono) ? (object)DBNull.Value : donante.Telefono);
                    cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(donante.Direccion) ? (object)DBNull.Value : donante.Direccion);
                    cmd.Parameters.AddWithValue("@bgroup", donante.GrupoSangre ?? string.Empty);
                    cmd.Parameters.AddWithValue("@id", id);

                    Log($"Update: abrir conexión. Database: {conn.Database} DataSource: {conn.DataSource}");
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    Log("Update: Filas afectadas = " + affected);

                    if (affected > 0)
                    {
                        try
                        {
                            NotificationService.Create("Donante Actualizado", $"Donante actualizado: {donante.Nombre}", "Media");
                        }
                        catch { }
                        return true;
                    }

                    errorMessage = "No se actualizaron filas. Verifica el id y permisos.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log("Update Error: " + ex.ToString());
                errorMessage = ex.Message;
                return false;
            }
        }

        // Elimina un donante por id. Detecta la columna PK real antes de ejecutar DELETE (incluye DNum).
        public bool Delete(int id, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (SqlConnection conn = cx.ConexionServer())
                {
                    conn.Open();
                    Log("Delete: conexión abierta. Database: " + conn.Database + " DataSource: " + conn.DataSource);

                    // Lista de candidatos que consideramos como PK en la tabla (DNum primero)
                    var pkCandidates = new[] { "DNum", "DId", "Id", "DonorId", "donor_id" };

                    // Consultar qué columnas de esa lista existen en DonorTbl
                    var existing = new List<string>();
                    string candidatesCsv = string.Join("','", pkCandidates);
                    string checkSql = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DonorTbl' AND COLUMN_NAME IN ('{candidatesCsv}')";
                    using (var checkCmd = new SqlCommand(checkSql, conn))
                    using (var rdr = checkCmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            existing.Add(rdr.GetString(0));
                        }
                    }

                    if (existing.Count == 0)
                    {
                        // Obtener lista completa de columnas para diagnóstico
                        var cols = new List<string>();
                        using (var allCmd = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DonorTbl'", conn))
                        using (var rdr2 = allCmd.ExecuteReader())
                        {
                            while (rdr2.Read()) cols.Add(rdr2.GetString(0));
                        }

                        errorMessage = "No se encontró una columna PK conocida (busqué: " + string.Join(", ", pkCandidates) + "). Columnas en DonorTbl: " + string.Join(", ", cols);
                        Log("Delete: " + errorMessage);
                        return false;
                    }

                    // Usar la primera columna encontrada (prioriza el orden de pkCandidates)
                    string pkToUse = null;
                    foreach (var p in pkCandidates)
                    {
                        if (existing.Contains(p))
                        {
                            pkToUse = p;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(pkToUse))
                    {
                        errorMessage = "No se pudo determinar la columna PK a usar.";
                        Log("Delete: " + errorMessage);
                        return false;
                    }

                    // Ejecutar DELETE usando la columna encontrada
                    string deleteSql = $"DELETE FROM DonorTbl WHERE [{pkToUse}] = @id";
                    using (var delCmd = new SqlCommand(deleteSql, conn))
                    {
                        delCmd.Parameters.AddWithValue("@id", id);
                        Log($"Delete: Ejecutando '{deleteSql}' con @id={id}");
                        int affected = delCmd.ExecuteNonQuery();
                        Log("Delete: Filas afectadas = " + affected);

                        if (affected <= 0)
                        {
                            errorMessage = $"No se eliminaron filas. Affected={affected}. Verifica que el id {id} exista en la columna {pkToUse}.";
                            Log("Delete: " + errorMessage);
                            return false;
                        }
                    }

                    // Verificar que no exista el registro
                    using (var verifyCmd = new SqlCommand($"SELECT COUNT(1) FROM DonorTbl WHERE [{pkToUse}] = @id", conn))
                    {
                        verifyCmd.Parameters.AddWithValue("@id", id);
                        int count = Convert.ToInt32(verifyCmd.ExecuteScalar());
                        Log($"Delete: Verificación COUNT después DELETE = {count} (pk={pkToUse})");
                        if (count > 0)
                        {
                            errorMessage = $"El registro con id {id} sigue presente después del DELETE en la columna {pkToUse}.";
                            Log("Delete: " + errorMessage);
                            return false;
                        }
                    }

                    try { NotificationService.Create("Donante Eliminado", $"Donante id: {id} eliminado (pk={pkToUse})", "Alta"); } catch { }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log("Delete Exception: " + ex.ToString());
                errorMessage = ex.Message;
                return false;
            }
        }

        // Obtener todos los donantes (puedes adaptar la consulta si requiere filtros)
        public DataTable GetAll()
        {
            var dt = new DataTable();
            try
            {
                using (SqlConnection conn = cx.ConexionServer())
                using (var cmd = new SqlCommand("SELECT * FROM DonorTbl", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    Log("GetAll: abrir conexión. ");
                    conn.Open();
                    da.Fill(dt);
                    Log("GetAll: Filas retornadas = " + dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                Log("GetAll Error: " + ex.ToString());
                // en la capa UI puedes manejar errores; aquí devolvemos tabla vacía
            }
            return dt;
        }
    }
}