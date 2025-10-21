using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;
using Proyecto;

namespace Proyecto
{
    public partial class TechnicianForm : Form
    {
        public TechnicianForm()
        {
            InitializeComponent();

            // Configuración de colores
            this.BackColor = System.Drawing.Color.LightSteelBlue;

            // Configuración de botones
            btnIngresarGasto.BackColor = System.Drawing.Color.SteelBlue;
            btnIngresarGasto.ForeColor = System.Drawing.Color.White;
            btnIngresarGasto.FlatStyle = FlatStyle.Flat;

            btnLogout.BackColor = System.Drawing.Color.SteelBlue;
            btnLogout.ForeColor = System.Drawing.Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;

            button1.BackColor = System.Drawing.Color.SteelBlue;
            button1.ForeColor = System.Drawing.Color.White;
            button1.FlatStyle = FlatStyle.Flat;

            button2.BackColor = System.Drawing.Color.SteelBlue;
            button2.ForeColor = System.Drawing.Color.White;
            button2.FlatStyle = FlatStyle.Flat;

            button3.BackColor = System.Drawing.Color.SteelBlue;
            button3.ForeColor = System.Drawing.Color.White;
            button3.FlatStyle = FlatStyle.Flat;

            // Configuración del DataGridView
            dgvOrdenes.BackgroundColor = System.Drawing.Color.White;
            dgvOrdenes.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;

            // Cargar datos
            LoadOrdenesAsignadas();
        }

        private void LoadOrdenesAsignadas()
        {
            if (!Session.TechnicianId.HasValue) return;

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT o.id_orden, o.descripcion, o.fecha_inicio, o.fecha_fin, o.estado,
                           COALESCE(SUM(g.monto), 0) AS total_gastos
                    FROM Ordenes o
                    LEFT JOIN Gastos g ON o.id_orden = g.id_orden
                    WHERE o.id_technician = @idTechnician
                    GROUP BY o.id_orden
                    ORDER BY o.fecha_inicio DESC;";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTechnician", Session.TechnicianId.Value);
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dgvOrdenes.DataSource = dt;

                        // Ajustar automáticamente el ancho de las columnas después de cargar los datos
                        AjustarColumnasDataGridView();
                    }
                }
            }
        }

        private void AjustarColumnasDataGridView()
        {
            // Asegurarse de que las columnas se ajusten al contenido y al espacio disponible
            if (dgvOrdenes.Columns.Count > 0)
            {
                // Configurar el modo de autoajuste para llenar el espacio disponible
                dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Ajustar el alto de las filas automáticamente
                dgvOrdenes.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                // Refrescar el control
                dgvOrdenes.Refresh();
            }
        }

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

        private void TechnicianForm_Load(object sender, EventArgs e)
        {
            // Actualizar el mensaje de bienvenida con información básica
            if (Session.TechnicianId.HasValue)
            {
                labelWelcome.Text = $"Bienvenido: Técnico {Session.TechnicianId.Value}";
            }

            // Ajustar las columnas al cargar el formulario
            AjustarColumnasDataGridView();
        }

        private void TechnicianForm_Resize(object sender, EventArgs e)
        {
            // Ajustar las columnas cuando se redimensiona el formulario
            AjustarColumnasDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Recargar las órdenes asignadas
            LoadOrdenesAsignadas();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de cambiar contraseña no implementada aún");
        }

        // Método para manejar el evento Click del labelWelcome
        private void labelWelcome_Click(object sender, EventArgs e)
        {
            
        }
    }
}