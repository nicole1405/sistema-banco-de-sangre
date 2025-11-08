using System;
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
    public partial class TransfusionDeSangre : Form
    {
        public TransfusionDeSangre()
        {
            InitializeComponent();
            fillPatientCb();
        }
        SqlConnection Con = new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=C:\Users\DELL\Documents\BancoDeSangreDB.mdf;
            Integrated Security=True;Connect Timeout=30");
        private void fillPatientCb()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select PNum from PatientTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PNum", typeof(string));
            dt.Load(rdr);
            PatientIdCb.ValueMember = "PNum";
            PatientIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetData()
        {
            Con.Open();
            string query = "select * from PatientTbl where PNum=" + PatientIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PatNameTb.Text = dr["PName"].ToString();
                BloodGroup.Text = dr["PBGroup"].ToString();

            }
            Con.Close();
        }
        int stock=0;
        private void GetStock(string Bgroup)
        {
            Con.Open();
            string query = "select * from BloodTbl where BGroup='" + Bgroup + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stock = Convert.ToInt32(dr["BStock"].ToString());
            }
            Con.Close();
        }
        private void TransfusionDeSangre_Load(object sender, EventArgs e)
        {

        }
       /*int oldstock;
        private void GetStock(string Bgroup)
        {
            Con.Open();
            string query = "select * from BloodTbl where BGroup='" + Bgroup + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                oldstock = Convert.ToInt32(dr["BStock"].ToString());
            }
            Con.Close();
        }*/
        
        private void PatientIdCb_SelectedValueChanged(object sender, EventArgs e)
        {
            GetData();
            GetStock(BloodGroup.Text);
            if (stock > 0)
            {
                TransferBtn.Visible = true;
                AvarlableLbl.Text = "Stock Disponible";
                AvarlableLbl.Visible = true;
            }
            else
            {
                AvarlableLbl.Text = "Stock No Disponible";
                AvarlableLbl.Visible = true;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Pat = new Paciente();
            Pat.Show();
            this.Hide();

        }
        private void Reset()
        {
            PatNameTb.Text = "";
            //PatientIdCb.SelectedIndex = -1;
            BloodGroup.Text = "";
            AvarlableLbl.Visible = false;
            TransferBtn.Visible = false;

        }
        private void updateStock()
        {
            int newstock = stock - 1;
            try
            {
                string query = "update BloodTbl set BStock=" + newstock + " where BGroup=" + BloodGroup.Text + ";";
                Con.Open();
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Patient Successfully Deleted");
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "")
            {
                MessageBox.Show("Información Faltante");
            }
            else
            {
                try
                {
                    string query = "insert into TransferTbl values('" + PatNameTb.Text + "','" + BloodGroup.Text + "')";
                    Con.Open();
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transfusión Exitosa");
                    Con.Close();
                    GetStock(BloodGroup.Text);
                    updateStock();
                    Reset();
                    

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            InventarioDeSangre Bstock = new InventarioDeSangre();
            Bstock.Show();
            this.Hide();
        }
    }
}