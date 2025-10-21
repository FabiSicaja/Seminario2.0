using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto_de_Seminario
{
    public partial class GestionarProveedoresForm : Form
    {
        public GestionarProveedoresForm()
        {
            InitializeComponent();
            LoadProveedores();
            SetupDataGridView();
        }

        private void LoadProveedores()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            id_proveedor,
                            nombre,
                            contacto,
                            telefono,
                            email,
                            direccion,
                            productos_servicios,
                            fecha_registro
                        FROM Proveedores 
                        ORDER BY nombre";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvProveedores.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvProveedores.Columns.Count == 0) return;

            try
            {
                // Configurar columnas
                if (dgvProveedores.Columns.Contains("id_proveedor"))
                {
                    dgvProveedores.Columns["id_proveedor"].HeaderText = "ID";
                    dgvProveedores.Columns["id_proveedor"].Width = 50;
                }

                if (dgvProveedores.Columns.Contains("nombre"))
                {
                    dgvProveedores.Columns["nombre"].HeaderText = "Nombre";
                    dgvProveedores.Columns["nombre"].Width = 150;
                }

                if (dgvProveedores.Columns.Contains("contacto"))
                {
                    dgvProveedores.Columns["contacto"].HeaderText = "Contacto";
                    dgvProveedores.Columns["contacto"].Width = 120;
                }

                if (dgvProveedores.Columns.Contains("telefono"))
                {
                    dgvProveedores.Columns["telefono"].HeaderText = "Teléfono";
                    dgvProveedores.Columns["telefono"].Width = 120;
                }

                if (dgvProveedores.Columns.Contains("email"))
                {
                    dgvProveedores.Columns["email"].HeaderText = "Email";
                    dgvProveedores.Columns["email"].Width = 150;
                }

                if (dgvProveedores.Columns.Contains("direccion"))
                {
                    dgvProveedores.Columns["direccion"].HeaderText = "Dirección";
                    dgvProveedores.Columns["direccion"].Width = 180;
                }

                if (dgvProveedores.Columns.Contains("productos_servicios"))
                {
                    dgvProveedores.Columns["productos_servicios"].HeaderText = "Productos/Servicios";
                    dgvProveedores.Columns["productos_servicios"].Width = 200;
                }

                if (dgvProveedores.Columns.Contains("fecha_registro"))
                {
                    dgvProveedores.Columns["fecha_registro"].HeaderText = "Fecha Registro";
                    dgvProveedores.Columns["fecha_registro"].Width = 120;
                }

                // Aplicar estilos consistentes
                ApplyCommonDataGridViewStyle(dgvProveedores);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGridView: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtContacto.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtProductos.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del proveedor", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Proveedores (nombre, contacto, telefono, email, direccion, productos_servicios, fecha_registro)
                        VALUES (@nombre, @contacto, @telefono, @email, @direccion, @productos_servicios, @fecha_registro)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@contacto", string.IsNullOrWhiteSpace(txtContacto.Text) ? DBNull.Value : (object)txtContacto.Text.Trim());
                        cmd.Parameters.AddWithValue("@telefono", string.IsNullOrWhiteSpace(txtTelefono.Text) ? DBNull.Value : (object)txtTelefono.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : (object)txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@direccion", string.IsNullOrWhiteSpace(txtDireccion.Text) ? DBNull.Value : (object)txtDireccion.Text.Trim());
                        cmd.Parameters.AddWithValue("@productos_servicios", string.IsNullOrWhiteSpace(txtProductos.Text) ? DBNull.Value : (object)txtProductos.Text.Trim());
                        cmd.Parameters.AddWithValue("@fecha_registro", DateTime.Now.ToString("yyyy-MM-dd"));

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Proveedor creado exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarFormulario();
                            LoadProveedores();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear proveedor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProveedor = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["id_proveedor"].Value);
            string nombre = dgvProveedores.CurrentRow.Cells["nombre"].Value.ToString();

            var result = MessageBox.Show($"¿Está seguro de eliminar al proveedor '{nombre}'?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Proveedores WHERE id_proveedor = @id_proveedor";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Proveedor eliminado exitosamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadProveedores();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar proveedor: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ApplyCommonDataGridViewStyle(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;

            // Estilo de headers
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Estilo de filas
            dgv.RowHeadersVisible = false;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }
    }
}
