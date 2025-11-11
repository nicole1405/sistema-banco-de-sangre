using System;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Donante : UserControl
    {
        // 1. Servicio para operaciones sobre la tabla DonorTbl.
        private readonly DonanteService _service;

        // 2. Constructor: inicializa el formulario y el servicio.
        public Donante()
        {
            InitializeComponent();
            _service = new DonanteService();
        }

        // 3. Limpia todos los campos del formulario.
        private void Reset()
        {
            DNameTb.Text = "";
            DAgeTb.Text = "";
            DPhoneTb.Text = "";
            DAddressTb.Text = "";
            DGenCb.SelectedIndex = -1;
            DBGroupCb.SelectedIndex = -1;
        }

        // 4. Evento click del botón Guardar (Guna2Button): valida y guarda el donante.
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // 5. Validación de campos obligatorios.
            if (string.IsNullOrWhiteSpace(DNameTb.Text) ||
                string.IsNullOrWhiteSpace(DAgeTb.Text) ||
                string.IsNullOrWhiteSpace(DPhoneTb.Text) ||
                DGenCb.SelectedIndex == -1 ||
                DBGroupCb.SelectedIndex == -1)
            {
                MessageBox.Show("Falta información");
                return;
            }

            // 6. Validación de edad.
            if (!int.TryParse(DAgeTb.Text.Trim(), out int edad))
            {
                MessageBox.Show("Edad inválida");
                return;
            }

            // 7. Crea el objeto DonanteDto con los datos del formulario.
            var donante = new DonanteDto
            {
                Nombre = DNameTb.Text.Trim(),
                Edad = edad,
                Genero = DGenCb.SelectedItem.ToString(),
                Telefono = DPhoneTb.Text.Trim(),
                Direccion = DAddressTb.Text.Trim(),
                GrupoSangre = DBGroupCb.SelectedItem.ToString()
            };

            // 8. Llama al servicio para guardar el donante.
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

        // 9. Eventos de UI (sin lógica relevante, solo para compatibilidad con el diseñador).
        private void label12_Click(object sender, EventArgs e) { }
        private void Donante_Load(object sender, EventArgs e) { }
        private void DAgeTb_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void DNameTb_TextChanged(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void DPhoneTb_TextChanged(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}