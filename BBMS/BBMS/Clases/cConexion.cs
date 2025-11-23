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
                string cadenaConexion = "Data Source=FIDEV;Initial Catalog=BancoDeSangre;Persist Security Info=True;User ID=sa;Password=Delta92_$1911;Encrypt=True;TrustServerCertificate=True";
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
