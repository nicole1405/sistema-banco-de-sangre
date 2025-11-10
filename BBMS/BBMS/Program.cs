using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBMS
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mostrar Splash como diálogo (bloquea hasta que se cierre)
            using (Splash splash = new Splash())
            {
                splash.ShowDialog();
            }

            // Después del Splash, iniciar la aplicación con Login
            Application.Run(new Login());
        }
    }
}