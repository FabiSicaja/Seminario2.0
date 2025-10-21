using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class CrearOrdenForm : Form
    {
        public CrearOrdenForm()
        {
            InitializeComponent();
            LoadTechnicians();
            
            this.AcceptButton = btnGuardar;
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
                MessageBox.Show("Error cargando técnicos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            string descripcion = (txtDescripcion.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese una descripción para la orden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            if (cmbTechnician.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un técnico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTechnician.DroppedDown = true;
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    
                    string insertSql = @"
                        INSERT INTO Ordenes (descripcion, fecha_inicio, id_technician, estado)
                        VALUES (@descripcion, @fecha, @idTechnician, 'Abierta');";

                    using (var cmd = new SQLiteCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@idTechnician", Convert.ToInt32(cmbTechnician.SelectedValue));

                        int affected = cmd.ExecuteNonQuery();

                        if (affected == 1)
                        {
                          
                            long nuevoId = 0;
                            using (var idCmd = new SQLiteCommand("SELECT last_insert_rowid();", conn))
                            {
                                object scalar = idCmd.ExecuteScalar();
                                if (scalar != null && long.TryParse(scalar.ToString(), out long id))
                                {
                                    nuevoId = id;
                                }
                            }

                           
                            string msg = nuevoId > 0
                                ? $"Orden creada exitosamente. ID: {nuevoId}"
                                : "Orden creada exitosamente.";

                            MessageBox.Show(msg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se insertó la orden. Intente nuevamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardando la orden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CrearOrdenForm_Load(object sender, EventArgs e)
        {
        }
    }
}
