using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Proyecto.Data;
using Proyecto_de_Seminario;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Proyecto.Data;
using Proyecto;
using ClosedXML.Excel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Proyecto_de_Seminario;
using System.Collections.Generic;

namespace Proyecto
{
    public partial class AdminForm : Form
    {
        // >>> Para no repetir el MessageBox de alerta en la misma sesión del formulario
        private bool _alertaMostrada = false;

        public AdminForm()
        {
            InitializeComponent();

            // Eventos de búsqueda
            if (btnBuscar != null)
                btnBuscar.Click += (s, e) => LoadOrdenes(txtBuscarCliente.Text.Trim());

            if (txtBuscarCliente != null)
                txtBuscarCliente.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        LoadOrdenes(txtBuscarCliente.Text.Trim());
                    }
                };

            // Historial de eliminaciones
            if (btnHistorialEliminaciones != null)
                btnHistorialEliminaciones.Click += btnHistorialEliminaciones_Click;

            ApplyModernStyles();
            LoadUserWelcome();
            LoadOrdenes();  // carga inicial sin filtro (mostrará la alerta una vez)
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
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 255, 255, 255);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 255, 255, 255);
                    btn.Cursor = Cursors.Hand;
                    btn.ForeColor = Color.White;
                }
            }
        }

        private void LoadUserWelcome()
        {
            labelWelcome.Text = $"Bienvenido: {Session.Username}";
            labelWelcome.ForeColor = Color.FromArgb(41, 128, 185);
        }

        // =========================================================
        //  CARGA DE ÓRDENES (soporta distintos esquemas de cliente)
        // =========================================================
        private void LoadOrdenes(string filtroCliente = null)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Detectar de dónde sacar el "nombre del cliente"
                    bool joinClientes;
                    string clienteExpr = GetClienteNameExpression(conn, out joinClientes);

                    string query = $@"
                        SELECT 
                            o.id_orden,
                            o.descripcion,
                            o.fecha_inicio,
                            o.fecha_fin,
                            {clienteExpr} AS cliente,
                            COALESCE((
                                SELECT GROUP_CONCAT(t.nombre, ', ')
                                FROM OrdenTechnicians ot 
                                JOIN Technicians t ON t.id_technician = ot.id_technician
                                WHERE ot.id_orden = o.id_orden
                            ), '') AS technicians,
                            o.estado,
                            COALESCE(SUM(g.monto), 0) AS total_gastos
                        FROM Ordenes o
                        {(joinClientes ? "LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente" : "")}
                        LEFT JOIN Gastos g   ON g.id_orden   = o.id_orden
                        /FILTRO/
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

                    // Filtro por cliente (case-insensitive)
                    if (!string.IsNullOrWhiteSpace(filtroCliente))
                        query = query.Replace("/FILTRO/", $"WHERE LOWER({clienteExpr}) LIKE @filtro");
                    else
                        query = query.Replace("/FILTRO/", "");

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(filtroCliente))
                            cmd.Parameters.AddWithValue("@filtro", "%" + filtroCliente.ToLower() + "%");

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvOrdenes.AutoGenerateColumns = true;
                            dgvOrdenes.DataSource = null;
                            dgvOrdenes.DataSource = dt;

                            FormatDataGridView();
                            PaintOverdueRows();

                            // >>> Mostrar la alerta (solo una vez) y solo cuando no hay filtro
                            if (!_alertaMostrada && string.IsNullOrWhiteSpace(filtroCliente))
                            {
                                AlertOverdue();
                                _alertaMostrada = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Devuelve el SQL para el nombre del cliente y si se debe JOIN con Clientes.
        /// Prioridades:
        /// 1) Si Ordenes tiene 'cliente' => 'o.cliente' (sin join).
        /// 2) Si Clientes tiene 'nombre' => 'c.nombre' (join).
        /// 3) Si Clientes tiene 'nombre_cliente' => 'c.nombre_cliente' (join).
        /// Fallback: COALESCE de ambas (con join).
        /// </summary>
        private string GetClienteNameExpression(SQLiteConnection conn, out bool joinClientes)
        {
            joinClientes = false;

            if (TableHasColumn(conn, "Ordenes", "cliente"))
                return "o.cliente";

            if (TableHasColumn(conn, "Clientes", "nombre"))
            {
                joinClientes = true;
                return "c.nombre";
            }

            if (TableHasColumn(conn, "Clientes", "nombre_cliente"))
            {
                joinClientes = true;
                return "c.nombre_cliente";
            }

            // Fallback seguro
            joinClientes = true;
            return "COALESCE(c.nombre, c.nombre_cliente, '')";
        }

        private bool TableHasColumn(SQLiteConnection conn, string table, string col)
        {
            using (var cmd = new SQLiteCommand($"PRAGMA table_info({table});", conn))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    var name = rd["name"]?.ToString();
                    if (string.Equals(name, col, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        // =========================================================

        private void FormatDataGridView()
        {
            if (dgvOrdenes == null)
            {
                MessageBox.Show("DataGridView no está inicializado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si no hay datos, salir sin error
            if (dgvOrdenes.Columns.Count == 0 || dgvOrdenes.DataSource == null)
            {
                return;
            }

            try
            {
                // Aplicar estilos generales primero
                ApplyGeneralDataGridViewStyles();

                // Formatear columnas solo si existen
                FormatColumnIfExists("id_orden", "ID", 60);
                FormatColumnIfExists("descripcion", "Descripción", 200);
                FormatColumnIfExists("fecha_inicio", "Fecha Inicio", 100, "dd/MM/yyyy");
                FormatColumnIfExists("fecha_fin", "Fecha Fin", 100, "dd/MM/yyyy");
                FormatColumnIfExists("technician", "Técnico", 120);
                FormatColumnIfExists("estado", "Estado", 100);
                FormatColumnIfExists("total_gastos", "Total Gastos", 100, "C2", DataGridViewContentAlignment.MiddleRight);

                // Aplicar formato condicional a las filas
                ApplyConditionalFormatting();

                // Autoajustar columnas
                dgvOrdenes.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            // TrySetCol("id_orden", "ID", 60);
            // TrySetCol("descripcion", "Descripción", 220);
            // TrySetCol("fecha_inicio", "Fecha Inicio", 100);
            // TrySetCol("fecha_fin", "Fecha Fin", 100);
            // TrySetCol("cliente", "Cliente", 160);
            // TrySetCol("technicians", "Técnicos", 200);
            // TrySetCol("estado", "Estado", 110);
            // TrySetCol("total_gastos", "Total Gastos", 120);

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
            catch (Exception ex)
            {
                // Mostrar error más específico
                MessageBox.Show($"Error al formatear DataGridView: {ex.Message}\n\nDetalles: {ex.StackTrace}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ApplyGeneralDataGridViewStyles()
        {
            dgvOrdenes.EnableHeadersVisualStyles = false;

            // Estilo de encabezados
            dgvOrdenes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvOrdenes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrdenes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvOrdenes.ColumnHeadersHeight = 35;

            // Estilo de filas
            dgvOrdenes.RowsDefaultCellStyle.BackColor = Color.White;
            dgvOrdenes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Estilo de celdas
            dgvOrdenes.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvOrdenes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvOrdenes.DefaultCellStyle.SelectionForeColor = Color.White;

            // Configuración de selección
            dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenes.MultiSelect = false;
            dgvOrdenes.ReadOnly = true;
        }

        private void FormatColumnIfExists(string columnName, string headerText, int width,
            string format = null, DataGridViewContentAlignment? alignment = null)
        {
            if (dgvOrdenes.Columns.Contains(columnName))
            {
                var column = dgvOrdenes.Columns[columnName];
                column.HeaderText = headerText;
                column.Width = width;

                if (!string.IsNullOrEmpty(format))
                {
                    column.DefaultCellStyle.Format = format;
                }

                if (alignment.HasValue)
                {
                    column.DefaultCellStyle.Alignment = alignment.Value;
                }

                // Alineación especial para la columna de estado
                if (columnName == "estado")
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void ApplyConditionalFormatting()
        {
            foreach (DataGridViewRow row in dgvOrdenes.Rows)
            {
                if (row.Cells["estado"]?.Value != null)
                {
                    string estado = row.Cells["estado"].Value.ToString();
                    switch (estado)
                    {
                        case "Abierta":
                            row.Cells["estado"].Style.BackColor = Color.LightGreen;
                            row.Cells["estado"].Style.ForeColor = Color.DarkGreen;
                            break;
                        case "En Proceso":
                            row.Cells["estado"].Style.BackColor = Color.LightYellow;
                            row.Cells["estado"].Style.ForeColor = Color.Orange;
                            break;
                        case "Cerrada":
                            row.Cells["estado"].Style.BackColor = Color.LightBlue;
                            row.Cells["estado"].Style.ForeColor = Color.DarkBlue;
                            break;
                        case "Anulada":
                            row.Cells["estado"].Style.BackColor = Color.LightCoral;
                            row.Cells["estado"].Style.ForeColor = Color.DarkRed;
                            break;
                    }
                }
            }
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

            dgvOrdenes.Columns["id_orden"].Width = 60;
            dgvOrdenes.Columns["descripcion"].Width = 200;
            dgvOrdenes.Columns["fecha_inicio"].Width = 100;
            dgvOrdenes.Columns["fecha_fin"].Width = 100;
            dgvOrdenes.Columns["technician"].Width = 120;
            dgvOrdenes.Columns["estado"].Width = 100;
            dgvOrdenes.Columns["total_gastos"].Width = 100;

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
                    {
                        labelTotalOrdenes.Text = cmd.ExecuteScalar().ToString();
                    }

                    string queryAbiertas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Abierta'";
                    using (var cmd = new SQLiteCommand(queryAbiertas, conn))
                    {
                        labelTotalAbiertas.Text = cmd.ExecuteScalar().ToString();
                    }

                    string queryCerradas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Cerrada'";
                    using (var cmd = new SQLiteCommand(queryCerradas, conn))
                    {
                        labelTotalCerradas.Text = cmd.ExecuteScalar().ToString();
                    }

                    string queryProceso = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'En Proceso'";
                    using (var cmd = new SQLiteCommand(queryProceso, conn))
                    {

                    }
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
                LoadOrdenes(txtBuscarCliente.Text.Trim());
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

            var verGastosForm = new VerGastosForm(idOrden, soloMios: false);
            verGastosForm.GastosChanged += () =>
            {
                LoadOrdenes(txtBuscarCliente.Text.Trim());
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

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "UPDATE Ordenes SET estado = 'Cerrada', fecha_fin = @fecha WHERE id_orden = @idOrden";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            MessageBox.Show("Orden cerrada exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrdenes(txtBuscarCliente.Text.Trim());
                            UpdateStats();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cerrar orden: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                LoadOrdenes(txtBuscarCliente.Text.Trim());
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

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "UPDATE Ordenes SET estado = 'Anulada' WHERE id_orden = @idOrden";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            MessageBox.Show("Orden anulada exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrdenes(txtBuscarCliente.Text.Trim());
                            UpdateStats();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al anular orden: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                    // Detectar expresión de cliente para el reporte
                    bool joinClientes;
                    string clienteExpr = GetClienteNameExpression(conn, out joinClientes);

                    string sqlOrden = $@"
                        SELECT 
                            o.id_orden,
                            o.descripcion,
                            o.fecha_inicio,
                            o.fecha_fin,
                            o.estado,
                            {clienteExpr} AS cliente,
                            COALESCE((
                                SELECT GROUP_CONCAT(t.nombre, ', ')
                                FROM OrdenTechnicians ot 
                                JOIN Technicians t ON t.id_technician = ot.id_technician
                                WHERE ot.id_orden = o.id_orden
                            ), '') AS technicians,
                            COALESCE((SELECT SUM(g2.monto) FROM Gastos g2 WHERE g2.id_orden = o.id_orden),0) AS total_gastos
                        FROM Ordenes o
                        {(joinClientes ? "LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente" : "")}
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
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
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
                            if (v == null || v == DBNull.Value)
                                c.SetValue(string.Empty);
                            else if (v is DateTime dt)
                                c.SetValue(dt);
                            else if (v is int i)
                                c.SetValue(i);
                            else if (v is long l)
                                c.SetValue(l);
                            else if (v is float f)
                                c.SetValue((double)f);
                            else if (v is double d)
                                c.SetValue(d);
                            else if (v is decimal dec)
                                c.SetValue((double)dec);
                            else
                                c.SetValue(v.ToString());

                            wsInfo.Cell(row, 1).Style.Font.Bold = true;
                            row++;
                        }

                        PutKV("ID Orden", r["id_orden"]);
                        PutKV("Descripción", dtOrden.Columns.Contains("descripcion") ? r["descripcion"] : null);
                        PutKV("Técnico", dtOrden.Columns.Contains("technician") ? r["technician"] : null);
                        PutKV("Fecha Inicio", dtOrden.Columns.Contains("fecha_inicio") ? r["fecha_inicio"] : null);
                        PutKV("Fecha Fin", dtOrden.Columns.Contains("fecha_fin") ? r["fecha_fin"] : null);
                        PutKV("Estado", dtOrden.Columns.Contains("estado") ? r["estado"] : null);
                        PutKV("Total Gastos", dtOrden.Columns.Contains("total_gastos") ? r["total_gastos"] : null);

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
                                int colIndex = montoCol.Ordinal + 1;
                                int lastDataRow = dtGastos.Rows.Count + 1;
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

        private void btnHistorialEliminaciones_Click(object sender, EventArgs e)
        {
            using (var f = new HistorialEliminacionesForm())
            {
                f.ShowDialog();
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
                //var loginForm = new LoginForm();
                //loginForm.Show();
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
            LoadOrdenes(txtBuscarCliente.Text.Trim());
            UpdateStats();
            MessageBox.Show("Datos actualizados correctamente", "Actualizar",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            GestionarUsuariosForm gestionarUsuariosForm = new GestionarUsuariosForm();
            gestionarUsuariosForm.ShowDialog();
        }

        private void btnGestionarClientes_Click(object sender, EventArgs e)
        {
            GestionarClientesForm gestionarClientesForm = new GestionarClientesForm();
            gestionarClientesForm.ShowDialog();
        }

        private void btnGestionarProveedores_Click(object sender, EventArgs e)
        {
            GestionarProveedoresForm gestionarProveedoresForm = new GestionarProveedoresForm();
            gestionarProveedoresForm.ShowDialog();
        }

        private void labelLogo_Click(object sender, EventArgs e) { }
    }
}
