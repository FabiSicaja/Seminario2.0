using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Proyecto.Data;
using Proyecto;
using ClosedXML.Excel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Proyecto_de_Seminario;

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
                            t.nombre as technician, 
                            o.estado,
                            COALESCE(SUM(g.monto), 0) as total_gastos
                        FROM Ordenes o
                        LEFT JOIN Technicians t ON o.id_technician = t.id_technician
                        LEFT JOIN Gastos g ON o.id_orden = g.id_orden
                        GROUP BY o.id_orden
                        ORDER BY 
                            CASE o.estado
                                WHEN 'Abierta' THEN 1
                                WHEN 'En Proceso' THEN 2
                                WHEN 'Cerrada' THEN 3
                                WHEN 'Anulada' THEN 4
                                ELSE 5
                            END,
                            o.fecha_inicio DESC";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvOrdenes.DataSource = dt;

                        FormatDataGridView();
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
            // Verificar que el DataGridView esté inicializado y tenga columnas
            if (dgvOrdenes == null)
            {
                MessageBox.Show("DataGridView no está inicializado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si no hay columnas, crear columnas vacías para mostrar la estructura
            if (dgvOrdenes.Columns.Count == 0)
            {
                CreateEmptyColumns();
                return;
            }

            try
            {
                // Tu código actual de formateo aquí...
                if (dgvOrdenes.Columns.Contains("id_orden"))
                {
                    dgvOrdenes.Columns["id_orden"].HeaderText = "ID";
                    dgvOrdenes.Columns["id_orden"].Width = 60;
                }

                // ... resto del código de formateo
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al formatear DataGridView: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateEmptyColumns()
        {
            // Crear columnas manualmente cuando no hay datos
            dgvOrdenes.Columns.Clear();

            dgvOrdenes.Columns.Add("id_orden", "ID");
            dgvOrdenes.Columns.Add("descripcion", "Descripción");
            dgvOrdenes.Columns.Add("fecha_inicio", "Fecha Inicio");
            dgvOrdenes.Columns.Add("fecha_fin", "Fecha Fin");
            dgvOrdenes.Columns.Add("technician", "Técnico");
            dgvOrdenes.Columns.Add("estado", "Estado");
            dgvOrdenes.Columns.Add("total_gastos", "Total Gastos");

            // Aplicar formato básico
            dgvOrdenes.Columns["id_orden"].Width = 60;
            dgvOrdenes.Columns["descripcion"].Width = 200;
            dgvOrdenes.Columns["fecha_inicio"].Width = 100;
            dgvOrdenes.Columns["fecha_fin"].Width = 100;
            dgvOrdenes.Columns["technician"].Width = 120;
            dgvOrdenes.Columns["estado"].Width = 100;
            dgvOrdenes.Columns["total_gastos"].Width = 100;

            // Mostrar mensaje de que no hay datos
            MessageBox.Show("No hay órdenes registradas. Use el botón 'Crear Orden' para agregar una nueva orden.",
                "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStats()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Total órdenes
                    string queryTotal = "SELECT COUNT(*) FROM Ordenes";
                    using (var cmd = new SQLiteCommand(queryTotal, conn))
                    {
                        labelTotalOrdenes.Text = cmd.ExecuteScalar().ToString();
                    }

                    // Órdenes abiertas
                    string queryAbiertas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Abierta'";
                    using (var cmd = new SQLiteCommand(queryAbiertas, conn))
                    {
                        labelTotalAbiertas.Text = cmd.ExecuteScalar().ToString();
                    }

                    // Órdenes cerradas
                    string queryCerradas = "SELECT COUNT(*) FROM Ordenes WHERE estado = 'Cerrada'";
                    using (var cmd = new SQLiteCommand(queryCerradas, conn))
                    {
                        labelTotalCerradas.Text = cmd.ExecuteScalar().ToString();
                    }

                    // Órdenes en proceso
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
            var verGastosForm = new VerGastosForm(idOrden);
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
                            t.nombre AS technician,
                            COALESCE((SELECT SUM(g.monto) FROM Gastos g WHERE g.id_orden = o.id_orden),0) AS total_gastos
                        FROM Ordenes o
                        LEFT JOIN Technicians t ON o.id_technician = t.id_technician
                        WHERE o.id_orden = @id;";

                    using (var cmd = new SQLiteCommand(sqlOrden, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idOrden);
                        using (var ad = new SQLiteDataAdapter(cmd))
                            ad.Fill(dtOrden);
                    }

                    string sqlGastos = @"SELECT * FROM Gastos WHERE id_orden = @id ORDER BY rowid;";
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
                        wsInfo.Cell(1, 1).SetValue("Reporte de Orden - INSELECT, S.A.");
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
            // Asegurarse de que el DataGridView sea visible
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

        // Método para actualizar manualmente
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LoadOrdenes();
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
    }
}