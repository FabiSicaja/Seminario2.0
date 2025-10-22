using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto_de_Seminario
{
    public partial class GestionarClientesForm : Form
    {
        public GestionarClientesForm()
        {
            InitializeComponent();
            LoadClientes();
            SetupDataGridView();
        }

        private void LoadClientes()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            id_cliente,
                            nombre,
                            direccion,
                            nit,
                            contactos
                        FROM Clientes 
                        ORDER BY nombre;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvClientes.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvClientes.Columns.Count == 0) return;

            try
            {
                // Configurar columnas visibles y anchos
                if (dgvClientes.Columns.Contains("id_cliente"))
                {
                    dgvClientes.Columns["id_cliente"].HeaderText = "ID";
                    dgvClientes.Columns["id_cliente"].Width = 60;
                }

                if (dgvClientes.Columns.Contains("nombre"))
                {
                    dgvClientes.Columns["nombre"].HeaderText = "Nombre";
                    dgvClientes.Columns["nombre"].Width = 180;
                }

                if (dgvClientes.Columns.Contains("direccion"))
                {
                    dgvClientes.Columns["direccion"].HeaderText = "Dirección";
                    dgvClientes.Columns["direccion"].Width = 220;
                }

                if (dgvClientes.Columns.Contains("nit"))
                {
                    dgvClientes.Columns["nit"].HeaderText = "NIT";
                    dgvClientes.Columns["nit"].Width = 120;
                }

                if (dgvClientes.Columns.Contains("contactos"))
                {
                    dgvClientes.Columns["contactos"].HeaderText = "Contactos";
                    dgvClientes.Columns["contactos"].Width = 220;
                }

                ApplyCommonDataGridViewStyle(dgvClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGridView: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyCommonDataGridViewStyle(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;

            // Encabezados
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Filas
            dgv.RowHeadersVisible = false;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtNit.Clear();
            txtContactos.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones mínimas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del cliente", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor ingrese la dirección", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNit.Text))
            {
                MessageBox.Show("Por favor ingrese el NIT", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNit.Focus();
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Clientes (nombre, direccion, nit, contactos)
                        VALUES (@nombre, @direccion, @nit, @contactos);";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text.Trim());
                        cmd.Parameters.AddWithValue("@nit", txtNit.Text.Trim());
                        cmd.Parameters.AddWithValue("@contactos", string.IsNullOrWhiteSpace(txtContactos.Text) ? DBNull.Value : (object)txtContactos.Text.Trim());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cliente creado exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarFormulario();
                            LoadClientes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells["id_cliente"].Value);
            string nombre = dgvClientes.CurrentRow.Cells["nombre"].Value.ToString();

            var result = MessageBox.Show($"¿Está seguro de eliminar al cliente '{nombre}'?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Clientes WHERE id_cliente = @id_cliente;";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Cliente eliminado exitosamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadClientes();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            // opcional
        }
    }
}
