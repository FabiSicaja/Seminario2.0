using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto_de_Seminario
{
    public partial class GestionarUsuariosForm : Form
    {
        public GestionarUsuariosForm()
        {
            InitializeComponent();
            LoadTechnicians();
            LoadUsuarios();
            SetupDataGridView();
        }

        private void LoadTechnicians()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_technician, nombre FROM Technicians ORDER BY nombre";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            cmbTechnician.DataSource = dt;
                            cmbTechnician.DisplayMember = "nombre";
                            cmbTechnician.ValueMember = "id_technician";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar técnicos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsuarios()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            u.id_usuario,
                            u.username,
                            u.tipo,
                            t.nombre as technician_name,
                            u.id_technician
                        FROM Usuarios u
                        LEFT JOIN Technicians t ON u.id_technician = t.id_technician
                        ORDER BY u.username";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvUsuarios.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvUsuarios.Columns.Count == 0) return;

            try
            {
                // Configurar columnas
                if (dgvUsuarios.Columns.Contains("id_usuario"))
                {
                    dgvUsuarios.Columns["id_usuario"].HeaderText = "ID";
                    dgvUsuarios.Columns["id_usuario"].Width = 50;
                }

                if (dgvUsuarios.Columns.Contains("username"))
                {
                    dgvUsuarios.Columns["username"].HeaderText = "Usuario";
                    dgvUsuarios.Columns["username"].Width = 120;
                }

                if (dgvUsuarios.Columns.Contains("tipo"))
                {
                    dgvUsuarios.Columns["tipo"].HeaderText = "Tipo";
                    dgvUsuarios.Columns["tipo"].Width = 100;
                }

                if (dgvUsuarios.Columns.Contains("technician_name"))
                {
                    dgvUsuarios.Columns["technician_name"].HeaderText = "Técnico Asociado";
                    dgvUsuarios.Columns["technician_name"].Width = 150;
                }

                // Aplicar estilos
                dgvUsuarios.BackgroundColor = Color.White;
                dgvUsuarios.BorderStyle = BorderStyle.None;
                dgvUsuarios.EnableHeadersVisualStyles = false;

                // Estilo de headers
                dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                dgvUsuarios.ColumnHeadersHeight = 35;
                dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                // Estilo de filas
                dgvUsuarios.RowHeadersVisible = false;
                dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
                dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGridView: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            rbAdmin.Checked = true;
            cmbTechnician.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
            string username = dgvUsuarios.CurrentRow.Cells["username"].Value.ToString();

            if (username == "admin")
            {
                MessageBox.Show("No se puede eliminar el usuario administrador principal", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show($"¿Está seguro de eliminar al usuario '{username}'?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Usuarios WHERE id_usuario = @id_usuario";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Usuario eliminado exitosamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadUsuarios();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Usuarios (username, password, tipo, id_technician)
                        VALUES (@username, @password, @tipo, @id_technician)";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text); // En producción usar hash
                        cmd.Parameters.AddWithValue("@tipo", rbAdmin.Checked ? "Admin" : "Technician");
                        cmd.Parameters.AddWithValue("@id_technician",
                            rbTechnician.Checked ? cmbTechnician.SelectedValue : (object)DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Usuario creado exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarFormulario();
                            LoadUsuarios();
                        }
                    }
                }
            }
            catch (SQLiteException ex) when (ex.Message.Contains("UNIQUE constraint failed"))
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor elija otro.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            cmbTechnician.Enabled = false;
        }

        private void rbTechnician_CheckedChanged(object sender, EventArgs e)
        {
            cmbTechnician.Enabled = rbTechnician.Checked;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
