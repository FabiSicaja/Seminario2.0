using MySql.Data.MySqlClient;
using Proyecto.Data;
using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class CrearOrdenForm : Form
    {
        public CrearOrdenForm()
        {
            InitializeComponent();
            LoadClientes();
            LoadTechnicians();
            this.AcceptButton = btnGuardar;
        }

        private void LoadClientes()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    const string query = "SELECT id_cliente, nombre FROM Clientes ORDER BY nombre;";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        cmbCliente.DataSource = dt;
                        cmbCliente.DisplayMember = "nombre";
                        cmbCliente.ValueMember = "id_cliente";
                        cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando clientes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTechnicians()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    const string query = "SELECT id_technician, nombre FROM Technicians ORDER BY nombre;";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        clbTechnicians.DataSource = dt;
                        clbTechnicians.DisplayMember = "nombre";
                        clbTechnicians.ValueMember = "id_technician";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando técnicos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarNumeroOrden()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string añoActual = DateTime.Now.ToString("yyyy");
                    string mesActual = DateTime.Now.ToString("MM");

                    string query = @"SELECT COUNT(*) FROM Ordenes 
                                   WHERE strftime('%Y', fecha_inicio) = @año 
                                   AND strftime('%m', fecha_inicio) = @mes";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@año", añoActual);
                        cmd.Parameters.AddWithValue("@mes", mesActual);

                        int numeroOrden = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                        return $"OT-{añoActual}-{mesActual}-{numeroOrden:D3}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando número de orden: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "OT-ERROR";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = (txtDescripcion.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese una descripción para la orden.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.DroppedDown = true;
                return;
            }

            if (clbTechnicians.CheckedItems.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un técnico.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clbTechnicians.Focus();
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        string numeroOrden = GenerarNumeroOrden();

                        const string insertOrden = @"
                            INSERT INTO Ordenes (descripcion, fecha_inicio, estado, id_cliente, numero_order)
                            VALUES (@descripcion, @fecha, 'Abierta', @id_cliente, @numero_order);";

                        long nuevoIdOrden;
                        using (var cmd = new MySqlCommand(insertOrden, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@id_cliente", Convert.ToInt32(cmbCliente.SelectedValue));
                            cmd.Parameters.AddWithValue("@numero_order", numeroOrden);

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("No se pudo crear la orden.");

                            cmd.CommandText = "SELECT last_insert_rowid();";
                            nuevoIdOrden = (long)(cmd.ExecuteScalar() ?? 0L);
                        }

                        const string insertPuente = @"
                            INSERT INTO OrdenTechnicians (id_orden, id_technician)
                            VALUES (@id_orden, @id_technician);";

                        using (var cmd = new MySqlCommand(insertPuente, conn, tx))
                        {
                            cmd.Parameters.Add("@id_orden", DbType.Int64).Value = nuevoIdOrden;
                            var pTec = cmd.Parameters.Add("@id_technician", DbType.Int32);

                            foreach (var item in clbTechnicians.CheckedItems)
                            {
                                var drv = item as DataRowView;
                                int idTec = Convert.ToInt32(drv["id_technician"]);
                                pTec.Value = idTec;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();

                        MessageBox.Show($"Orden creada exitosamente.\nNúmero de Orden: {numeroOrden}\nID: {nuevoIdOrden}",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardando la orden: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CrearOrdenForm_Load(object sender, EventArgs e) { }

        private void txtDescripcion_TextChanged(object sender, EventArgs e) { }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}