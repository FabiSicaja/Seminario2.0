using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class ModificarOrdenForm : Form
    {
        private int idOrden;

        public ModificarOrdenForm(int idOrden)
        {
            InitializeComponent();
            this.idOrden = idOrden;
            LoadOrdenData();
            LoadTechnicians();
            LoadEstados();
        }

        private void LoadOrdenData()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT o.descripcion, o.fecha_inicio, o.fecha_fin, 
                               o.id_technician, o.estado, t.nombre as technician
                        FROM Ordenes o
                        LEFT JOIN Technicians t ON o.id_technician = t.id_technician
                        WHERE o.id_orden = @idOrden";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDescripcion.Text = reader["descripcion"].ToString();
                                dtpFechaInicio.Value = Convert.ToDateTime(reader["fecha_inicio"]);

                                if (reader["fecha_fin"] != DBNull.Value)
                                {
                                    dtpFechaFin.Value = Convert.ToDateTime(reader["fecha_fin"]);
                                    dtpFechaFin.Checked = true;
                                }
                                else
                                {
                                    dtpFechaFin.Checked = false;
                                }

                                if (cmbTechnician.Items.Count > 0)
                                    cmbTechnician.SelectedValue = reader["id_technician"];

                                cmbEstado.SelectedItem = reader["estado"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la orden: {ex.Message}", "Error",
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
                    string query = "SELECT id_technician, nombre FROM Technicians ORDER BY nombre";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cmbTechnician.DataSource = dt;
                        cmbTechnician.DisplayMember = "nombre";
                        cmbTechnician.ValueMember = "id_technician";
                        cmbTechnician.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar técnicos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEstados()
        {
            cmbEstado.Items.AddRange(new object[] { "Abierta", "En Proceso", "Cerrada", "Anulada" });
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción es requerida", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            if (cmbTechnician.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un técnico", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTechnician.Focus();
                return;
            }

            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estado", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        UPDATE Ordenes 
                        SET descripcion = @descripcion, 
                            fecha_inicio = @fecha_inicio, 
                            fecha_fin = @fecha_fin, 
                            id_technician = @id_technician,
                            estado = @estado
                        WHERE id_orden = @idOrden";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@fecha_inicio", dtpFechaInicio.Value.ToString("yyyy-MM-dd"));

                        if (dtpFechaFin.Checked)
                            cmd.Parameters.AddWithValue("@fecha_fin", dtpFechaFin.Value.ToString("yyyy-MM-dd"));
                        else
                            cmd.Parameters.AddWithValue("@fecha_fin", DBNull.Value);

                        cmd.Parameters.AddWithValue("@id_technician", cmbTechnician.SelectedValue);
                        cmd.Parameters.AddWithValue("@estado", cmbEstado.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@idOrden", idOrden);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Orden modificada exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar orden: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ModificarOrdenForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Modificar Orden #{idOrden}";
        }
    }
}