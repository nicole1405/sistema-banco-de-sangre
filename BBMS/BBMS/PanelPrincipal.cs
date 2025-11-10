using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBMS
{
    public partial class PanelPrincipal : Form
    {
        public PanelPrincipal()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Server=tcp:eu-az-sql-serv1.database.windows.net,1433;Initial Catalog=d6od1fpxsjfl7w6;Persist Security Info=False;User ID=uaky7g8xaa24yks;Password=8yNTcJ$#7n8KFsCHAwxDJ?BrO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        private void GetData()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select counto (*) from DonorTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DonorLbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select counto (*) from TransferTbl", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            TransferLbl.Text = dt1.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("Select counto (*) from EmployeeTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            EmployeeLbl.Text = dt2.Rows[0][0].ToString();
            //Parte grafica de los tipos de sangre
            SqlDataAdapter sda3 = new SqlDataAdapter("Select counto (*) from BloodTbl", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            int Bstock = Convert.ToInt32(dt3.Rows[0][0].ToString());
            TotalLbl.Text = "" + Bstock;
            SqlDataAdapter sda4 = new SqlDataAdapter("Select counto (*) from BloodTbl where BGroup='" + "O+" + "'", Con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            OplusNumLbl.Text = dt4.Rows[0][0].ToString();
            double OplusPercentage = (Convert.ToDouble(dt4.Rows[0][0].ToString()) / Bstock) * 100;
            OplusProgress.Value = Convert.ToInt32(OplusPercentage);


            SqlDataAdapter sda5 = new SqlDataAdapter("Select BStock from BloodTbl where BGroup='" + "AB+" + "'", Con);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            ABplusLabel.Text = dt5.Rows[0][0].ToString();
            double ABPlusPercentage = (Convert.ToDouble(dt5.Rows[0][0].ToString()) / Bstock) * 100;
            ABplusProgress.Value = Convert.ToInt32(ABPlusPercentage);
            //Ominus Group Coding
            SqlDataAdapter sda6 = new SqlDataAdapter("Select BStock from BloodTbl where BGroup='" + "O-" + "'", Con);
            DataTable dt6 = new DataTable();
            sda6.Fill(dt6);
            OminusLabel.Text = dt6.Rows[0][0].ToString();
            double OminusPercentage = (Convert.ToDouble(dt6.Rows[0][0].ToString()) / Bstock) * 100;
            OminusProgress.Value = Convert.ToInt32(OminusPercentage);
            //ABminus Group Coding
            SqlDataAdapter sda7 = new SqlDataAdapter("Select BStock from BloodTbl where BGroup='" + "AB-" + "'", Con);
            DataTable dt7 = new DataTable();
            sda7.Fill(dt7);
            ABminuslbl.Text = dt7.Rows[0][0].ToString();
            double ABminusPercentage = (Convert.ToDouble(dt7.Rows[0][0].ToString()) / Bstock) * 100;
            ABminusProgress.Value = Convert.ToInt32(ABminusPercentage);
            Con.Close();
        }
        private void PanelPrincipal_Load(object sender, EventArgs e)
        {

        }

    }
}
