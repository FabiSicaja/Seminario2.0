using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;
using Microsoft.VisualBasic; // Interaction.InputBox

namespace Proyecto
{
    public partial class VerGastosForm : Form
    {
        private readonly int _idOrden;
        private readonly bool _soloMios;

        // Para que el form que abrió éste refresque totales/listas
        public event Action GastosChanged;

        public VerGastosForm(int idOrden, bool soloMios = false)
        {
            InitializeComponent();
            _idOrden = idOrden;
            _soloMios = soloMios;
            Text = $"Gastos - Orden #{_idOrden}";

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

        private void LoadGastos()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT 
                            g.id_gasto,
                            g.id_orden,
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
                            COALESCE(t.nombre,'') AS tecnico,
                            ROUND(CASE WHEN LOWER(g.tipo_gasto) = 'combustible' THEN 0 ELSE g.monto * 0.12 END, 2) AS iva,
                            ROUND(g.monto - CASE WHEN LOWER(g.tipo_gasto) = 'combustible' THEN 0 ELSE g.monto * 0.12 END, 2) AS subtotal
                        FROM Gastos g
                        LEFT JOIN Technicians t ON t.id_technician = g.id_technician
                        WHERE g.id_orden = @o";

                    if (_soloMios && Session.TechnicianId.HasValue)
                        sql += " AND g.id_technician = @tid";

                    sql += " ORDER BY g.fecha DESC, g.id_gasto DESC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@o", _idOrden);
                        if (_soloMios && Session.TechnicianId.HasValue)
                            cmd.Parameters.AddWithValue("@tid", Session.TechnicianId.Value);

                        using (var ad = new MySqlDataAdapter(cmd))
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

            void Money(string n)
            {
                if (dgvGastos.Columns[n] != null)
                    dgvGastos.Columns[n].DefaultCellStyle.Format = "N2";
            }
            Money("monto");
            Money("iva");
            Money("subtotal");

            if (dgvGastos.Columns["tecnico"] != null)
            {
                dgvGastos.Columns["tecnico"].HeaderText = "Técnico";
            }
        }

        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {
            var f = new IngresarGastoForm(_idOrden);
            f.FormClosed += (s, args) =>
            {
                LoadGastos();
                GastosChanged?.Invoke();
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
            string desc = dgvGastos.CurrentRow.Cells["descripcion"].Value?.ToString() ?? "";

            string motivo = Interaction.InputBox(
                $"Ingrese el motivo de eliminación para el gasto:\n\"{desc}\"",
                "Motivo de eliminación", ""
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

                    var gastoRow = GetGastoRow(conn, idGasto);
                    if (gastoRow == null)
                    {
                        MessageBox.Show("No se encontró el gasto seleccionado.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string eliminadoPor = Session.Username ?? "desconocido";
                    string eliminadoRol = (Session.TechnicianId.HasValue ? "Técnico" : "Admin");

                    using (var tx = conn.BeginTransaction())
                    {
                        const string ins = @"INSERT INTO GastosEliminados 
                        (id_gasto, id_orden, id_technician, fecha, serie, no_factura, nit, proveedor, descripcion, monto, tipo_gasto, tipo_combustible, galonaje, comentario, eliminado_por, eliminado_rol, fecha_eliminacion)
                        VALUES (@id_gasto, @id_orden, @id_technician, @fecha, @serie, @no_factura, @nit, @proveedor, @descripcion, @monto, @tipo_gasto, @tipo_combustible, @galonaje, @comentario, @eliminado_por, @eliminado_rol, NOW());";

                        using (var log = new MySqlCommand(ins, conn, tx))
                        {
                            object V(object x) => x ?? DBNull.Value;
                            log.Parameters.AddWithValue("@id_gasto", gastoRow["id_gasto"]);
                            log.Parameters.AddWithValue("@id_orden", V(gastoRow["id_orden"]));
                            log.Parameters.AddWithValue("@id_technician", V(gastoRow["id_technician"]));
                            log.Parameters.AddWithValue("@fecha", V(gastoRow["fecha"]));
                            log.Parameters.AddWithValue("@serie", V(gastoRow["serie"]));
                            log.Parameters.AddWithValue("@no_factura", V(gastoRow["no_factura"]));
                            log.Parameters.AddWithValue("@nit", V(gastoRow["nit"]));
                            log.Parameters.AddWithValue("@proveedor", V(gastoRow["proveedor"]));
                            log.Parameters.AddWithValue("@descripcion", V(gastoRow["descripcion"]));
                            log.Parameters.AddWithValue("@monto", V(gastoRow["monto"]));
                            log.Parameters.AddWithValue("@tipo_gasto", V(gastoRow["tipo_gasto"]));
                            log.Parameters.AddWithValue("@tipo_combustible", V(gastoRow["tipo_combustible"]));
                            log.Parameters.AddWithValue("@galonaje", V(gastoRow["galonaje"]));
                            log.Parameters.AddWithValue("@comentario", motivo);
                            log.Parameters.AddWithValue("@eliminado_por", eliminadoPor);
                            log.Parameters.AddWithValue("@eliminado_rol", eliminadoRol);
                            log.ExecuteNonQuery();
                        }

                        // Eliminar gasto original
                        using (var del = new MySqlCommand("DELETE FROM Gastos WHERE id_gasto = @id", conn, tx))
                        {
                            del.Parameters.AddWithValue("@id", idGasto);
                            del.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }

                    MessageBox.Show("Gasto eliminado y registrado en historial.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadGastos();
                    GastosChanged?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar gasto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataRow GetGastoRow(MySqlConnection conn, int idGasto)
        {
            const string q = @"SELECT 
                id_gasto, id_orden, id_technician, fecha, serie, no_factura, nit, proveedor,
                descripcion, monto, tipo_gasto, tipo_combustible, galonaje
            FROM Gastos
            WHERE id_gasto = @id
            LIMIT 1;";
            using (var ad = new MySqlDataAdapter(q, conn))
            {
                ad.SelectCommand.Parameters.AddWithValue("@id", idGasto);
                var dt = new DataTable();
                ad.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void VerGastosForm_Load(object sender, EventArgs e) { }
    }
}