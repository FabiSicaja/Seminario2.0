using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class HistorialEliminacionesForm : Form
    {
        private readonly Form _parentForm;

        public HistorialEliminacionesForm(Form parent = null)
        {
            InitializeComponent();
            _parentForm = parent;
            CargarHistorial();
        }

        private void CargarHistorial(string filtroTexto = "")
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string where = string.IsNullOrWhiteSpace(filtroTexto)
                        ? ""
                        : " WHERE (ge.descripcion LIKE @f OR ge.comentario LIKE @f OR t.nombre LIKE @f OR c.nombre LIKE @f OR ge.proveedor LIKE @f) ";

                    string sql = @"
                        SELECT 
                            ge.fecha_eliminacion,
                            ge.id_gasto,
                            ge.id_orden,
                            c.nombre AS cliente,
                            t.nombre AS tecnico,
                            ge.descripcion,
                            ge.monto,
                            ge.tipo_gasto,
                            ge.tipo_combustible,
                            ge.galonaje,
                            ge.proveedor,
                            ge.no_factura,
                            ge.serie,
                            ge.nit,
                            ge.comentario,
                            ge.eliminado_por,
                            ge.eliminado_rol
                        FROM GastosEliminados ge
                        LEFT JOIN Ordenes o ON o.id_orden = ge.id_orden
                        LEFT JOIN Clientes c ON c.id_cliente = o.id_cliente
                        LEFT JOIN Technicians t ON t.id_technician = ge.id_technician"
                        + where +
                        " ORDER BY ge.fecha_eliminacion DESC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(filtroTexto))
                            cmd.Parameters.AddWithValue("@f", "%" + filtroTexto.Trim() + "%");

                        using (var ad = new MySqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            ad.Fill(dt);
                            dgvHistorial.DataSource = dt;

                            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            if (dgvHistorial.Columns.Contains("monto"))
                                dgvHistorial.Columns["monto"].DefaultCellStyle.Format = "N2";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando historial: " + ex.Message,
                    "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) => CargarHistorial(txtBuscar.Text);

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarHistorial(txtBuscar.Text);
                e.SuppressKeyPress = true;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // Si se abrió desde AdminForm, lo volvemos a mostrar al cerrar
            this.Close();

            if (_parentForm != null)
            {
                _parentForm.Show();
                _parentForm.Enabled = true;
                _parentForm.BringToFront();
            }
        }
        // Métodos vacíos para evitar errores del diseñador
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Este evento se genera automáticamente desde el diseñador.
            // Puedes dejarlo vacío o eliminar la asignación desde el diseñador.
        }

        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este evento se genera automáticamente desde el diseñador.
            // Puedes dejarlo vacío o eliminar la asignación desde el diseñador.
        }
    }

}
