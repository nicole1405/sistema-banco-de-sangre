using System;
using System.Data.SqlClient;
using System.Windows.Forms; // Para MessageBox en caso de error

namespace BBMS.Clases
{
    /// <summary>
    /// DTO (Data Transfer Object).
    /// Una clase simple para transportar los datos desde la lógica
    /// de negocio hasta el formulario (UI).
    /// </summary>
    public class EstadisticasDashboard
    {
        public int ConteoDonantes { get; set; }
        public int ConteoTransferencias { get; set; }
        public int ConteoEmpleados { get; set; }
        public int StockTotalSangre { get; set; }
        public int StockO_Pos { get; set; }
        public int StockAB_Pos { get; set; }
        public int StockO_Neg { get; set; }
        public int StockAB_Neg { get; set; }
        // Agrega aquí cualquier otro grupo que necesites, ej: StockA_Pos, etc.
    }


    /// <summary>
    /// Clase de servicio que se encarga de obtener los datos
    /// para el panel principal.
    /// </summary>
    internal class cDashboardDatos
    {
        // 1. Usamos la conexión centralizada
        private cConexion conexionDB = new cConexion();

        /// <summary>
        /// Obtiene todas las estadísticas del dashboard en una
        /// sola consulta eficiente.
        /// </summary>
        /// <returns>Un objeto EstadisticasDashboard con todos los conteos.</returns>
        public EstadisticasDashboard ObtenerEstadisticas()
        {
            var estadisticas = new EstadisticasDashboard(); // Inicializa todo en 0

            // 2. Esta query única es MUCHO más eficiente que múltiples llamadas.
            // Ejecuta todas las subconsultas en el servidor de una sola vez.
            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM DonorTbl) AS ConteoDonantes,
                    (SELECT COUNT(*) FROM TransferTbl) AS ConteoTransferencias,
                    (SELECT COUNT(*) FROM EmployeeTbl) AS ConteoEmpleados,
                    (SELECT ISNULL(SUM(BStock), 0) FROM BloodTbl) AS StockTotal,
                    (SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = 'O+') AS StockO_Pos,
                    (SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = 'AB+') AS StockAB_Pos,
                    (SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = 'O-') AS StockO_Neg,
                    (SELECT ISNULL(BStock, 0) FROM BloodTbl WHERE BGroup = 'AB-') AS StockAB_Neg
            "; // Si necesitas más grupos (A+, B+, etc.) añádelos aquí.

            try
            {
                // 3. Usamos la conexión de cConexion
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Lee la única fila de resultados
                        {
                            // Helper local para leer de forma segura
                            int GetInt(string columnName) => reader[columnName] != DBNull.Value ? Convert.ToInt32(reader[columnName]) : 0;

                            // 4. Llenamos el objeto DTO
                            estadisticas.ConteoDonantes = GetInt("ConteoDonantes");
                            estadisticas.ConteoTransferencias = GetInt("ConteoTransferencias");
                            estadisticas.ConteoEmpleados = GetInt("ConteoEmpleados");
                            estadisticas.StockTotalSangre = GetInt("StockTotal");
                            estadisticas.StockO_Pos = GetInt("StockO_Pos");
                            estadisticas.StockAB_Pos = GetInt("StockAB_Pos");
                            estadisticas.StockO_Neg = GetInt("StockO_Neg");
                            estadisticas.StockAB_Neg = GetInt("StockAB_Neg");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadísticas del dashboard: " + ex.Message);
                // Retorna el objeto 'estadisticas' vacío (con ceros) en caso de error
            }

            return estadisticas;
        }
    }
}
