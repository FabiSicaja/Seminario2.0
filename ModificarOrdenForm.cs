using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class ModificarOrdenForm : Form
    {
        private readonly int idOrden;

        public ModificarOrdenForm(int idOrden)
        {
            InitializeComponent();
            this.idOrden = idOrden;

            LoadTechnicians();     // llena combo y checklist
            LoadEstados();
            LoadOrdenData();       // carga datos de la orden (después de combos)
            LoadTechniciansAsignados(); // marca técnicos ya asignados
            this.Text = $"Modificar Orden #{idOrden}";
        }

        private void LoadOrdenData()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT descripcion, fecha_inicio, fecha_fin, id_technician, estado
                        FROM Ordenes
                        WHERE id_orden = @idOrden";

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

                                if (reader["id_technician"] != DBNull.Value && cmbTechnician.DataSource != null)
                                    cmbTechnician.SelectedValue = Convert.ToInt32(reader["id_technician"]);

                                var estado = reader["estado"].ToString();
                                if (!string.IsNullOrWhiteSpace(estado))
                                    cmbEstado.SelectedItem = estado;
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
                    const string query = "SELECT id_technician, nombre FROM Technicians ORDER BY nombre";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var da = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Técnico principal
                        cmbTechnician.DataSource = dt.Copy();
                        cmbTechnician.DisplayMember = "nombre";
                        cmbTechnician.ValueMember = "id_technician";
                        cmbTechnician.DropDownStyle = ComboBoxStyle.DropDownList;

                        // Técnicos adicionales
                        clbTechnicians.DataSource = dt;
                        clbTechnicians.DisplayMember = "nombre";
                        clbTechnicians.ValueMember = "id_technician";
                        clbTechnicians.CheckOnClick = true;
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
            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new object[] { "Abierta", "En Proceso", "Cerrada", "Anulada" });
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadTechniciansAsignados()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT id_technician FROM OrdenTechnicians WHERE id_orden = @id;";
                    var asignados = new HashSet<int>();
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idOrden);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                                asignados.Add(Convert.ToInt32(rdr["id_technician"]));
                        }
                    }

                    for (int i = 0; i < clbTechnicians.Items.Count; i++)
                    {
                        var row = clbTechnicians.Items[i] as DataRowView;
                        if (row == null) continue;
                        int techId = Convert.ToInt32(row["id_technician"]);
                        if (asignados.Contains(techId))
                            clbTechnicians.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar técnicos asignados: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Seleccione el técnico principal", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estado", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        // Actualizar datos principales
                        string updateOrden = @"
                            UPDATE Ordenes 
                            SET descripcion = @descripcion, 
                                fecha_inicio = @fecha_inicio, 
                                fecha_fin = @fecha_fin, 
                                id_technician = @id_technician,
                                estado = @estado
                            WHERE id_orden = @idOrden";

                        using (var cmd = new SQLiteCommand(updateOrden, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                            cmd.Parameters.AddWithValue("@fecha_inicio", dtpFechaInicio.Value.ToString("yyyy-MM-dd"));
                            if (dtpFechaFin.Checked)
                                cmd.Parameters.AddWithValue("@fecha_fin", dtpFechaFin.Value.ToString("yyyy-MM-dd"));
                            else
                                cmd.Parameters.AddWithValue("@fecha_fin", DBNull.Value);

                            cmd.Parameters.AddWithValue("@id_technician", Convert.ToInt32(cmbTechnician.SelectedValue));
                            cmd.Parameters.AddWithValue("@estado", cmbEstado.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@idOrden", idOrden);
                            cmd.ExecuteNonQuery();
                        }

                        // Reemplazar técnicos adicionales en OrdenTechnicians
                        using (var del = new SQLiteCommand("DELETE FROM OrdenTechnicians WHERE id_orden = @id;", conn, tx))
                        {
                            del.Parameters.AddWithValue("@id", idOrden);
                            del.ExecuteNonQuery();
                        }

                        string insertSql = @"INSERT INTO OrdenTechnicians (id_orden, id_technician) VALUES (@id_orden, @id_technician);";
                        foreach (var item in clbTechnicians.CheckedItems)
                        {
                            var drv = item as DataRowView;
                            if (drv == null) continue;
                            int techId = Convert.ToInt32(drv["id_technician"]);

                            using (var ins = new SQLiteCommand(insertSql, conn, tx))
                            {
                                ins.Parameters.AddWithValue("@id_orden", idOrden);
                                ins.Parameters.AddWithValue("@id_technician", techId);
                                ins.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Orden modificada exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbTechnicians.Items.Count; i++)
                clbTechnicians.SetItemChecked(i, true);
        }

        private void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbTechnicians.Items.Count; i++)
                clbTechnicians.SetItemChecked(i, false);
        }

        private void ModificarOrdenForm_Load(object sender, EventArgs e) { }
    }
}