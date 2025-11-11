using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBMS
{
    public partial class IntegrantesForm : Form
    {
        public IntegrantesForm()
        {
            InitializeComponent();
        }
        

        private void CerrarBtn_Click_1(object sender, EventArgs e)
        {
            // Cierra solo la ventana de integrantes
            this.Close();
        }
    }
    
}
