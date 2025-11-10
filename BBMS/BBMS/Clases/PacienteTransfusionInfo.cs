using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BBMS.Clases
{
    /// <summary>
    /// DTO simple para transportar la información básica
    /// del paciente al formulario de transfusión.
    /// </summary>
    public class PacienteTransfusionInfo
    {
        public string Nombre { get; set; } = "";
        public string GrupoSanguineo { get; set; } = "";
    }

    /// <summary>
    /// Gestiona toda la lógica de datos para el
    /// formulario de TransfusionDeSangre.
    /// </summary>
    internal class cTransfusionDatos
    {
        // 1. Instanciamos la conexión central
        private cConexion conexionDB = new cConexion();

        /// <summary>
        /// Obtiene solo los IDs de los pacientes para llenar el ComboBox.
        /// </summary>
        public DataTable ObtenerIdsPacientes()
        {
            var dt = new DataTable();
            try
            {
                using (var con = conexionDB.ConexionServer())
                using (var sda = new SqlDataAdapter("SELECT PNum FROM PatientTbl", con))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar IDs de pacientes: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene el Nombre y Grupo Sanguíneo de un paciente específico.
        /// </summary>
        public PacienteTransfusionInfo ObtenerDetallesPaciente(int pacienteId)
        {
            var info = new PacienteTransfusionInfo();
            try
            {
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand("SELECT PName, PBGroup FROM PatientTbl WHERE PNum = @pnum", con))
                {
                    cmd.Parameters.AddWithValue("@pnum", pacienteId);
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            info.Nombre = reader["PName"].ToString();
                            info.GrupoSanguineo = reader["PBGroup"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener detalles del paciente: " + ex.Message);
            }
            return info;
        }

        /// <summary>
        /// Obtiene el stock actual para un grupo sanguíneo específico.
        /// </summary>
        public int ObtenerStock(string grupoSanguineo)
        {
            int stock = 0;
            if (string.IsNullOrWhiteSpace(grupoSanguineo)) return 0;

            try
            {
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand("SELECT BStock FROM BloodTbl WHERE BGroup = @bg", con))
                {
                    cmd.Parameters.AddWithValue("@bg", grupoSanguineo);
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        stock = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener stock: " + ex.Message);
            }
            return stock;
        }

        /// <summary>
        /// Realiza la transfusión completa como una transacción:
        /// 1. Verifica el stock.
        /// 2. Registra la transfusión.
        /// 3. Descuenta 1 del stock.
        /// Si algo falla, revierte todo.
        /// </summary>
        /// <returns>True si la transacción fue exitosa, False si falló.</returns>
        public bool RealizarTransfusion(string nombrePaciente, string grupoSanguineo)
        {
            // Usamos una única conexión para toda la transacción
            using (var con = conexionDB.ConexionServer())
            {
                con.Open();
                // Iniciamos la transacción
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Re-verificar stock DENTRO de la transacción (para máxima seguridad)
                        var stockCmd = new SqlCommand("SELECT BStock FROM BloodTbl WHERE BGroup = @bg", con, transaction);
                        stockCmd.Parameters.AddWithValue("@bg", grupoSanguineo);
                        var result = stockCmd.ExecuteScalar();
                        int currentStock = 0;
                        if (result != null && result != DBNull.Value)
                            currentStock = Convert.ToInt32(result);

                        if (currentStock <= 0)
                        {
                            MessageBox.Show("Error: El stock se agotó. No se puede realizar la transfusión.");
                            transaction.Rollback(); // Revertir
                            return false;
                        }

                        // 2. Registrar la transfusión
                        var insertCmd = new SqlCommand("INSERT INTO TransferTbl (PName, BGroup) VALUES (@pname, @bgroup)", con, transaction);
                        insertCmd.Parameters.AddWithValue("@pname", nombrePaciente);
                        insertCmd.Parameters.AddWithValue("@bgroup", grupoSanguineo);
                        insertCmd.ExecuteNonQuery();

                        // 3. Descontar el stock (forma segura)
                        var updateCmd = new SqlCommand("UPDATE BloodTbl SET BStock = BStock - 1 WHERE BGroup = @bg", con, transaction);
                        updateCmd.Parameters.AddWithValue("@bg", grupoSanguineo);
                        updateCmd.ExecuteNonQuery();

                        // 4. Si todo salió bien, confirmar los cambios
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // 5. Si algo falló, revertir todo
                        transaction.Rollback();
                        MessageBox.Show("Error en la transacción: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}