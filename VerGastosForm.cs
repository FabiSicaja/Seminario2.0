using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;
using Microsoft.VisualBasic; // Para Interaction.InputBox

namespace Proyecto
{
    public partial class VerGastosForm : Form
    {
        private readonly int _idOrden;
        private readonly bool _soloMios;

        // Se dispara cuando se agregan/eliminan gastos para refrescar totales en el formulario que abrió
        public event Action GastosChanged;

        /// <summary>
        /// Admin u otros roles: ver todos los gastos de la orden.
        /// Técnico: pasar soloMios = true para que solo vea sus propios gastos.
        /// </summary>
        public VerGastosForm(int idOrden, bool soloMios = false)
        {
            InitializeComponent();
            _idOrden = idOrden;
            _soloMios = soloMios;
            this.Text = $"Gastos - Orden #{_idOrden}";

            // Mejoras visuales
            dgvGastos.AutoGenerateColumns = true;
            dgvGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGastos.MultiSelect = false;
            dgvGastos.ReadOnly = true;
            dgvGastos.AllowUserToAddRows = false;
            dgvGastos.AllowUserToDeleteRows = false;
            dgvGastos.BackgroundColor = Color.White;
            dgvGastos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            LoadGastos();
        }

        private void EnsureLogTable(SQLiteConnection conn)
        {
            const string createLog = @"
                CREATE TABLE IF NOT EXISTS GastoEliminacionesLog (
                    id_log        INTEGER PRIMARY KEY AUTOINCREMENT,
                    id_gasto      INTEGER NOT NULL,
                    id_orden      INTEGER NOT NULL,
                    id_technician INTEGER,
                    fecha         TEXT NOT NULL,
                    motivo        TEXT NOT NULL
                );";
            using (var cmd = new SQLiteCommand(createLog, conn))
                cmd.ExecuteNonQuery();
        }

        private void LoadGastos()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Base: unión con Technicians para mostrar nombre del técnico
                    var sql = @"
                        SELECT 
                            g.id_gasto,
                            g.tipo_gasto,
                            g.fecha,
                            g.serie,
                            g.no_factura,
                            g.nit,
                            g.proveedor,
                            g.descripcion,
                            g.monto,
                            g.tipo_combustible,
                            g.galonaje,
                            -- Nombre del técnico que registró el gasto
                            COALESCE(t.nombre,'') AS tecnico,
                            -- IVA/Subtotal calculados al vuelo
                            ROUND(
                                CASE WHEN LOWER(g.tipo_gasto) = 'combustible' THEN 0 
                                     ELSE g.monto * 0.12 
                                END, 2
                            ) AS iva,
                            ROUND(
                                g.monto - CASE WHEN LOWER(g.tipo_gasto) = 'combustible' THEN 0 
                                               ELSE g.monto * 0.12 
                                          END, 2
                            ) AS subtotal
                        FROM Gastos g
                        LEFT JOIN Technicians t ON t.id_technician = g.id_technician
                        WHERE g.id_orden = @idOrden";

                    // Filtro: si el técnico solo debe ver sus gastos
                    if (_soloMios && Session.TechnicianId.HasValue)
                        sql += " AND g.id_technician = @idTech";

                    sql += " ORDER BY g.fecha DESC, g.id_gasto DESC;";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", _idOrden);
                        if (_soloMios && Session.TechnicianId.HasValue)
                            cmd.Parameters.AddWithValue("@idTech", Session.TechnicianId.Value);

                        using (var ad = new SQLiteDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            ad.Fill(dt);
                            dgvGastos.DataSource = dt;
                            FormatDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar gastos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvGastos.Columns.Count == 0) return;

            dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvGastos.Columns["monto"] != null)
                dgvGastos.Columns["monto"].DefaultCellStyle.Format = "N2";
            if (dgvGastos.Columns["iva"] != null)
                dgvGastos.Columns["iva"].DefaultCellStyle.Format = "N2";
            if (dgvGastos.Columns["subtotal"] != null)
                dgvGastos.Columns["subtotal"].DefaultCellStyle.Format = "N2";

