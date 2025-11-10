using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BancoSangreApp
{
    // =============================================
    // Clase: DatabaseConnection
    // Descripción: Maneja la conexión a SQL Server
    // =============================================
    public class DatabaseConnection
    {
        // Cadena de conexión - AJUSTAR según tu servidor
        private static string connectionString =
                 "Server=localhost;Database=BancoSangreDB;Integrated Security=true;";

        /// <summary>
        /// Obtiene una nueva conexión a la base de datos
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Prueba la conexión a la base de datos
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    // =============================================
    // Clase: Usuario
    // Descripción: Representa un usuario del sistema
    // =============================================
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }

        // Usuario actual del sistema (sesión)
        public static Usuario UsuarioActual { get; set; }

        /// <summary>
        /// Valida las credenciales del usuario
        /// </summary>
        public static bool Login(string usuario, string contrasena)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT UsuarioID, NombreUsuario, NombreCompleto, Rol, Email 
                                   FROM Usuarios 
                                   WHERE NombreUsuario = @Usuario 
                                   AND Contrasena = @Contrasena 
                                   AND Activo = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UsuarioActual = new Usuario
                                {
                                    UsuarioID = (int)reader["UsuarioID"],
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    NombreCompleto = reader["NombreCompleto"].ToString(),
                                    Rol = reader["Rol"].ToString(),
                                    Email = reader["Email"].ToString()
                                };

                                // Registrar actividad de login
                                RegistrarActividad("Login", "Inicio de sesión exitoso");
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Registra una actividad en el historial
        /// </summary>
        public static void RegistrarActividad(string tipoAccion, string descripcion,
            string tablaAfectada = null, int? registroID = null)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO HistorialActividades 
                                   (UsuarioID, TipoAccion, Descripcion, TablaAfectada, RegistroID)
                                   VALUES (@UsuarioID, @TipoAccion, @Descripcion, @TablaAfectada, @RegistroID)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioID",
                            UsuarioActual != null ? UsuarioActual.UsuarioID : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoAccion", tipoAccion);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@TablaAfectada",
                            tablaAfectada ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RegistroID",
                            registroID ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // No mostrar error al usuario, solo log
                Console.WriteLine("Error al registrar actividad: " + ex.Message);
            }
        }
    }

    // =============================================
    // Clase: Donante
    // Descripción: Gestión de donantes
    // =============================================
    public class Donante
    {
        public int DonanteID { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoSangre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        /// <summary>
        /// Registra un nuevo donante en la base de datos
        /// </summary>
        public bool Registrar()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Donantes 
                                   (Cedula, NombreCompleto, TipoSangre, Telefono, Email, Direccion, FechaNacimiento)
                                   VALUES (@Cedula, @Nombre, @TipoSangre, @Telefono, @Email, @Direccion, @FechaNacimiento);
                                   SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", Cedula);
                        cmd.Parameters.AddWithValue("@Nombre", NombreCompleto);
                        cmd.Parameters.AddWithValue("@TipoSangre", TipoSangre);
                        cmd.Parameters.AddWithValue("@Telefono", Telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Direccion", Direccion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaNacimiento",
                            FechaNacimiento ?? (object)DBNull.Value);

                        DonanteID = Convert.ToInt32(cmd.ExecuteScalar());

                        // Registrar actividad
                        Usuario.RegistrarActividad("Crear Donante",
                            $"Donante registrado: {NombreCompleto} ({Cedula})",
                            "Donantes", DonanteID);

                        // Crear notificación
                        CrearNotificacion("Nuevo Donante",
                            $"Se ha registrado un nuevo donante: {NombreCompleto}", "Baja");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar donante: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Actualiza los datos de un donante existente
        /// </summary>
        public bool Actualizar()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Donantes SET 
                                   Cedula = @Cedula,
                                   NombreCompleto = @Nombre,
                                   TipoSangre = @TipoSangre,
                                   Telefono = @Telefono,
                                   Email = @Email,
                                   Direccion = @Direccion,
                                   FechaNacimiento = @FechaNacimiento
                                   WHERE DonanteID = @DonanteID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DonanteID", DonanteID);
                        cmd.Parameters.AddWithValue("@Cedula", Cedula);
                        cmd.Parameters.AddWithValue("@Nombre", NombreCompleto);
                        cmd.Parameters.AddWithValue("@TipoSangre", TipoSangre);
                        cmd.Parameters.AddWithValue("@Telefono", Telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Direccion", Direccion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaNacimiento",
                            FechaNacimiento ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();

                        // Registrar actividad
                        Usuario.RegistrarActividad("Actualizar Donante",
                            $"Donante actualizado: {NombreCompleto}",
                            "Donantes", DonanteID);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar donante: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Cambia el estado del donante a inactivo (eliminación lógica)
        /// </summary>
        public static bool CambiarEstado(int donanteID, bool activo)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Donantes SET Activo = @Activo WHERE DonanteID = @DonanteID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DonanteID", donanteID);
                        cmd.Parameters.AddWithValue("@Activo", activo);

                        cmd.ExecuteNonQuery();

                        // Registrar actividad
                        Usuario.RegistrarActividad("Cambiar Estado Donante",
                            $"Estado cambiado a {(activo ? "Activo" : "Inactivo")}",
                            "Donantes", donanteID);

                        // Crear notificación
                        CrearNotificacion("Estado Donante",
                            $"Donante ID {donanteID} marcado como {(activo ? "activo" : "inactivo")}",
                            "Baja");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar estado del donante: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Busca donantes por diferentes criterios
        /// </summary>
        public static DataTable Buscar(string criterio, string valor)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "";

                    switch (criterio.ToLower())
                    {
                        case "cedula":
                            query = "SELECT * FROM Donantes WHERE Cedula LIKE @Valor AND Activo = 1";
                            break;
                        case "nombre":
                            query = "SELECT * FROM Donantes WHERE NombreCompleto LIKE @Valor AND Activo = 1";
                            break;
                        case "tiposangre":
                            query = "SELECT * FROM Donantes WHERE TipoSangre = @Valor AND Activo = 1";
                            break;
                        default:
                            query = "SELECT * FROM Donantes WHERE Activo = 1";
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Valor", "%" + valor + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar donantes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene todos los donantes activos
        /// </summary>
        public static DataTable ObtenerTodos()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT DonanteID, Cedula, NombreCompleto, TipoSangre, 
                                   Telefono, Email, FechaRegistro 
                                   FROM Donantes WHERE Activo = 1 
                                   ORDER BY FechaRegistro DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener donantes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Crea una notificación en el sistema
        /// </summary>
        private static void CrearNotificacion(string tipo, string mensaje, string prioridad)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Notificaciones 
                                   (TipoNotificacion, Mensaje, Prioridad)
                                   VALUES (@Tipo, @Mensaje, @Prioridad)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", tipo);
                        cmd.Parameters.AddWithValue("@Mensaje", mensaje);
                        cmd.Parameters.AddWithValue("@Prioridad", prioridad);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
    }

    // =============================================
    // Clase: Donacion
    // Descripción: Gestión de donaciones de sangre
    // =============================================
    public class Donacion
    {
        public int DonacionID { get; set; }
        public int DonanteID { get; set; }
        public int? CentroID { get; set; }
        public string TipoSangre { get; set; }
        public int CantidadML { get; set; }
        public DateTime FechaRecoleccion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }

        /// <summary>
        /// Registra una nueva donación y actualiza el inventario
        /// </summary>
        public bool Registrar()
        {
            SqlConnection conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = DatabaseConnection.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Insertar donación
                string queryDonacion = @"INSERT INTO Donaciones 
                                       (DonanteID, CentroID, TipoSangre, CantidadML, 
                                        FechaRecoleccion, FechaCaducidad, Estado, Observaciones, UsuarioRegistro)
                                       VALUES (@DonanteID, @CentroID, @TipoSangre, @CantidadML, 
                                               @FechaRecoleccion, @FechaCaducidad, @Estado, @Observaciones, @UsuarioID);
                                       SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(queryDonacion, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@DonanteID", DonanteID);
                    cmd.Parameters.AddWithValue("@CentroID", CentroID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TipoSangre", TipoSangre);
                    cmd.Parameters.AddWithValue("@CantidadML", CantidadML);
                    cmd.Parameters.AddWithValue("@FechaRecoleccion", FechaRecoleccion);
                    cmd.Parameters.AddWithValue("@FechaCaducidad", FechaCaducidad);
                    cmd.Parameters.AddWithValue("@Estado", Estado ?? "Disponible");
                    cmd.Parameters.AddWithValue("@Observaciones", Observaciones ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UsuarioID", Usuario.UsuarioActual.UsuarioID);

                    DonacionID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Actualizar inventario
                string queryInventario = @"UPDATE InventarioSangre 
                                         SET CantidadDisponibleML = CantidadDisponibleML + @CantidadML,
                                             UltimaActualizacion = GETDATE()
                                         WHERE TipoSangre = @TipoSangre";

                using (SqlCommand cmd = new SqlCommand(queryInventario, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@TipoSangre", TipoSangre);
                    cmd.Parameters.AddWithValue("@CantidadML", CantidadML);
                    cmd.ExecuteNonQuery();
                }

                // 3. Crear notificación
                string queryNotificacion = @"INSERT INTO Notificaciones 
                                           (TipoNotificacion, Mensaje, Prioridad)
                                           VALUES (@Tipo, @Mensaje, @Prioridad)";

                using (SqlCommand cmd = new SqlCommand(queryNotificacion, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@Tipo", "Nueva Donación");
                    cmd.Parameters.AddWithValue("@Mensaje",
                        $"Nueva donación registrada: {CantidadML}ml de tipo {TipoSangre}");
                    cmd.Parameters.AddWithValue("@Prioridad", "Media");
                    cmd.ExecuteNonQuery();
                }

                // Commit de la transacción
                transaction.Commit();

                // Registrar actividad
                Usuario.RegistrarActividad("Registrar Donación",
                    $"Donación registrada: {CantidadML}ml tipo {TipoSangre}",
                    "Donaciones", DonacionID);

                MessageBox.Show("Donación registrada exitosamente. Inventario actualizado.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Error al registrar donación: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn?.Close();
            }
        }

        /// <summary>
        /// Obtiene todas las donaciones con información detallada
        /// </summary>
        public static DataTable ObtenerTodas()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT * FROM vw_DonacionesRecientes ORDER BY FechaRecoleccion DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener donaciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Busca donaciones por criterios
        /// </summary>
        public static DataTable Buscar(string criterio, string valor)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "";

                    switch (criterio.ToLower())
                    {
                        case "tiposangre":
                            query = @"SELECT * FROM vw_DonacionesRecientes 
                                    WHERE TipoSangre = @Valor 
                                    ORDER BY FechaRecoleccion DESC";
                            break;
                        case "donante":
                            query = @"SELECT * FROM vw_DonacionesRecientes 
                                    WHERE Donante LIKE @Valor 
                                    ORDER BY FechaRecoleccion DESC";
                            break;
                        case "fecha":
                            query = @"SELECT * FROM vw_DonacionesRecientes 
                                    WHERE CAST(FechaRecoleccion AS DATE) = @Valor 
                                    ORDER BY FechaRecoleccion DESC";
                            break;
                        default:
                            return ObtenerTodas();
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (criterio.ToLower() == "donante")
                            cmd.Parameters.AddWithValue("@Valor", "%" + valor + "%");
                        else
                            cmd.Parameters.AddWithValue("@Valor", valor);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar donaciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
    }
}