using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto_de_Seminario
{
    public partial class GestionarUsuariosForm : Form
    {
        private int? _editingUserId = null;

        public GestionarUsuariosForm()
        {
            InitializeComponent();
            LoadUsuarios();
            SetupDataGridView();
            dgvUsuarios.CellDoubleClick += dgvUsuarios_CellDoubleClick;
        }

        #region Carga y estilo de grilla
        private void LoadUsuarios()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    const string query = @"
                        SELECT 
                            id_usuario,
                            username,
                            tipo
                        FROM Usuarios
                        ORDER BY username;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dgvUsuarios.DataSource = dt;
                    }
                }

                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvUsuarios?.Columns == null || dgvUsuarios.Columns.Count == 0) return;

            try
            {
                if (dgvUsuarios.Columns.Contains("id_usuario"))
                {
                    dgvUsuarios.Columns["id_usuario"].HeaderText = "ID";
                    dgvUsuarios.Columns["id_usuario"].Width = 60;
                }

                if (dgvUsuarios.Columns.Contains("username"))
                {
                    dgvUsuarios.Columns["username"].HeaderText = "Usuario";
                    dgvUsuarios.Columns["username"].Width = 150;
                }

                if (dgvUsuarios.Columns.Contains("tipo"))
                {
                    dgvUsuarios.Columns["tipo"].HeaderText = "Tipo";
                    dgvUsuarios.Columns["tipo"].Width = 100;
                }

                dgvUsuarios.BackgroundColor = Color.White;
                dgvUsuarios.BorderStyle = BorderStyle.None;
                dgvUsuarios.EnableHeadersVisualStyles = false;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                dgvUsuarios.ColumnHeadersHeight = 35;
                dgvUsuarios.RowHeadersVisible = false;
                dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
                dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios.MultiSelect = false;
                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGridView: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Utilidades
        private void LimpiarFormulario()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            rbAdmin.Checked = true;
            _editingUserId = null;
            btnGuardar.Text = "Guardar";
            label5.Text = "Tipo de Usuario:";
        }

        private static string HashPassword(string passwordPlain)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(passwordPlain));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>
        /// Busca un técnico por nombre exacto. Si existe, devuelve su id; si no, null.
        /// </summary>
        private static long? FindTechnicianIdByName(SQLiteConnection conn, string nombre)
        {
            using (var cmd = new SQLiteCommand("SELECT id_technician FROM Technicians WHERE nombre = @n LIMIT 1;", conn))
            {
                cmd.Parameters.AddWithValue("@n", nombre);
                var r = cmd.ExecuteScalar();
                if (r == null || r == DBNull.Value) return null;
                return Convert.ToInt64(r);
            }
        }

        /// <summary>
        /// Crea un técnico con el nombre indicado y devuelve su id.
        /// </summary>
        private static long CreateTechnician(SQLiteConnection conn, string nombre, string telefono = "")
        {
            using (var cmd = new SQLiteCommand(
                "INSERT INTO Technicians (nombre, telefono) VALUES (@n, @t); SELECT last_insert_rowid();", conn))
            {
                cmd.Parameters.AddWithValue("@n", nombre);
                cmd.Parameters.AddWithValue("@t", telefono ?? "");
                return (long)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Asegura que exista un técnico con ese nombre y devuelve su id (creándolo si no existe).
        /// </summary>
        private static long EnsureTechnician(SQLiteConnection conn, string nombre)
        {
            var existing = FindTechnicianIdByName(conn, nombre);
            if (existing.HasValue) return existing.Value;
            return CreateTechnician(conn, nombre);
        }

        /// <summary>
        /// Devuelve el id_technician del usuario o null si no tiene.
        /// </summary>
        private static long? GetUserTechnicianId(SQLiteConnection conn, int idUsuario)
        {
            using (var cmd = new SQLiteCommand("SELECT id_technician FROM Usuarios WHERE id_usuario = @id;", conn))
            {
                cmd.Parameters.AddWithValue("@id", idUsuario);
                var r = cmd.ExecuteScalar();
                if (r == null || r == DBNull.Value) return null;
                return Convert.ToInt64(r);
            }
        }
        #endregion

        #region Guardar / Eliminar / Editar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string username = (txtUsername.Text ?? "").Trim();
            string password = (txtPassword.Text ?? "").Trim();
            string tipo = rbAdmin.Checked ? "Admin" : "Technician";

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Por favor ingrese el nombre de usuario.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Asegurar FKs activas (por si acaso)
                    using (var fk = new SQLiteCommand("PRAGMA foreign_keys = ON;", conn))
                        fk.ExecuteNonQuery();

                    if (_editingUserId == null)
                    {
                        // Insertar usuario nuevo
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            MessageBox.Show("Ingrese una contraseña.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPassword.Focus();
                            return;
                        }

                        const string insertSql = @"
                            INSERT INTO Usuarios (username, password, tipo, id_technician)
                            VALUES (@u, @p, @t, NULL);";
                        using (var cmd = new SQLiteCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@u", username);
                            cmd.Parameters.AddWithValue("@p", HashPassword(password));
                            cmd.Parameters.AddWithValue("@t", tipo);
                            cmd.ExecuteNonQuery();
                        }

                        // Si es Técnico, crear/vincular técnico
                        if (tipo == "Technician")
                        {
                            long techId = EnsureTechnician(conn, username);
                            using (var up = new SQLiteCommand(
                                "UPDATE Usuarios SET id_technician = @tid WHERE username = @u;", conn))
                            {
                                up.Parameters.AddWithValue("@tid", techId);
                                up.Parameters.AddWithValue("@u", username);
                                up.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Usuario creado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Actualizar usuario existente
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            const string updateSql = @"
                                UPDATE Usuarios
                                SET username = @u, tipo = @t
                                WHERE id_usuario = @id;";
                            using (var cmd = new SQLiteCommand(updateSql, conn))
                            {
                                cmd.Parameters.AddWithValue("@u", username);
                                cmd.Parameters.AddWithValue("@t", tipo);
                                cmd.Parameters.AddWithValue("@id", _editingUserId.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            const string updateSql = @"
                                UPDATE Usuarios
                                SET username = @u, password = @p, tipo = @t
                                WHERE id_usuario = @id;";
                            using (var cmd = new SQLiteCommand(updateSql, conn))
                            {
                                cmd.Parameters.AddWithValue("@u", username);
                                cmd.Parameters.AddWithValue("@p", HashPassword(password));
                                cmd.Parameters.AddWithValue("@t", tipo);
                                cmd.Parameters.AddWithValue("@id", _editingUserId.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Mantener coherencia con Technicians según el tipo elegido
                        if (tipo == "Technician")
                        {
                            // Si no tiene técnico asociado, se lo creamos/asignamos
                            var currentTid = GetUserTechnicianId(conn, _editingUserId.Value);
                            if (!currentTid.HasValue)
                            {
                                long techId = EnsureTechnician(conn, username);
                                using (var up = new SQLiteCommand(
                                    "UPDATE Usuarios SET id_technician = @tid WHERE id_usuario = @id;", conn))
                                {
                                    up.Parameters.AddWithValue("@tid", techId);
                                    up.Parameters.AddWithValue("@id", _editingUserId.Value);
                                    up.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            // Si ahora es Admin, desvinculamos técnico
                            using (var up = new SQLiteCommand(
                                "UPDATE Usuarios SET id_technician = NULL WHERE id_usuario = @id;", conn))
                            {
                                up.Parameters.AddWithValue("@id", _editingUserId.Value);
                                up.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Usuario actualizado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadUsuarios();
                LimpiarFormulario();
            }
            catch (SQLiteException ex) when (ex.Message.Contains("UNIQUE constraint failed"))
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor elija otro.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("No se puede eliminar el usuario administrador principal", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show($"¿Está seguro de eliminar al usuario '{username}'?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    const string query = "DELETE FROM Usuarios WHERE id_usuario = @id_usuario;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Usuario eliminado exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsuarios();
                            LimpiarFormulario();
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

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsuarios.CurrentRow == null) return;

            try
            {
                var row = dgvUsuarios.Rows[e.RowIndex];

                _editingUserId = Convert.ToInt32(row.Cells["id_usuario"].Value);
                txtUsername.Text = row.Cells["username"].Value?.ToString() ?? "";
                txtPassword.Clear();
                string tipo = row.Cells["tipo"].Value?.ToString() ?? "Admin";

                if (tipo == "Technician")
                    rbTechnician.Checked = true;
                else
                    rbAdmin.Checked = true;

                btnGuardar.Text = "Actualizar";
                label5.Text = "Editar Usuario:";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el usuario para edición: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
        private void panelForm_Paint(object sender, PaintEventArgs e) { }
        private void panelHeader_Paint(object sender, PaintEventArgs e) { }
    }
}