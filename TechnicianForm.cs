using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class TechnicianForm : Form
    {
        public TechnicianForm()
        {
            InitializeComponent();

            // Estilos ligeros (opcional)
            this.BackColor = Color.LightSteelBlue;
            btnIngresarGasto.BackColor = Color.SteelBlue; btnIngresarGasto.ForeColor = Color.White; btnIngresarGasto.FlatStyle = FlatStyle.Flat;
            btnLogout.BackColor = Color.SteelBlue; btnLogout.ForeColor = Color.White; btnLogout.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.SteelBlue; button1.ForeColor = Color.White; button1.FlatStyle = FlatStyle.Flat;
            button2.BackColor = Color.SteelBlue; button2.ForeColor = Color.White; button2.FlatStyle = FlatStyle.Flat;
            button3.BackColor = Color.SteelBlue; button3.ForeColor = Color.White; button3.FlatStyle = FlatStyle.Flat;

            dgvOrdenes.BackgroundColor = Color.White;
            dgvOrdenes.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            // Enlazar handler del botón “Seleccionar Orden” por código (no necesitas tocar el designer)
            button2.Click += button2_Click;

            LoadOrdenesAsignadas();
        }

        private void TechnicianForm_Load(object sender, EventArgs e)
        {
            if (Session.Username != null)
                labelWelcome.Text = $"Bienvenido: {Session.Username}";
            else if (Session.TechnicianId.HasValue)
                labelWelcome.Text = $"Bienvenido: Técnico {Session.TechnicianId.Value}";

            AjustarColumnasDataGridView();
        }

        private void LoadOrdenesAsignadas()
        {
            if (!Session.TechnicianId.HasValue)
            {
                MessageBox.Show("No hay técnico en sesión. Inicie sesión nuevamente.", "Sesión",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // Órdenes donde el técnico está asignado vía OrdenTechnicians
                    string query = @"
                        SELECT 
                            o.id_orden,
                            o.descripcion,
                            o.fecha_inicio,
                            o.fecha_fin,
                            c.nombre AS cliente,
                            COALESCE((
                                SELECT GROUP_CONCAT(t2.nombre, ', ')
                                FROM OrdenTechnicians ot2
                                JOIN Technicians t2 ON t2.id_technician = ot2.id_technician
                                WHERE ot2.id_orden = o.id_orden
                            ), '') AS technicians,
                            o.estado,
                            COALESCE(SUM(g.monto), 0) AS total_gastos
                        FROM Ordenes o
                        LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente
                        LEFT JOIN Gastos g   ON g.id_orden   = o.id_orden
                        WHERE EXISTS (
                            SELECT 1
                            FROM OrdenTechnicians ot
                            WHERE ot.id_orden = o.id_orden
                              AND ot.id_technician = @tid
                        )
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
                    {
                        cmd.Parameters.AddWithValue("@tid", Session.TechnicianId.Value);
                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            dgvOrdenes.DataSource = dt;
                        }
                    }
                }

                FormatearGrid();
                PintarAtrasadas();
                AjustarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar órdenes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearGrid()
        {
            if (dgvOrdenes.Columns.Count == 0) return;

            TrySetCol("id_orden", "ID", 60);
            TrySetCol("descripcion", "Descripción", 220);
            TrySetCol("fecha_inicio", "Fecha Inicio", 100);
            TrySetCol("fecha_fin", "Fecha Fin", 100);
            TrySetCol("cliente", "Cliente", 150);
            TrySetCol("technicians", "Técnicos", 180);
            TrySetCol("estado", "Estado", 100);
            TrySetCol("total_gastos", "Total Gastos", 110);

            if (dgvOrdenes.Columns.Contains("total_gastos"))
                dgvOrdenes.Columns["total_gastos"].DefaultCellStyle.Format = "N2";

            dgvOrdenes.EnableHeadersVisualStyles = false;
            dgvOrdenes.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvOrdenes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenes.MultiSelect = false;
            dgvOrdenes.ReadOnly = true;
        }

        private void TrySetCol(string name, string header, int width)
        {
            var col = dgvOrdenes.Columns.Cast<DataGridViewColumn>()
                .FirstOrDefault(c =>
                    string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.DataPropertyName, name, StringComparison.OrdinalIgnoreCase));
            if (col == null) return;
            col.HeaderText = header;
            col.Width = width;
        }

        private void PintarAtrasadas()
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

        private void AjustarColumnasDataGridView()
        {
            if (dgvOrdenes.Columns.Count > 0)
            {
                dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvOrdenes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                dgvOrdenes.Refresh();
            }
        }

        // Recargar órdenes
        private void button1_Click(object sender, EventArgs e) => LoadOrdenesAsignadas();

        // Ingresar gasto a la orden seleccionada
        private void btnIngresarGasto_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero");
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);
            string estado = dgvOrdenes.CurrentRow.Cells["estado"].Value.ToString();

            if (estado == "Cerrada")
            {
                MessageBox.Show("No puede agregar gastos a una orden cerrada");
                return;
            }

            var ingresarGastoForm = new IngresarGastoForm(idOrden);
            ingresarGastoForm.FormClosed += (s, args) => LoadOrdenesAsignadas();
            ingresarGastoForm.ShowDialog();
        }

        // Seleccionar Orden -> ver SOLO MIS gastos
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una orden primero", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idOrden = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["id_orden"].Value);
            var ver = new VerGastosForm(idOrden, soloMios: true);

            ver.GastosChanged += () =>
            {
                // Cuando se agregan/eliminan, refrescar la grilla y totales
                LoadOrdenesAsignadas();
            };

            ver.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de cambiar contraseña no implementada aún");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            var login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void TechnicianForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["LoginForm"] == null)
            {
                var login = new LoginForm();
                login.Show();
            }
        }

        private void TechnicianForm_Resize(object sender, EventArgs e) => AjustarColumnasDataGridView();

        private void labelWelcome_Click(object sender, EventArgs e) { }
    }
}