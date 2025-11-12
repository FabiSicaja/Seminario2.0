using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Proyecto.Data;
using INSELEC;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto
    {
        public partial class AdminForm : Form
        {
            private bool _alertaMostrada = false;
            private string _filtroActual = null;
            private string _filtroEstado = null;

            public AdminForm()
            {
                InitializeComponent();

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

                if (btnHistorialEliminaciones != null)
                    btnHistorialEliminaciones.Click += btnHistorialEliminaciones_Click;

                // ELIMINAR ApplyModernStyles() y SetupResponsiveStatsPanel()
                // para mantener el diseño original del Designer

                LoadUserWelcome();
                LoadOrdenes();
                UpdateStats();

                // Agregar eventos de click a los paneles de estadísticas
                AddStatsClickEvents();
            }

            private void AddStatsClickEvents()
            {
                // Agregar funcionalidad de click a los paneles de estadísticas
                if (panelTotal != null)
                {
                    panelTotal.Click += (s, e) => FilterByStatus("TOTAL");
                    labelTotalOrdenes.Click += (s, e) => FilterByStatus("TOTAL");
                    labelOrdenesCount.Click += (s, e) => FilterByStatus("TOTAL");

                    // Agregar efecto hover
                    panelTotal.MouseEnter += (s, e) => panelTotal.BackColor = Color.FromArgb(245, 249, 252);
                    panelTotal.MouseLeave += (s, e) => panelTotal.BackColor = Color.White;
                }

                if (panelAbiertas != null)
                {
                    panelAbiertas.Click += (s, e) => FilterByStatus("Abierta");
                    labelTotalAbiertas.Click += (s, e) => FilterByStatus("Abierta");
                    labelAbiertas.Click += (s, e) => FilterByStatus("Abierta");

                    panelAbiertas.MouseEnter += (s, e) => panelAbiertas.BackColor = Color.FromArgb(252, 248, 242);
                    panelAbiertas.MouseLeave += (s, e) => panelAbiertas.BackColor = Color.White;
                }

                if (panelCerradas != null)
                {
                    panelCerradas.Click += (s, e) => FilterByStatus("Cerrada");
                    labelTotalCerradas.Click += (s, e) => FilterByStatus("Cerrada");
                    labelCerradas.Click += (s, e) => FilterByStatus("Cerrada");

                    panelCerradas.MouseEnter += (s, e) => panelCerradas.BackColor = Color.FromArgb(242, 252, 245);
                    panelCerradas.MouseLeave += (s, e) => panelCerradas.BackColor = Color.White;
                }
            }

            private void FilterByStatus(string estado)
            {
                if (estado == "TOTAL")
                {
                    // Mostrar todas las órdenes
                    _filtroEstado = null;
                    LoadOrdenes(_filtroActual);
                    UpdateStatsHighlight("TOTAL");
                }
                else
                {
                    // Filtrar por estado específico
                    _filtroEstado = estado;
                    LoadOrdenes(_filtroActual, estado);
                    UpdateStatsHighlight(estado);
                }
            }

            private void UpdateStatsHighlight(string selectedStat)
            {
                // Restablecer todos los paneles primero
                ResetStatsAppearance();

                // Resaltar el panel seleccionado
                switch (selectedStat)
                {
                    case "TOTAL":
                        if (panelTotal != null)
                        {
                            panelTotal.BackColor = Color.FromArgb(235, 245, 251);
                            panelTotal.BorderStyle = BorderStyle.FixedSingle;
                        }
                        break;
                    case "Abierta":
                        if (panelAbiertas != null)
                        {
                            panelAbiertas.BackColor = Color.FromArgb(254, 245, 231);
                            panelAbiertas.BorderStyle = BorderStyle.FixedSingle;
                        }
                        break;
                    case "Cerrada":
                        if (panelCerradas != null)
                        {
                            panelCerradas.BackColor = Color.FromArgb(235, 251, 238);
                            panelCerradas.BorderStyle = BorderStyle.FixedSingle;
                        }
                        break;
                }
            }

            private void ResetStatsAppearance()
            {
                if (panelTotal != null)
                {
                    panelTotal.BackColor = Color.White;
                    panelTotal.BorderStyle = BorderStyle.None;
                }
                if (panelAbiertas != null)
                {
                    panelAbiertas.BackColor = Color.White;
                    panelAbiertas.BorderStyle = BorderStyle.None;
                }
                if (panelCerradas != null)
                {
                    panelCerradas.BackColor = Color.White;
                    panelCerradas.BorderStyle = BorderStyle.None;
                }
            }

            private void LoadUserWelcome()
            {
                labelWelcome.Text = $"Bienvenido: {Session.Username}";
            }

            private void LoadOrdenes(string filtroBusqueda = null, string filtroEstado = null)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();

                        string query = @"
                    SELECT 
                        o.id_orden,
                        o.numero_order AS numero_orden,
                        o.descripcion,
                        o.fecha_inicio,
                        o.fecha_fin,
                        c.nombre AS cliente,
                        COALESCE((
                            SELECT GROUP_CONCAT(t.nombre, ', ')
                            FROM OrdenTechnicians ot 
                            JOIN Technicians t ON t.id_technician = ot.id_technician
                            WHERE ot.id_orden = o.id_orden
                        ), 'Sin técnicos') AS technicians,
                        o.estado,
                        COALESCE(SUM(g.monto), 0) AS total_gastos
                    FROM Ordenes o
                    LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente
                    LEFT JOIN Gastos g ON g.id_orden = o.id_orden";

                        string whereClause = "";

                        // Agregar filtro de búsqueda si se proporciona
                        if (!string.IsNullOrWhiteSpace(filtroBusqueda))
                        {
                            whereClause += @"
                        WHERE (o.numero_order LIKE @filtro 
                           OR o.descripcion LIKE @filtro 
                           OR o.estado LIKE @filtro
                           OR c.nombre LIKE @filtro
                           OR EXISTS (
                               SELECT 1 FROM OrdenTechnicians ot 
                               JOIN Technicians t ON t.id_technician = ot.id_technician 
                               WHERE ot.id_orden = o.id_orden AND t.nombre LIKE @filtro
                           ))";
                        }

                        // Agregar filtro de estado si se proporciona
                        if (!string.IsNullOrWhiteSpace(filtroEstado) && filtroEstado != "TOTAL")
                        {
                            if (!string.IsNullOrEmpty(whereClause))
                                whereClause += " AND o.estado = @estado";
                            else
                                whereClause += " WHERE o.estado = @estado";
                        }

                        query += whereClause;

                        query += @"
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

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            // Agregar parámetro de filtro si existe
                            if (!string.IsNullOrWhiteSpace(filtroBusqueda))
                            {
                                cmd.Parameters.AddWithValue("@filtro", $"%{filtroBusqueda}%");
                            }

                            // Agregar parámetro de estado si existe
                            if (!string.IsNullOrWhiteSpace(filtroEstado) && filtroEstado != "TOTAL")
                            {
                                cmd.Parameters.AddWithValue("@estado", filtroEstado);
                            }

                            using (var adapter = new MySqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);

                                dgvOrdenes.DataSource = null;
                                dgvOrdenes.DataSource = dt;

                                FormatDataGridView();
                                PaintOverdueRows();

                                if (!_alertaMostrada && string.IsNullOrWhiteSpace(filtroBusqueda) && string.IsNullOrWhiteSpace(filtroEstado))
                                {
                                    AlertOverdue();
                                    _alertaMostrada = true;
                                }

                                // Mostrar mensaje si no hay resultados con filtro
                                if ((!string.IsNullOrWhiteSpace(filtroBusqueda) || !string.IsNullOrWhiteSpace(filtroEstado)) && dt.Rows.Count == 0)
                                {
                                    string mensajeFiltro = "";
                                    if (!string.IsNullOrWhiteSpace(filtroBusqueda) && !string.IsNullOrWhiteSpace(filtroEstado))
                                        mensajeFiltro = $"búsqueda: \"{filtroBusqueda}\" y estado: \"{filtroEstado}\"";
                                    else if (!string.IsNullOrWhiteSpace(filtroBusqueda))
                                        mensajeFiltro = $"búsqueda: \"{filtroBusqueda}\"";
                                    else
                                        mensajeFiltro = $"estado: \"{filtroEstado}\"";

                                    MessageBox.Show($"No se encontraron órdenes con {mensajeFiltro}",
                                        "Búsqueda sin resultados",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            private void FormatDataGridView()
            {
                if (dgvOrdenes == null || dgvOrdenes.Columns.Count == 0)
                {
                    CreateEmptyColumns();
                    return;
                }

                // Configurar autoajuste para llenar el ancho disponible
                dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Configurar anchos preferentes para cada columna
                foreach (DataGridViewColumn column in dgvOrdenes.Columns)
                {
                    column.MinimumWidth = 80;

                    switch (column.Name.ToLower())
                    {
                        case "id_orden":
                            column.HeaderText = "ID";
                            column.FillWeight = 60;
                            break;
                        case "numero_orden":
                            column.HeaderText = "Número Orden";
                            column.FillWeight = 100;
                            break;
                        case "descripcion":
                            column.HeaderText = "Descripción";
                            column.FillWeight = 200;
                            break;
                        case "fecha_inicio":
                            column.HeaderText = "Fecha Inicio";
                            column.FillWeight = 80;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "fecha_fin":
                            column.HeaderText = "Fecha Fin";
                            column.FillWeight = 80;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "cliente":
                            column.HeaderText = "Cliente";
                            column.FillWeight = 120;
                            break;
                        case "technicians":
                            column.HeaderText = "Técnicos";
                            column.FillWeight = 150;
                            break;
                        case "estado":
                            column.HeaderText = "Estado";
                            column.FillWeight = 70;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "total_gastos":
                            column.HeaderText = "Total Gastos";
                            column.FillWeight = 90;
                            column.DefaultCellStyle.Format = "Q #,##0.00";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                    }
                }
            }

            private void CreateEmptyColumns()
            {
                dgvOrdenes.Columns.Clear();

                // Agregar todas las columnas
                dgvOrdenes.Columns.Add("id_orden", "ID");
                dgvOrdenes.Columns.Add("numero_orden", "Número Orden");
                dgvOrdenes.Columns.Add("descripcion", "Descripción");
                dgvOrdenes.Columns.Add("fecha_inicio", "Fecha Inicio");
                dgvOrdenes.Columns.Add("fecha_fin", "Fecha Fin");
                dgvOrdenes.Columns.Add("cliente", "Cliente");
                dgvOrdenes.Columns.Add("technicians", "Técnicos");
                dgvOrdenes.Columns.Add("estado", "Estado");
                dgvOrdenes.Columns.Add("total_gastos", "Total Gastos");

                // Configurar el autoajuste para llenar el ancho disponible
                dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Configurar anchos preferentes
                foreach (DataGridViewColumn column in dgvOrdenes.Columns)
                {
                    column.MinimumWidth = 80;

                    switch (column.Name.ToLower())
                    {
                        case "descripcion":
                            column.FillWeight = 200;
                            break;
                        case "technicians":
                            column.FillWeight = 150;
                            break;
                        case "cliente":
                            column.FillWeight = 120;
                            break;
                        case "numero_orden":
                            column.FillWeight = 100;
                            break;
                        case "total_gastos":
                            column.FillWeight = 90;
                            column.DefaultCellStyle.Format = "Q #,##0.00";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "fecha_inicio":
                        case "fecha_fin":
                            column.FillWeight = 80;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "estado":
                            column.FillWeight = 70;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "id_orden":
                            column.FillWeight = 60;
                            break;
                        default:
                            column.FillWeight = 100;
                            break;
                    }
                }

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
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(200, 0, 0);
                        }
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
                        var cmd = new MySqlCommand(
                            "SELECT COUNT(*) FROM Ordenes WHERE estado NOT IN ('Cerrada','Anulada') AND fecha_inicio <= @lim;",
                            c);
                        cmd.Parameters.AddWithValue("@lim", DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd"));
                        int n = Convert.ToInt32(cmd.ExecuteScalar());
                        if (n > 0)
                            MessageBox.Show($"Hay {n} orden(es) abiertas/en proceso con más de 2 meses.", "Alerta",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    // Ignorar errores en la alerta
                }
            }

            private void UpdateStats()
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();

                        string queryTotal = "SELECT COUNT(*) FROM Ordenes";
                        using (var cmd = new MySqlCommand(queryTotal, conn))
                        {
                            string total = cmd.ExecuteScalar()?.ToString() ?? "0";
                            labelTotalOrdenes.Text = total;
                        }

                        string queryAbiertas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Abierta'";
                        using (var cmd = new MySqlCommand(queryAbiertas, conn))
                        {
                            string abiertas = cmd.ExecuteScalar()?.ToString() ?? "0";
                            labelTotalAbiertas.Text = abiertas;
                        }

                        string queryCerradas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Cerrada'";
                        using (var cmd = new MySqlCommand(queryCerradas, conn))
                        {
                            string cerradas = cmd.ExecuteScalar()?.ToString() ?? "0";
                            labelTotalCerradas.Text = cerradas;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar estadísticas: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private int ObtenerIdOrdenDesdeNumero(string numeroOrden)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();

                        // Intenta primero con numero_order, luego con id_orden
                        string query = @"
                        SELECT id_orden 
                        FROM Ordenes 
                        WHERE numero_order = @numero_order 
                           OR id_orden = @numero_order";

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@numero_order", numeroOrden);
                            var result = cmd.ExecuteScalar();
                            return result != null ? Convert.ToInt32(result) : 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error obteniendo ID de orden: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }

            private string ObtenerNumeroOrdenSeleccionada()
            {
                if (dgvOrdenes.CurrentRow == null)
                    return null;

                // Primero intenta obtener el número de orden de la columna correcta
                var numeroOrden = dgvOrdenes.CurrentRow.Cells["numero_orden"]?.Value?.ToString();

                // Si no encuentra en numero_orden, intenta con id_orden
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    numeroOrden = dgvOrdenes.CurrentRow.Cells["id_orden"]?.Value?.ToString();
                }

                return numeroOrden;
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
                string numeroOrden = ObtenerNumeroOrdenSeleccionada();
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Seleccione una orden primero", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idOrden = ObtenerIdOrdenDesdeNumero(numeroOrden);
                if (idOrden == 0)
                {
                    MessageBox.Show("No se pudo encontrar la orden seleccionada", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                string numeroOrden = ObtenerNumeroOrdenSeleccionada();
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Seleccione una orden primero", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idOrden = ObtenerIdOrdenDesdeNumero(numeroOrden);
                if (idOrden == 0)
                {
                    MessageBox.Show("No se pudo encontrar la orden seleccionada", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);

                            int resultUpdate = cmd.ExecuteNonQuery();
                            if (resultUpdate > 0)
                            {
                                MessageBox.Show("Orden cerrada exitosamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadOrdenes(txtBuscarCliente.Text.Trim());
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
                string numeroOrden = ObtenerNumeroOrdenSeleccionada();
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Seleccione una orden primero", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idOrden = ObtenerIdOrdenDesdeNumero(numeroOrden);
                if (idOrden == 0)
                {
                    MessageBox.Show("No se pudo encontrar la orden seleccionada", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                string numeroOrden = ObtenerNumeroOrdenSeleccionada();
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Seleccione una orden primero", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idOrden = ObtenerIdOrdenDesdeNumero(numeroOrden);
                if (idOrden == 0)
                {
                    MessageBox.Show("No se pudo encontrar la orden seleccionada", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);

                            int resultUpdate = cmd.ExecuteNonQuery();
                            if (resultUpdate > 0)
                            {
                                MessageBox.Show("Orden anulada exitosamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadOrdenes(txtBuscarCliente.Text.Trim());
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
                string numeroOrden = ObtenerNumeroOrdenSeleccionada();
                if (string.IsNullOrEmpty(numeroOrden))
                {
                    MessageBox.Show("Seleccione una orden primero.", "Reporte",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int idOrden = ObtenerIdOrdenDesdeNumero(numeroOrden);
                if (idOrden == 0)
                {
                    MessageBox.Show("No se pudo encontrar la orden seleccionada", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                    o.numero_order AS numero_orden,
                    o.descripcion,
                    o.fecha_inicio,
                    o.fecha_fin,
                    o.estado,
                    'Cliente' AS cliente,
                    COALESCE((
                        SELECT GROUP_CONCAT(t.nombre, ', ')
                        FROM OrdenTechnicians ot 
                        JOIN Technicians t ON t.id_technician = ot.id_technician
                        WHERE ot.id_orden = o.id_orden
                    ), '') AS technicians,
                    COALESCE((SELECT SUM(g2.monto) FROM Gastos g2 WHERE g2.id_orden = o.id_orden),0) AS total_gastos
                FROM Ordenes o
                WHERE o.id_orden = @id;";

                        using (var cmd = new MySqlCommand(sqlOrden, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idOrden);
                            using (var ad = new MySqlDataAdapter(cmd))
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
                        using (var cmd = new MySqlCommand(sqlGastos, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idOrden);
                            using (var ad = new MySqlDataAdapter(cmd))
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
                    sfd.FileName = $"Orden_{numeroOrden}_Reporte.xlsx";

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

                            PutKV("Número de Orden", r["numero_orden"] ?? r["id_orden"]);
                            PutKV("ID Interno", r["id_orden"]);
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
                                    int colIndex = montoCol.Ordinal + 1;
                                    int lastDataRow = dtGastos.Rows.Count + 1;
                                    int totalRow = lastDataRow + 1;

                                    wsGastos.Cell(totalRow, colIndex - 1).SetValue("Total:");
                                    wsGastos.Cell(totalRow, colIndex - 1).Style.Font.Bold = true;

                                    // Calcular la suma directamente (solución más robusta)
                                    decimal total = 0;
                                    foreach (DataRow rowGasto in dtGastos.Rows)
                                    {
                                        if (rowGasto[montoCol] != DBNull.Value)
                                        {
                                            total += Convert.ToDecimal(rowGasto[montoCol]);
                                        }
                                    }

                                    wsGastos.Cell(totalRow, colIndex).SetValue(total);
                                    wsGastos.Cell(totalRow, colIndex).Style.Font.Bold = true;
                                    wsGastos.Cell(totalRow, colIndex).Style.NumberFormat.Format = "N2";
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
            this.Hide(); // Oculta temporalmente el AdminForm

            using (var historial = new HistorialEliminacionesForm())
            {
                historial.ShowDialog(); // Abre el historial como ventana modal
            }

            this.Show(); // Al cerrar el historial, vuelve al AdminForm
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
                LoadOrdenes(txtBuscarCliente.Text.Trim());
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

            private void panelStats_Paint(object sender, PaintEventArgs e)
            {

            }

            private void labelWelcome_Click(object sender, EventArgs e)
            {

            }

            private void btnBuscar_Click(object sender, EventArgs e)
            {
                string busqueda = txtBuscarCliente.Text.Trim();
                _filtroActual = busqueda;
                LoadOrdenes(busqueda, _filtroEstado);
            }

            private void labelCerradas_Click(object sender, EventArgs e)
            {

            }

            private void dgvOrdenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }
        }
    }