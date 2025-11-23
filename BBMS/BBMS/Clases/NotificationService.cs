using System;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Clases
{
    /// <summary>
    /// Servicio para crear/leer/marcar notificaciones en la tabla Notificaciones.
    /// Inserta UsuarioId opcional para notificaciones dirigidas a un usuario concreto.
    /// </summary>
    public static class NotificationService
    {
        private static readonly cConexion _cx = new cConexion();

        /// <summary>
        /// Crea una notificación.
        /// Si usuarioId es null => notificación global para todos los usuarios.
        /// </summary>
        public static void Create(string tipo, string mensaje, string prioridad = "Baja", int? usuarioId = null)
        {
            try
            {
                using (var con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(@"
                    INSERT INTO Notificaciones
                        (TipoNotificacion, Mensaje, Prioridad, UsuarioId) 
                    VALUES
                        (@Tipo, @Mensaje, @Prioridad, @UsuarioId)", con))
                {
                    cmd.Parameters.AddWithValue("@Tipo", tipo ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Mensaje", mensaje ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Prioridad", prioridad ?? "Baja");
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId.HasValue ? (object)usuarioId.Value : DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                try { System.Diagnostics.Debug.WriteLine("Error NotificationService.Create: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// Obtiene notificaciones no leídas para el usuario (incluye notificaciones globales UsuarioId IS NULL).
        /// Devuelve DataTable con todas las columnas disponibles; la UI mapeará los nombres.
        /// </summary>
        public static DataTable GetUnreadForUser(int usuarioId)
        {
            var dt = new DataTable();
            try
            {
                using (var con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(@"
                    SELECT *
                    FROM Notificaciones
                    WHERE IsRead = 0
                      AND (UsuarioId = @UsuarioId OR UsuarioId IS NULL)
                    ORDER BY CreatedAt DESC", con))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                try { System.Diagnostics.Debug.WriteLine("Error NotificationService.GetUnreadForUser: " + ex.Message); } catch { }
            }
            return dt;
        }

        /// <summary>
        /// Marca una notificación como leída.
        /// </summary>
        public static void MarkAsRead(int notificationId)
        {
            try
            {
                using (var con = _cx.ConexionServer())
                using (var cmd = new SqlCommand("UPDATE Notificaciones SET IsRead = 1 WHERE NotificationId = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", notificationId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                try { System.Diagnostics.Debug.WriteLine("Error NotificationService.MarkAsRead: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// Marca todas las notificaciones no leídas de un usuario como leídas.
        /// Incluye notificaciones globales (UsuarioId IS NULL) que aún no estén leídas.
        /// </summary>
        public static void MarkAllAsReadForUser(int usuarioId)
        {
            try
            {
                using (var con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(@"
                    UPDATE Notificaciones
                    SET IsRead = 1
                    WHERE IsRead = 0
                      AND (UsuarioId = @UsuarioId OR UsuarioId IS NULL)", con))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                try { System.Diagnostics.Debug.WriteLine("Error NotificationService.MarkAllAsReadForUser: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// Cuenta notificaciones no leídas para el usuario (incluye globales).
        /// </summary>
        public static int CountUnreadForUser(int usuarioId)
        {
            try
            {
                using (var con = _cx.ConexionServer())
                using (var cmd = new SqlCommand(@"
                    SELECT COUNT(1)
                    FROM Notificaciones
                    WHERE IsRead = 0
                      AND (UsuarioId = @UsuarioId OR UsuarioId IS NULL)", con))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    con.Open();
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) return Convert.ToInt32(res);
                }
            }
            catch (Exception ex)
            {
                try { System.Diagnostics.Debug.WriteLine("Error NotificationService.CountUnreadForUser: " + ex.Message); } catch { }
            }
            return 0;
        }
    }
}