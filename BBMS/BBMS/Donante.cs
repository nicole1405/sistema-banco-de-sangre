using System;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Donante : Form
    {
        private readonly DonanteService _service;

        // Constructor del formulario
        public Donante()
        {
            InitializeComponent();
            _service = new DonanteService();
        }

        // Lo llamamos luego de guardar un donante, con esto reseteamos los campos del formulario
        private void Reset()
        {
            DNameTb.Text = "";
            DAgeTb.Text = "";
            DPhoneTb.Text = "";
            DAddressTb.Text = "";
            DGenCb.SelectedIndex = -1;
            DBGroupCb.SelectedIndex = -1;
        }

        // Evento que se ejecuta cuando le damos click al botón guardar
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Validación simple de UI
            if (string.IsNullOrWhiteSpace(DNameTb.Text) ||
                string.IsNullOrWhiteSpace(DAgeTb.Text) ||
                string.IsNullOrWhiteSpace(DPhoneTb.Text) ||
                DGenCb.SelectedIndex == -1 ||
                DBGroupCb.SelectedIndex == -1)
            {
                MessageBox.Show("Falta información");
                return;
            }

            // Intentar parsear la edad
            if (!int.TryParse(DAgeTb.Text.Trim(), out int edad))
            {
                MessageBox.Show("Edad inválida");
                return;
            }

            // Crear DTO
            var donante = new DonanteDto
            {
                Nombre = DNameTb.Text.Trim(),
                Edad = edad,
                Genero = DGenCb.SelectedItem.ToString(),
                Telefono = DPhoneTb.Text.Trim(),
                Direccion = DAddressTb.Text.Trim(),
                GrupoSangre = DBGroupCb.SelectedItem.ToString()
            };

            // Llamar al servicio
            if (_service.Insert(donante, out string error))
            {
                MessageBox.Show("Donante guardado con éxito");
                Reset();
            }
            else
            {
                MessageBox.Show("Error al guardar donante: " + error);
            }
        }

        // Resto de eventos UI (sin cambios)
        private void label12_Click(object sender, EventArgs e) { }
        private void Donante_Load(object sender, EventArgs e) { }
        private void DAgeTb_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void DNameTb_TextChanged(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void DPhoneTb_TextChanged(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e)
        {
            Donar Ob = new Donar();
            Ob.Show();
            this.Hide();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Verdonantes Ob = new Verdonantes();
            Ob.Show();
            this.Hide();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Paciente Ob = new Paciente();
            Ob.Show();
            this.Hide();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            ListaPacientes Ob = new ListaPacientes();
            Ob.Show();
            this.Hide();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            InventarioDeSangre Ob = new InventarioDeSangre();
            Ob.Show();
            this.Hide();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            TransfusionDeSangre Ob = new TransfusionDeSangre();
            Ob.Show();
            this.Hide();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            PanelPrincipal Ob = new PanelPrincipal();
            Ob.Show();
            this.Hide();
        }
        private void label9_Click(object sender, EventArgs e)
        {
            Login Ob = new Login();
            Ob.Show();
            this.Hide();
        }
    }
}