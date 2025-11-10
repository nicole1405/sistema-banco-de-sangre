using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms; // Solo para MessageBox en caso de error grave

namespace BBMS.Clases
{
    internal class cPacienteDatos
    {
        // Instanciamos la clase de conexión que nos provee la conexión
        private cConexion conexionDB = new cConexion();

        /// <summary>
        /// Obtiene todos los pacientes de la base de datos.
        /// </summary>
        /// <returns>Un DataTable con todos los pacientes.</returns>
        public DataTable ObtenerPacientes()
        {
            var dt = new DataTable();
            try
            {
                // Usamos el método de nuestra clase cConexion
                using (var con = conexionDB.ConexionServer())
                using (var sda = new SqlDataAdapter("SELECT * FROM PatientTbl", con))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de pacientes: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Elimina un paciente específico por su ID.
        /// </summary>
        /// <param name="id">El ID (PNum) del paciente a eliminar.</param>
        /// <returns>El número de filas afectadas (debería ser 1 si tuvo éxito).</returns>
        public int EliminarPaciente(int id)
        {
            int affectedRows = 0;
            try
            {
                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand("DELETE FROM PatientTbl WHERE PNum = @pnum", con))
                {
                    cmd.Parameters.AddWithValue("@pnum", id);
                    con.Open();
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el paciente: " + ex.Message);
            }
            return affectedRows;
        }

        /// <summary>
        /// Actualiza la información de un paciente existente.
        /// </summary>
        /// <returns>El número de filas afectadas (debería ser 1 si tuvo éxito).</returns>
        public int ActualizarPaciente(int id, string nombre, int edad, string telefono, string genero, string grupoSanguineo, string direccion)
        {
            int affectedRows = 0;
            try
            {
                string query = @"UPDATE PatientTbl 
                                 SET PName = @pname, 
                                     PAge = @page, 
                                     PPhone = @pphone, 
                                     PGender = @pgender, 
                                     PBGroup = @pbgroup, 
                                     PAddress = @paddress 
                                 WHERE PNum = @pnum";

                using (var con = conexionDB.ConexionServer())
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pname", nombre);
                    cmd.Parameters.AddWithValue("@page", edad);
                    cmd.Parameters.AddWithValue("@pphone", telefono);
                    cmd.Parameters.AddWithValue("@pgender", genero);
                    cmd.Parameters.AddWithValue("@pbgroup", grupoSanguineo);
                    cmd.Parameters.AddWithValue("@paddress", direccion);
                    cmd.Parameters.AddWithValue("@pnum", id);

                    con.Open();
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el paciente: " + ex.Message);
            }
            return affectedRows;
        }
    }
}
