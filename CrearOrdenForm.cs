using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Proyecto.Data;

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

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
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

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        // CheckedListBox soporta DataSource/DisplayMember/ValueMember
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
                        // 1) Crear la orden con cliente
                        const string insertOrden = @"
                            INSERT INTO Ordenes (descripcion, fecha_inicio, estado, id_cliente)
                            VALUES (@descripcion, @fecha, 'Abierta', @id_cliente);";

                        long nuevoIdOrden;
                        using (var cmd = new SQLiteCommand(insertOrden, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@id_cliente", Convert.ToInt32(cmbCliente.SelectedValue));
                            if (cmd.ExecuteNonQuery() != 1)
                                throw new Exception("No se pudo crear la orden.");

                            cmd.CommandText = "SELECT last_insert_rowid();";
                            nuevoIdOrden = (long)(cmd.ExecuteScalar() ?? 0L);
                            if (nuevoIdOrden <= 0)
                                throw new Exception("No se obtuvo el ID de la nueva orden.");
                        }

                        // 2) Insertar múltiples técnicos en la tabla puente
                        const string insertPuente = @"
                            INSERT INTO OrdenTechnicians (id_orden, id_technician)
                            VALUES (@id_orden, @id_technician);";

                        using (var cmd = new SQLiteCommand(insertPuente, conn, tx))
                        {
                            cmd.Parameters.Add("@id_orden", DbType.Int64).Value = nuevoIdOrden;
                            var pTec = cmd.Parameters.Add("@id_technician", DbType.Int32);

                            foreach (var item in clbTechnicians.CheckedItems)
                            {
                                // Cada item es un DataRowView si usamos DataSource
                                var drv = item as DataRowView;
                                int idTec = Convert.ToInt32(drv["id_technician"]);
                                pTec.Value = idTec;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();

                        MessageBox.Show($"Orden creada exitosamente. ID: {nuevoIdOrden}",
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
    }
}