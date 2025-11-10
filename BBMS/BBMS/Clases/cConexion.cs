using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBMS.Clases
{
    internal class cConexion
    {
        //hacemos el metodo para la conexión al servidor que devolverá el tipo de dato SqlConnection
        public SqlConnection ConexionServer()
        {
            //declaramos la variable conn de tipo de dato SqlConnection
            SqlConnection conn;
            try
            {
                //declaramos la variable de tipo string que contendrá toda la configuración de la cadena de conexion
                string cadenaConexion = @"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                conn = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                //estructura try catch por algún error
                throw new ArgumentException("Error al conectar", ex);
            }
            return conn;
        }
    }
}