            if (dgvGastos.Columns["tecnico"] != null)
            {
                dgvGastos.Columns["tecnico"].HeaderText = "Técnico";
                dgvGastos.Columns["tecnico"].Width = 160;
            }
        }

        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {
            var f = new IngresarGastoForm(_idOrden);
            f.FormClosed += (s, args) =>
            {
                LoadGastos();
                GastosChanged?.Invoke(); // refrescar totales en el formulario que abrió
            };
            f.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGastos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un gasto para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idGasto = Convert.ToInt32(dgvGastos.CurrentRow.Cells["id_gasto"].Value);
            string descripcion = dgvGastos.CurrentRow.Cells["descripcion"].Value?.ToString() ?? "";

            // Si el técnico solo puede ver/gestionar los suyos, valida autoría
            if (_soloMios && Session.TechnicianId.HasValue)
            {
                int idTechGasto = GetTechnicianFromGasto(idGasto);
                if (idTechGasto != Session.TechnicianId.Value)
                {
                    MessageBox.Show("No puede eliminar un gasto ingresado por otro técnico.", "Acceso denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Pedir motivo obligatorio
            string motivo = Interaction.InputBox(
                $"Ingrese el motivo de eliminación para el gasto:\n\"{descripcion}\"",
                "Motivo de eliminación",
                ""
            ).Trim();

            if (string.IsNullOrEmpty(motivo))
            {
                MessageBox.Show("Debe ingresar un motivo para eliminar el gasto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                "Esta acción eliminará el gasto de forma permanente.\n¿Desea continuar?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    EnsureLogTable(conn);

                    // Obtener datos requeridos antes de borrar (id_orden, id_technician)
                    int idOrden = _idOrden;
                    int? idTech = GetTechnicianFromGastoNullable(conn, idGasto);

                    using (var tx = conn.BeginTransaction())
                    {
                        // Registrar log
                        const string insLog = @"
                            INSERT INTO GastoEliminacionesLog (id_gasto, id_orden, id_technician, fecha, motivo)
                            VALUES (@g, @o, @t, @f, @m);";
                        using (var log = new SQLiteCommand(insLog, conn, tx))
                        {
                            log.Parameters.AddWithValue("@g", idGasto);
                            log.Parameters.AddWithValue("@o", idOrden);
                            if (idTech.HasValue) log.Parameters.AddWithValue("@t", idTech.Value);
                            else log.Parameters.AddWithValue("@t", DBNull.Value);
                            log.Parameters.AddWithValue("@f", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            log.Parameters.AddWithValue("@m", motivo);
                            log.ExecuteNonQuery();
                        }

                        // Eliminar gasto
                        const string del = "DELETE FROM Gastos WHERE id_gasto = @id;";
                        using (var cmd = new SQLiteCommand(del, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idGasto);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Gasto eliminado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadGastos();
                GastosChanged?.Invoke(); // refrescar totales/listas en el formulario que abrió
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar gasto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetTechnicianFromGasto(int idGasto)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                const string q = "SELECT id_technician FROM Gastos WHERE id_gasto = @id;";
                using (var cmd = new SQLiteCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idGasto);
                    var res = cmd.ExecuteScalar();
                    return (res == null || res == DBNull.Value) ? -1 : Convert.ToInt32(res);
                }
            }
        }

        private int? GetTechnicianFromGastoNullable(SQLiteConnection conn, int idGasto)
        {
            const string q = "SELECT id_technician FROM Gastos WHERE id_gasto = @id;";
            using (var cmd = new SQLiteCommand(q, conn))
            {
                cmd.Parameters.AddWithValue("@id", idGasto);
                var res = cmd.ExecuteScalar();
                if (res == null || res == DBNull.Value) return null;
                return Convert.ToInt32(res);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();

        // Event handlers en blanco si el designer los tiene enganchados
        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void VerGastosForm_Load(object sender, EventArgs e) { }
    }
}