using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class VerGastosForm : Form
    {
        private int _idOrden;

        public VerGastosForm(int idOrden)
        {
            InitializeComponent();
            _idOrden = idOrden;
            LoadGastos();
            this.Text = $"Gastos - Orden #{_idOrden}";
        }

        private void LoadGastos()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            id_gasto,
                            tipo_gasto,
                            fecha,
                            serie,
                            no_factura,
                            nit,
                            proveedor,
                            descripcion,
                            monto,
                            tipo_combustible,
                            galonaje,
                            iva,
                            subtotal
                        FROM Gastos 
                        WHERE id_orden = @idOrden 
                        ORDER BY fecha DESC";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", _idOrden);
                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvGastos.DataSource = dt;

                            // Formatear DataGridView
                            FormatDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar gastos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvGastos.Columns.Count > 0)
            {
                dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvGastos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

                // Formato de moneda para columnas numéricas
                if (dgvGastos.Columns["monto"] != null)
                    dgvGastos.Columns["monto"].DefaultCellStyle.Format = "C2";

                if (dgvGastos.Columns["iva"] != null)
                    dgvGastos.Columns["iva"].DefaultCellStyle.Format = "C2";

                if (dgvGastos.Columns["subtotal"] != null)
                    dgvGastos.Columns["subtotal"].DefaultCellStyle.Format = "C2";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGastos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un gasto para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idGasto = Convert.ToInt32(dgvGastos.CurrentRow.Cells["id_gasto"].Value);
            string descripcion = dgvGastos.CurrentRow.Cells["descripcion"].Value.ToString();

            var result = MessageBox.Show(
                $"¿Está seguro que desea eliminar el gasto:\n\"{descripcion}\"?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Gastos WHERE id_gasto = @idGasto";

                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idGasto", idGasto);
                            int resultDelete = cmd.ExecuteNonQuery();

                            if (resultDelete > 0)
                            {
                                MessageBox.Show("Gasto eliminado exitosamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadGastos();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar gasto: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {
            var ingresarGastoForm = new IngresarGastoForm(_idOrden);
            ingresarGastoForm.FormClosed += (s, args) => LoadGastos();
            ingresarGastoForm.ShowDialog();
        }
    }
}