using ClosedXML.Excel;
using Proyecto.Data;
using Proyecto_de_Seminario;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            ApplyModernStyles();
            LoadUserWelcome();
            LoadOrdenes();
            UpdateStats();
        }

        private void ApplyModernStyles()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
            panelSidebar.BackColor = Color.FromArgb(41, 128, 185);
            panelHeader.BackColor = Color.White;

            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Button btn)
                {
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 255, 255, 255);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 255, 255, 255);
                    btn.Cursor = Cursors.Hand;
                }
            }
        }

        private void LoadUserWelcome()
        {
            labelWelcome.Text = $"Bienvenido: {Session.Username}";
            labelWelcome.ForeColor = Color.FromArgb(41, 128, 185);
        }

        private void LoadOrdenes()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            o.id_orden,
                            o.descripcion,
                            o.fecha_inicio,
                            o.fecha_fin,
                            c.nombre AS cliente,
                            COALESCE((
                                SELECT GROUP_CONCAT(t.nombre, ', ')
                                FROM OrdenTechnicians ot 
                                JOIN Technicians t ON t.id_technician = ot.id_technician
                                WHERE ot.id_orden = o.id_orden
                            ), '') AS technicians,
                            o.estado,
                            COALESCE(SUM(g.monto), 0) AS total_gastos
                        FROM Ordenes o
                        LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente
                        LEFT JOIN Gastos g   ON g.id_orden   = o.id_orden
                        GROUP BY o.id_orden
                        ORDER BY 
                            CASE o.estado
                                WHEN 'Abierta' THEN 1
                                WHEN 'En Proceso' THEN 2
                                WHEN 'Cerrada' THEN 3
                                WHEN 'Anulada' THEN 4
                                ELSE 5
                            END,
                            o.fecha_inicio DESC;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvOrdenes.AutoGenerateColumns = true;
                        dgvOrdenes.DataSource = null;
                        dgvOrdenes.DataSource = dt;

                        FormatDataGridView();
                        PaintOverdueRows();
                        // No llamo AlertOverdue aquí para no molestar cada carga; deja comentado si quieres
                        // AlertOverdue();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvOrdenes == null) return;
            if (dgvOrdenes.Columns.Count == 0)
            {
                CreateEmptyColumns();
                return;
            }

            TrySetCol("id_orden", "ID", 60);
            TrySetCol("descripcion", "Descripción", 220);
            TrySetCol("fecha_inicio", "Fecha Inicio", 100);
            TrySetCol("fecha_fin", "Fecha Fin", 100);
            TrySetCol("cliente", "Cliente", 160);
            TrySetCol("technicians", "Técnicos", 200);
            TrySetCol("estado", "Estado", 110);
            TrySetCol("total_gastos", "Total Gastos", 120);

            var colTotal = FindColumn("total_gastos");
            if (colTotal != null)
                colTotal.DefaultCellStyle.Format = "N2";

            dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenes.MultiSelect = false;
            dgvOrdenes.ReadOnly = true;
            dgvOrdenes.AllowUserToAddRows = false;
            dgvOrdenes.AllowUserToDeleteRows = false;
            dgvOrdenes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvOrdenes.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvOrdenes.EnableHeadersVisualStyles = false;
            dgvOrdenes.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvOrdenes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private DataGridViewColumn FindColumn(string key)
        {
            if (dgvOrdenes == null || dgvOrdenes.Columns == null || string.IsNullOrWhiteSpace(key))
                return null;

            foreach (DataGridViewColumn col in dgvOrdenes.Columns)
            {
                if (string.Equals(col.Name, key, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(col.DataPropertyName, key, StringComparison.OrdinalIgnoreCase))
                {
                    return col;
                }
            }
            return null;
        }

        private void TrySetCol(string name, string header, int width)
        {
            var col = FindColumn(name);
            if (col == null) return;
            col.HeaderText = header;
            col.Width = width;
        }

        private void CreateEmptyColumns()
        {
            dgvOrdenes.Columns.Clear();
            dgvOrdenes.Columns.Add("id_orden", "ID");
            dgvOrdenes.Columns.Add("descripcion", "Descripción");
            dgvOrdenes.Columns.Add("fecha_inicio", "Fecha Inicio");
            dgvOrdenes.Columns.Add("fecha_fin", "Fecha Fin");
            dgvOrdenes.Columns.Add("cliente", "Cliente");
            dgvOrdenes.Columns.Add("technicians", "Técnicos");
            dgvOrdenes.Columns.Add("estado", "Estado");
            dgvOrdenes.Columns.Add("total_gastos", "Total Gastos");

            MessageBox.Show("No hay órdenes registradas. Use el botón 'Crear Orden' para agregar una nueva orden.",
                "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PaintOverdueRows()
        {
            foreach (DataGridViewRow row in dgvOrdenes.Rows)
            {
                var estado = row.Cells["estado"]?.Value?.ToString();
                if (estado == "Cerrada" || estado == "Anulada") continue;

                if (DateTime.TryParse(row.Cells["fecha_inicio"]?.Value?.ToString(), out var fi))
                {
                    if (fi <= DateTime.Now.AddMonths(-2))
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        private void AlertOverdue()
        {
            try
            {
                using (var c = Database.GetConnection())
                {
                    c.Open();
                    var cmd = new SQLiteCommand(
                        "SELECT COUNT(*) FROM Ordenes WHERE estado NOT IN ('Cerrada','Anulada') AND fecha_inicio <= @lim;",
                        c);
                    cmd.Parameters.AddWithValue("@lim", DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd"));
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    if (n > 0)
                        MessageBox.Show($"Hay {n} orden(es) abiertas/en proceso con más de 2 meses.", "Alerta",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { /* no bloquear la carga por esto */ }
        }

        private void UpdateStats()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string queryTotal = "SELECT COUNT(*) FROM Ordenes";
                    using (var cmd = new SQLiteCommand(queryTotal, conn))
                        labelTotalOrdenes.Text = cmd.ExecuteScalar()?.ToString() ?? "0";

                    string queryAbiertas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Abierta'";
                    using (var cmd = new SQLiteCommand(queryAbiertas, conn))
                        labelTotalAbiertas.Text = cmd.ExecuteScalar()?.ToString() ?? "0";

                    string queryCerradas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Cerrada'";
                    using (var cmd = new SQLiteCommand(queryCerradas, conn))
                        labelTotalCerradas.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            var crearOrdenForm = new CrearOrdenForm();
            crearOrdenForm.FormClosed += (s, args) =>
            {
                LoadOrdenes();
                UpdateStats();
            };
            crearOrdenForm.ShowDialog();
        }

        private void btnVerGastos_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);

            // Abrir VerGastosForm (admin ve todos los gastos)
            var verGastosForm = new VerGastosForm(idOrden, soloMios: false);

            // 🔄 Refrescar listado/estadísticas si se agregan/eliminan gastos
            verGastosForm.GastosChanged += () =>
            {
                LoadOrdenes();
                UpdateStats();
            };

            verGastosForm.ShowDialog();
        }

        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);
            string estado = dgvOrdenes.CurrentRow.Cells["estado"].Value.ToString();

            if (estado == "Cerrada")
            {
                MessageBox.Show("Esta orden ya está cerrada", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (estado == "Anulada")
            {
                MessageBox.Show("No puede cerrar una orden anulada", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea cerrar esta orden?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Ordenes SET estado = 'Cerrada', fecha_fin = @fecha WHERE id_orden = @idOrden";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        int resultUpdate = cmd.ExecuteNonQuery();
                        if (resultUpdate > 0)
                        {
                            MessageBox.Show("Orden cerrada exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrdenes();
                            UpdateStats();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar orden: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarOrden_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);
            var modificarOrdenForm = new ModificarOrdenForm(idOrden);
            modificarOrdenForm.FormClosed += (s, args) =>
            {
                LoadOrdenes();
                UpdateStats();
            };
            modificarOrdenForm.ShowDialog();
        }

        private void btnAnularOrden_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);
            string estado = dgvOrdenes.CurrentRow.Cells["estado"].Value.ToString();

            if (estado == "Anulada")
            {
                MessageBox.Show("Esta orden ya está anulada", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (estado == "Cerrada")
            {
                MessageBox.Show("No puede anular una orden cerrada", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("¿Está seguro que desea anular esta orden?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Ordenes SET estado = 'Anulada' WHERE id_orden = @idOrden";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        int resultUpdate = cmd.ExecuteNonQuery();
                        if (resultUpdate > 0)
                        {
                            MessageBox.Show("Orden anulada exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrdenes();
                            UpdateStats();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al anular orden: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero.", "Reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);

            DataTable dtOrden = new DataTable();
            DataTable dtGastos = new DataTable();

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sqlOrden = @"
                        SELECT 
                            o.id_orden,
                            o.descripcion,
                            o.fecha_inicio,
                            o.fecha_fin,
                            o.estado,
                            c.nombre AS cliente,
                            COALESCE((
                                SELECT GROUP_CONCAT(t.nombre, ', ')
                                FROM OrdenTechnicians ot 
                                JOIN Technicians t ON t.id_technician = ot.id_technician
                                WHERE ot.id_orden = o.id_orden
                            ), '') AS technicians,
                            COALESCE((SELECT SUM(g2.monto) FROM Gastos g2 WHERE g2.id_orden = o.id_orden),0) AS total_gastos
                        FROM Ordenes o
                        LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente
                        WHERE o.id_orden = @id;";

                    using (var cmd = new SQLiteCommand(sqlOrden, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idOrden);
                        using (var ad = new SQLiteDataAdapter(cmd))
                            ad.Fill(dtOrden);
                    }

                    string sqlGastos = @"
                        SELECT 
                            g.id_gasto, g.fecha, g.tipo_gasto, g.serie, g.no_factura, g.nit, g.proveedor,
                            g.descripcion, g.monto, g.tipo_combustible, g.galonaje,
                            t.nombre AS tecnico
                        FROM Gastos g
                        LEFT JOIN Technicians t ON t.id_technician = g.id_technician
                        WHERE g.id_orden = @id
                        ORDER BY g.fecha, g.id_gasto;";
                    using (var cmd = new SQLiteCommand(sqlGastos, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idOrden);
                        using (var ad = new SQLiteDataAdapter(cmd))
                            ad.Fill(dtGastos);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error consultando datos: " + ex.Message, "Reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtOrden.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la orden seleccionada.", "Reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Guardar reporte de orden";
                sfd.Filter = "Excel Workbook (.xlsx)|.xlsx";
                sfd.FileName = $"Orden_{idOrden}_Reporte.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        var wsInfo = wb.AddWorksheet("Orden");
                        wsInfo.Cell(1, 1).SetValue("Reporte de Orden - INSELEC, S.A.");
                        wsInfo.Cell(1, 1).Style.Font.Bold = true;
                        wsInfo.Cell(1, 1).Style.Font.FontSize = 14;

                        var r = dtOrden.Rows[0];
                        int row = 3;

                        void PutKV(string k, object v)
                        {
                            wsInfo.Cell(row, 1).SetValue(k);
                            var c = wsInfo.Cell(row, 2);
                            if (v == null || v == DBNull.Value) c.SetValue(string.Empty);
                            else c.SetValue(v.ToString());
                            wsInfo.Cell(row, 1).Style.Font.Bold = true;
                            row++;
                        }

                        PutKV("ID Orden", r["id_orden"]);
                        PutKV("Descripción", r["descripcion"]);
                        PutKV("Cliente", r["cliente"]);
                        PutKV("Técnicos", r["technicians"]);
                        PutKV("Fecha Inicio", r["fecha_inicio"]);
                        PutKV("Fecha Fin", r["fecha_fin"]);
                        PutKV("Estado", r["estado"]);
                        PutKV("Total Gastos", r["total_gastos"]);

                        wsInfo.Columns().AdjustToContents();

                        var wsGastos = wb.AddWorksheet("Gastos");
                        if (dtGastos.Rows.Count > 0)
                        {
                            var tbl = wsGastos.Cell(1, 1).InsertTable(dtGastos, "TablaGastos", true);
                            tbl.Theme = XLTableTheme.TableStyleMedium2;
                            wsGastos.Columns().AdjustToContents();

                            var montoCol = dtGastos.Columns
                                .Cast<DataColumn>()
                                .FirstOrDefault(c => string.Equals(c.ColumnName, "monto", StringComparison.OrdinalIgnoreCase));

                            if (montoCol != null)
                            {
                                int colIndex = montoCol.Ordinal + 1;      // 1-based en Excel
                                int lastDataRow = dtGastos.Rows.Count + 1; // +1 por encabezado
                                int totalRow = lastDataRow + 1;

                                wsGastos.Cell(totalRow, colIndex - 1).SetValue("Total:");
                                wsGastos.Cell(totalRow, colIndex - 1).Style.Font.Bold = true;

                                wsGastos.Cell(totalRow, colIndex).FormulaA1 =
                                    $"SUM({wsGastos.Cell(2, colIndex).Address}:{wsGastos.Cell(lastDataRow, colIndex).Address})";
                                wsGastos.Cell(totalRow, colIndex).Style.Font.Bold = true;
                            }
                        }
                        else
                        {
                            wsGastos.Cell(1, 1).SetValue("No hay gastos registrados para esta orden.");
                        }

                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Reporte generado correctamente.", "Reporte",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error generando Excel: " + ex.Message, "Reporte",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.Clear();
                var loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["LoginForm"] == null)
            {
                var loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            dgvOrdenes.BringToFront();
        }

        private void dgvOrdenes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnVerGastos_Click(sender, EventArgs.Empty);
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawLine(pen, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LoadOrdenes();
            UpdateStats();
            MessageBox.Show("Datos actualizados correctamente", "Actualizar",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            var gestionarUsuariosForm = new GestionarUsuariosForm();
            gestionarUsuariosForm.ShowDialog();
        }

        private void btnGestionarClientes_Click(object sender, EventArgs e)
        {
            var gestionarClientesForm = new GestionarClientesForm();
            gestionarClientesForm.ShowDialog();
        }

        private void btnGestionarProveedores_Click(object sender, EventArgs e)
        {
            var gestionarProveedoresForm = new GestionarProveedoresForm();
            gestionarProveedoresForm.ShowDialog();
        }

        private void labelLogo_Click(object sender, EventArgs e) { }
    }
}