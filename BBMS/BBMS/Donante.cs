using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BBMS.Clases;

namespace BBMS
{
    public partial class Donante : UserControl
    {
        private readonly DonanteService _service;
        private int? _selectedId = null;

        // Constructor del formulario
        public Donante()
        {
            InitializeComponent();
            _service = new DonanteService();

            // Enlazar validadores de entrada (KeyPress) para forzar sólo números donde corresponde
            DPhoneTb.KeyPress += DPhoneTb_KeyPress;
            DAgeTb.KeyPress += DAgeTb_KeyPress;
            DNameTb.KeyPress += DNameTb_KeyPress;

            // Limitar longitudes razonables
            try
            {
                DAgeTb.MaxLength = 3;
                DPhoneTb.MaxLength = 15;
            }
            catch { /* Si el control no soporta MaxLength por alguna versión, ignorar */ }
        }

        private void Log(string msg)
        {
            Debug.WriteLine("[Donante] " + DateTime.Now.ToString("O") + " - " + msg);
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
            _selectedId = null;
            guna2Button1.Text = "Guardar";
            donorsGrid.ClearSelection();

            // Restaurar colores por si quedaron marcados
            RestoreControlStyles();
        }

        private void RestoreControlStyles()
        {
            try
            {
                // Guna2TextBox soporta BorderColor; si no, no hace nada crítico
                DNameTb.BorderColor = System.Drawing.Color.Indigo;
                DAgeTb.BorderColor = System.Drawing.Color.Indigo;
                DPhoneTb.BorderColor = System.Drawing.Color.Indigo;
                DAddressTb.BackColor = System.Drawing.Color.White;
            }
            catch { }
        }

        // Carga datos en el grid
        private void LoadDonors()
        {
            var dt = _service.GetAll();
            donorsGrid.DataSource = dt;

            // Si la tabla tiene columna DNum, mostrarla como ID
            if (dt.Columns.Contains("DNum"))
            {
                donorsGrid.Columns["DNum"].HeaderText = "ID";
                donorsGrid.Columns["DNum"].DisplayIndex = 0;
                donorsGrid.Columns["DNum"].Width = 60;
            }

            // Ajustes de cabeceras si existen
            if (dt.Columns.Contains("DName")) donorsGrid.Columns["DName"].HeaderText = "Nombre";
            if (dt.Columns.Contains("DAge")) donorsGrid.Columns["DAge"].HeaderText = "Edad";
            if (dt.Columns.Contains("DGender")) donorsGrid.Columns["DGender"].HeaderText = "Género";
            if (dt.Columns.Contains("DPhone")) donorsGrid.Columns["DPhone"].HeaderText = "Teléfono";
            if (dt.Columns.Contains("DAddress")) donorsGrid.Columns["DAddress"].HeaderText = "Dirección";
            if (dt.Columns.Contains("DBGroup")) donorsGrid.Columns["DBGroup"].HeaderText = "Grupo";

            // Asegurarnos de que NO quede ninguna fila seleccionada tras el rebind.
            try
            {
                donorsGrid.ClearSelection();
                donorsGrid.CurrentCell = null;
            }
            catch (Exception ex)
            {
                Log("LoadDonors: no se pudo limpiar la selección del grid: " + ex);
            }
        }

        // --- NUEVO: helper robusto para extraer id desde la fila seleccionada ---
        private int? GetSelectedIdFromGrid()
        {
            try
            {
                if (_selectedId.HasValue) return _selectedId.Value;

                DataGridViewRow row = null;
                if (donorsGrid.CurrentRow != null && donorsGrid.CurrentRow.Index >= 0)
                    row = donorsGrid.CurrentRow;
                else if (donorsGrid.SelectedRows != null && donorsGrid.SelectedRows.Count > 0)
                    row = donorsGrid.SelectedRows[0];

                if (row == null) return null;

                var drv = row.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    var names = new[] { "DNum", "DId", "Id", "DonorId", "ID" };
                    foreach (var n in names)
                    {
                        if (drv.Row.Table.Columns.Contains(n))
                        {
                            var v = drv.Row[n];
                            if (v != DBNull.Value && int.TryParse(v.ToString(), out int idv))
                                return idv;
                        }
                    }
                }

                string[] candidates = new[] { "DNum", "DId", "Id", "DonorId", "ID" };
                foreach (var c in candidates)
                {
                    if (donorsGrid.Columns.Contains(c))
                    {
                        var cell = row.Cells[c];
                        if (cell != null && cell.Value != null && int.TryParse(cell.Value.ToString(), out int idv))
                        {
                            return idv;
                        }
                    }
                }

                if (row.Cells.Count > 0 && row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int firstId))
                    return firstId;

