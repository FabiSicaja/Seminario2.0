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
        private int? _editingUserId = null;
        private int? _editingUserTechId = null; // para saber si está enlazado a un técnico

        public GestionarUsuariosForm()
        {
            InitializeComponent();
            LoadUsuarios();
            SetupDataGridView();
            dgvUsuarios.CellDoubleClick += dgvUsuarios_CellDoubleClick;
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
                            id_usuario,
                            username,
                            tipo,
                            id_technician
                        FROM Usuarios
                        ORDER BY username;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
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
            if (dgvUsuarios.Columns.Count == 0) return;

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
                    dgvUsuarios.Columns["tipo"].Width = 110;
                }

                if (dgvUsuarios.Columns.Contains("id_technician"))
                {
                    dgvUsuarios.Columns["id_technician"].HeaderText = "ID Técnico";
                    dgvUsuarios.Columns["id_technician"].Width = 100;
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
                MessageBox.Show($"Error al configurar la tabla: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            rbAdmin.Checked = true;
            _editingUserId = null;
            _editingUserTechId = null;
            btnGuardar.Text = "Guardar";
            label5.Text = "Tipo de Usuario:";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string username = (txtUsername.Text ?? "").Trim();
            string password = (txtPassword.Text ?? "").Trim();
            string tipo = rbAdmin.Checked ? "Admin" : "Technician";

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Ingrese el nombre de usuario.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Si es creación
                    if (_editingUserId == null)
                    {
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            MessageBox.Show("Ingrese una contraseña.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPassword.Focus();
                            return;
                        }

                        using (var tx = conn.BeginTransaction())
                        {
                            int? techIdToLink = null;

                            if (tipo == "Technician")
                            {
                                // Crear técnico automáticamente
                                using (var cmdT = new SQLiteCommand(
                                    "INSERT INTO Technicians (nombre, telefono) VALUES (@n, NULL);", conn, tx))
                                {
                                    cmdT.Parameters.AddWithValue("@n", username);
                                    cmdT.ExecuteNonQuery();
                                }

                                using (var cmdGet = new SQLiteCommand("SELECT last_insert_rowid();", conn, tx))
                                {
                                    techIdToLink = Convert.ToInt32(cmdGet.ExecuteScalar());
                                }
                            }

                            using (var cmdU = new SQLiteCommand(@"
                                INSERT INTO Usuarios (username, password, tipo, id_technician)
                                VALUES (@u, @p, @t, @idT);", conn, tx))
                            {
                                cmdU.Parameters.AddWithValue("@u", username);
                                cmdU.Parameters.AddWithValue("@p", password); // sin hash, tal como pediste
                                cmdU.Parameters.AddWithValue("@t", tipo);
                                if (techIdToLink.HasValue)
                                    cmdU.Parameters.AddWithValue("@idT", techIdToLink.Value);
                                else
                                    cmdU.Parameters.AddWithValue("@idT", DBNull.Value);

                                cmdU.ExecuteNonQuery();
                            }

                            tx.Commit();
                        }

                        MessageBox.Show("Usuario creado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Edición
                        using (var tx = conn.BeginTransaction())
                        {
                            // Traer el usuario actual por si lo abrieron hace rato
                            int? currentTechId = null;
                            string currentTipo = "Admin";

                            using (var cmdSel = new SQLiteCommand(
                                "SELECT tipo, id_technician FROM Usuarios WHERE id_usuario = @id;", conn, tx))
                            {
                                cmdSel.Parameters.AddWithValue("@id", _editingUserId.Value);
                                using (var r = cmdSel.ExecuteReader())
                                {
                                    if (r.Read())
                                    {
                                        currentTipo = r["tipo"].ToString();
                                        currentTechId = r["id_technician"] == DBNull.Value
                                            ? (int?)null
                                            : Convert.ToInt32(r["id_technician"]);
                                    }
                                }
                            }

                            // Si pasa de Admin -> Technician y no tiene tecnico, crear y enlazar
                            if (tipo == "Technician" && currentTechId == null)
                            {
                                int newTechId;
                                using (var cmdT = new SQLiteCommand(
                                    "INSERT INTO Technicians (nombre, telefono) VALUES (@n, NULL);", conn, tx))
                                {
                                    cmdT.Parameters.AddWithValue("@n", username);
                                    cmdT.ExecuteNonQuery();
                                }
                                using (var cmdGet = new SQLiteCommand("SELECT last_insert_rowid();", conn, tx))
                                {
                                    newTechId = Convert.ToInt32(cmdGet.ExecuteScalar());
                                }

                                using (var cmdU = new SQLiteCommand(@"
                                    UPDATE Usuarios
                                    SET username = @u,
                                        password = COALESCE(NULLIF(@p,''), password),
                                        tipo = @t,
                                        id_technician = @tid
                                    WHERE id_usuario = @id;", conn, tx))
                                {
                                    cmdU.Parameters.AddWithValue("@u", username);
                                    cmdU.Parameters.AddWithValue("@p", password); // si viene vacío, no cambia
                                    cmdU.Parameters.AddWithValue("@t", tipo);
                                    cmdU.Parameters.AddWithValue("@tid", newTechId);
                                    cmdU.Parameters.AddWithValue("@id", _editingUserId.Value);
                                    cmdU.ExecuteNonQuery();
                                }
                            }
                            // Si pasa de Technician -> Admin y tenía técnico, limpiar y desenlazar
                            else if (tipo == "Admin" && currentTechId != null)
                            {
                                int tid = currentTechId.Value;

                                // Quitar asignaciones y referencias
                                using (var cmdOT = new SQLiteCommand(
                                    "DELETE FROM OrdenTechnicians WHERE id_technician = @tid;", conn, tx))
                                {
                                    cmdOT.Parameters.AddWithValue("@tid", tid);
                                    cmdOT.ExecuteNonQuery();
                                }

                                using (var cmdG = new SQLiteCommand(
                                    "UPDATE Gastos SET id_technician = NULL WHERE id_technician = @tid;", conn, tx))
                                {
                                    cmdG.Parameters.AddWithValue("@tid", tid);
                                    cmdG.ExecuteNonQuery();
                                }

                                using (var cmdDelT = new SQLiteCommand(
                                    "DELETE FROM Technicians WHERE id_technician = @tid;", conn, tx))
                                {
                                    cmdDelT.Parameters.AddWithValue("@tid", tid);
                                    cmdDelT.ExecuteNonQuery();
                                }

                                using (var cmdU = new SQLiteCommand(@"
                                    UPDATE Usuarios
                                    SET username = @u,
                                        password = COALESCE(NULLIF(@p,''), password),
                                        tipo = @t,
                                        id_technician = NULL
                                    WHERE id_usuario = @id;", conn, tx))
                                {
                                    cmdU.Parameters.AddWithValue("@u", username);
                                    cmdU.Parameters.AddWithValue("@p", password); // si vacío, no cambia
                                    cmdU.Parameters.AddWithValue("@t", tipo);
                                    cmdU.Parameters.AddWithValue("@id", _editingUserId.Value);
                                    cmdU.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Mismo tipo; solo actualizar datos básicos
                                using (var cmdU = new SQLiteCommand(@"
                                    UPDATE Usuarios
                                    SET username = @u,
                                        password = COALESCE(NULLIF(@p,''), password),
                                        tipo = @t
                                    WHERE id_usuario = @id;", conn, tx))
                                {
                                    cmdU.Parameters.AddWithValue("@u", username);
                                    cmdU.Parameters.AddWithValue("@p", password); // si vacío, no cambia
                                    cmdU.Parameters.AddWithValue("@t", tipo);
                                    cmdU.Parameters.AddWithValue("@id", _editingUserId.Value);
                                    cmdU.ExecuteNonQuery();
                                }
                            }

                            tx.Commit();
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

            var result = MessageBox.Show($"¿Eliminar al usuario '{username}' y su técnico asociado (si tiene)?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    using (var tx = conn.BeginTransaction())
                    {
                        int? techId = null;
                        using (var cmdSel = new SQLiteCommand(
                            "SELECT id_technician FROM Usuarios WHERE id_usuario = @id;", conn, tx))
                        {
                            cmdSel.Parameters.AddWithValue("@id", idUsuario);
                            var val = cmdSel.ExecuteScalar();
                            techId = (val == DBNull.Value || val == null) ? (int?)null : Convert.ToInt32(val);
                        }

                        // Si hay técnico enlazado, limpiar sus referencias y borrarlo
                        if (techId != null)
                        {
                            int tid = techId.Value;

                            using (var cmdOT = new SQLiteCommand(
                                "DELETE FROM OrdenTechnicians WHERE id_technician = @tid;", conn, tx))
                            {
                                cmdOT.Parameters.AddWithValue("@tid", tid);
                                cmdOT.ExecuteNonQuery();
                            }

                            using (var cmdG = new SQLiteCommand(
                                "UPDATE Gastos SET id_technician = NULL WHERE id_technician = @tid;", conn, tx))
                            {
                                cmdG.Parameters.AddWithValue("@tid", tid);
                                cmdG.ExecuteNonQuery();
                            }

                            using (var cmdDelT = new SQLiteCommand(
                                "DELETE FROM Technicians WHERE id_technician = @tid;", conn, tx))
                            {
                                cmdDelT.Parameters.AddWithValue("@tid", tid);
                                cmdDelT.ExecuteNonQuery();
                            }
                        }

                        // Borrar el usuario
                        using (var cmdDelU = new SQLiteCommand(
                            "DELETE FROM Usuarios WHERE id_usuario = @id;", conn, tx))
                        {
                            cmdDelU.Parameters.AddWithValue("@id", idUsuario);
                            cmdDelU.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Usuario (y técnico asociado, si aplicaba) eliminado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUsuarios();
                LimpiarFormulario();
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
                _editingUserTechId = row.Cells["id_technician"].Value == DBNull.Value
                    ? (int?)null
                    : Convert.ToInt32(row.Cells["id_technician"].Value);

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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelForm_Paint(object sender, PaintEventArgs e) { }
        private void panelHeader_Paint(object sender, PaintEventArgs e) { }
    }
}