                return null;
            }
            catch (Exception ex)
            {
                Log("GetSelectedIdFromGrid error: " + ex);
                return null;
            }
        }

        // Valida los campos del formulario; devuelve false y mensaje con errores si hay problemas
        private bool ValidateInputs(out string message)
        {
            var sb = new StringBuilder();
            bool ok = true;

            // Nombre: requerido, mínimo 2 caracteres, sólo letras y espacios
            var name = DNameTb.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(name) || name.Length < 2 || !Regex.IsMatch(name, @"^[\p{L}\s'\-\.]+$"))
            {
                sb.AppendLine("- Nombre inválido (mínimo 2 caracteres, sólo letras/espacios).");
                try { DNameTb.BorderColor = System.Drawing.Color.Maroon; } catch { }
                ok = false;
            }
            else
            {
                try { DNameTb.BorderColor = System.Drawing.Color.Indigo; } catch { }
            }

            // Edad: requerido, numérico, rango 18-99
            var ageText = DAgeTb.Text?.Trim() ?? "";
            if (!int.TryParse(ageText, out int ageVal))
            {
                sb.AppendLine("- Edad inválida (debe ser un número).");
                try { DAgeTb.BorderColor = System.Drawing.Color.Maroon; } catch { }
                ok = false;
            }
            else if (ageVal < 18 || ageVal > 99)
            {
                sb.AppendLine("- Edad fuera de rango (18 - 99).");
                try { DAgeTb.BorderColor = System.Drawing.Color.Maroon; } catch { }
                ok = false;
            }
            else
            {
                try { DAgeTb.BorderColor = System.Drawing.Color.Indigo; } catch { }
            }

            // Género: requerido
            if (DGenCb.SelectedIndex == -1)
            {
                sb.AppendLine("- Seleccione un género.");
                ok = false;
            }

            // Grupo sanguíneo: requerido
            if (DBGroupCb.SelectedIndex == -1)
            {
                sb.AppendLine("- Seleccione un grupo sanguíneo.");
                ok = false;
            }

            // Teléfono: requerido, sólo dígitos, longitud razonable
            var phone = DPhoneTb.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^\d{7,15}$"))
            {
                sb.AppendLine("- Teléfono inválido (sólo dígitos, entre 7 y 15 caracteres).");
                try { DPhoneTb.BorderColor = System.Drawing.Color.Maroon; } catch { }
                ok = false;
            }
            else
            {
                try { DPhoneTb.BorderColor = System.Drawing.Color.Indigo; } catch { }
            }

            // Dirección: requerida, mínimo 5 caracteres
            var addr = DAddressTb.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(addr) || addr.Length < 5)
            {
                sb.AppendLine("- Dirección inválida (mínimo 5 caracteres).");
                try { DAddressTb.BackColor = System.Drawing.Color.MistyRose; } catch { }
                ok = false;
            }
            else
            {
                try { DAddressTb.BackColor = System.Drawing.Color.White; } catch { }
            }

            message = sb.ToString();
            return ok;
        }

        // Evento que se ejecuta cuando le damos click al botón guardar
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Nueva validación centralizada
            if (!ValidateInputs(out string validationMessage))
            {
                MessageBox.Show("Corrija los siguientes errores:\n\n" + validationMessage, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear DTO (edad ya validada)
            var donante = new DonanteDto
            {
                Nombre = DNameTb.Text.Trim(),
                Edad = int.Parse(DAgeTb.Text.Trim()),
                Genero = DGenCb.SelectedItem.ToString(),
                Telefono = DPhoneTb.Text.Trim(),
                Direccion = DAddressTb.Text.Trim(),
                GrupoSangre = DBGroupCb.SelectedItem.ToString()
            };

            if (_selectedId == null)
            {
                // Insert
                if (_service.Insert(donante, out string error))
                {
                    MessageBox.Show("Donante guardado con éxito");
                    Reset();
                    LoadDonors();
                }
                else
                {
                    MessageBox.Show("Error al guardar donante: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Update
                if (_service.Update(donante, _selectedId.Value, out string error))
                {
                    MessageBox.Show("Donante actualizado con éxito");
                    Reset();
                    LoadDonors();
                }
                else
                {
                    MessageBox.Show("Error al actualizar donante: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Evento eliminar (mejorado y más robusto)
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var id = GetSelectedIdFromGrid();
            Log("Eliminar: id extraído = " + (id.HasValue ? id.Value.ToString() : "null"));

            if (!id.HasValue)
            {
                string info = "No se pudo determinar el ID del donante seleccionado.\n\n" +
                              "Posibles causas:\n" +
                              "- La columna PK no está en la grilla con nombre esperado (DId/Id/DonorId).\n" +
                              "- El valor de la celda es NULL o no numérico.\n" +
                              "- La fila no está realmente seleccionada.\n\n" +
                              "Abre la ventana Output (Debug) para ver logs o selecciona la fila correcta.";
                MessageBox.Show(info, "Eliminar donante - ID no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idAEliminar = id.Value;
            var confirm = MessageBox.Show($"¿Desea eliminar el donante con ID = {idAEliminar} ?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            Log("Eliminar: llamando a DonanteService.Delete con id=" + idAEliminar);
            if (_service.Delete(idAEliminar, out string error))
            {
                MessageBox.Show("Donante eliminado.");
                Reset();
                LoadDonors();
            }
            else
            {
                MessageBox.Show("Error al eliminar donante: " + error, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Eliminar error: " + error);
            }
        }

        // Evento limpiar
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // KeyPress: permitir sólo dígitos y teclas de control para teléfono
        private void DPhoneTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // KeyPress: permitir sólo dígitos y teclas de control para edad
        private void DAgeTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // KeyPress: permitir letras, espacios y signos comunes en nombres
        private void DNameTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || e.KeyChar == '\'' || e.KeyChar == '-' || e.KeyChar == '.')
                return;
            e.Handled = true;
        }

        // Helper seguro para leer celdas por varios nombres posibles
        private string ReadCellAsString(DataGridViewRow row, params string[] names)
        {
            foreach (var name in names)
            {
                if (donorsGrid.Columns.Contains(name))
                {
                    var cell = row.Cells[name];
                    if (cell != null && cell.Value != null) return cell.Value.ToString();
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        // Cuando cambian la selección en el grid, cargamos los datos en el formulario para editar
        private void donorsGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (donorsGrid.SelectedRows.Count == 0)
            {
                Reset();
                return;
            }

            var row = donorsGrid.SelectedRows[0];

            _selectedId = null;
            string idStr = ReadCellAsString(row, "DId", "Id", "ID", "DonorId", "donorid");
            if (!string.IsNullOrWhiteSpace(idStr) && int.TryParse(idStr, out int idVal))
            {
                _selectedId = idVal;
            }
            else
            {
                var drv = row.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    foreach (var c in new[] { "DId", "Id", "DonorId", "ID" })
                    {
                        if (drv.Row.Table.Columns.Contains(c))
                        {
                            var v = drv.Row[c];
                            if (v != DBNull.Value && int.TryParse(v.ToString(), out int pv))
                            {
                                _selectedId = pv;
                                break;
                            }
                        }
                    }
                }
            }

            DNameTb.Text = ReadCellAsString(row, "DName", "Name", "Nombre");
            DAgeTb.Text = ReadCellAsString(row, "DAge", "Age", "Edad");
            DPhoneTb.Text = ReadCellAsString(row, "DPhone", "Phone", "Telefono", "Teléfono");
            DAddressTb.Text = ReadCellAsString(row, "DAddress", "Address", "Dirección");

            var gen = ReadCellAsString(row, "DGender", "Gender", "Genero", "Género");
            if (!string.IsNullOrEmpty(gen) && DGenCb.Items.Contains(gen)) DGenCb.SelectedItem = gen;
            else DGenCb.SelectedIndex = -1;

            var grp = ReadCellAsString(row, "DBGroup", "BGroup", "Group", "Grupo");
            if (!string.IsNullOrEmpty(grp) && DBGroupCb.Items.Contains(grp)) DBGroupCb.SelectedItem = grp;
            else DBGroupCb.SelectedIndex = -1;

            guna2Button1.Text = _selectedId == null ? "Guardar" : "Actualizar";

            Log("SelectionChanged: _selectedId = " + (_selectedId.HasValue ? _selectedId.Value.ToString() : "null"));
        }

        // Resto de eventos UI (sin cambios)
        private void label12_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void DAgeTb_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void DNameTb_TextChanged(object sender, EventArgs e) { }
        private void DPhoneTb_TextChanged(object sender, EventArgs e) { }

        private void Donante_Load(object sender, EventArgs e)
        {
            // Cargar listado al iniciar
            LoadDonors();
        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}