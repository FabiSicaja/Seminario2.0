using System;
using System.Data.SQLite;
using System.Globalization;
using System.Windows.Forms;
using Proyecto.Data;

namespace Proyecto
{
    public partial class IngresarGastoForm : Form
    {
        private readonly int _idOrden;

        public IngresarGastoForm(int idOrden)
        {
            InitializeComponent();
            _idOrden = idOrden;
            lblOrden.Text = $"Orden: {_idOrden}";
        }

        private void IngresarGastoForm_Load(object sender, EventArgs e)
        {
            // Llenar combos
            cmbTipoGasto.Items.Clear();
            cmbTipoGasto.Items.AddRange(new object[] { "Bienes", "Servicios", "Combustible" });
            cmbTipoGasto.SelectedIndex = 0;

            cmbTipoCombustible.Items.Clear();
            cmbTipoCombustible.Items.AddRange(new object[] {
                "Gasolina Regular",
                "Gasolina Superior",
                "Diésel",
                "Gas LP"
            });

            // Valores por defecto UI
            dtpFecha.Value = DateTime.Today;
            pnlCombustible.Visible = false;

            // Placeholder simple
            txtMonto.Text = "";
            txtGalonaje.Text = "";
        }

        private void cmbTipoGasto_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esCombustible = string.Equals(cmbTipoGasto.Text, "Combustible", StringComparison.OrdinalIgnoreCase);
            pnlCombustible.Visible = esCombustible;

            if (!esCombustible)
            {
                cmbTipoCombustible.SelectedIndex = -1;
                txtGalonaje.Text = "";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Gastos (
                            id_orden, tipo_gasto, fecha, serie, no_factura, 
                            nit, proveedor, descripcion, monto, tipo_combustible, galonaje
                        ) VALUES (
                            @idOrden, @tipoGasto, @fecha, @serie, @noFactura,
                            @nit, @proveedor, @descripcion, @monto, @tipoCombustible, @galonaje
                        );";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        // Requeridos
                        cmd.Parameters.AddWithValue("@idOrden", _idOrden);
                        cmd.Parameters.AddWithValue("@tipoGasto", cmbTipoGasto.Text.Trim());
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());

                        // Opcionales (guardar NULL si están vacíos)
                        cmd.Parameters.AddWithValue("@serie", string.IsNullOrWhiteSpace(txtSerie.Text) ? (object)DBNull.Value : txtSerie.Text.Trim());
                        cmd.Parameters.AddWithValue("@noFactura", string.IsNullOrWhiteSpace(txtNoFactura.Text) ? (object)DBNull.Value : txtNoFactura.Text.Trim());
                        cmd.Parameters.AddWithValue("@nit", string.IsNullOrWhiteSpace(txtNit.Text) ? (object)DBNull.Value : txtNit.Text.Trim());
                        cmd.Parameters.AddWithValue("@proveedor", string.IsNullOrWhiteSpace(txtProveedor.Text) ? (object)DBNull.Value : txtProveedor.Text.Trim());

                        // Monto
                        if (!decimal.TryParse(txtMonto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var monto) || monto <= 0)
                        {
                            MessageBox.Show("Ingrese un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMonto.Focus();
                            return;
                        }
                        cmd.Parameters.AddWithValue("@monto", monto);

                        // Campos de combustible (solo si aplica)
                        if (pnlCombustible.Visible)
                        {
                            cmd.Parameters.AddWithValue("@tipoCombustible",
                                string.IsNullOrWhiteSpace(cmbTipoCombustible.Text) ? (object)DBNull.Value : cmbTipoCombustible.Text.Trim());

                            if (!decimal.TryParse(txtGalonaje.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var galonaje) || galonaje <= 0)
                            {
                                MessageBox.Show("Ingrese un galonaje válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtGalonaje.Focus();
                                return;
                            }
                            cmd.Parameters.AddWithValue("@galonaje", galonaje);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@tipoCombustible", DBNull.Value);
                            cmd.Parameters.AddWithValue("@galonaje", DBNull.Value);
                        }

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Gasto ingresado exitosamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar el gasto. Intente nuevamente.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar gasto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(cmbTipoGasto.Text))
            {
                MessageBox.Show("Seleccione el tipo de gasto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoGasto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese una descripción/concepto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            if (!decimal.TryParse(txtMonto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            if (pnlCombustible.Visible)
            {
                if (string.IsNullOrWhiteSpace(cmbTipoCombustible.Text))
                {
                    MessageBox.Show("Seleccione el tipo de combustible.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTipoCombustible.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtGalonaje.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var galonaje) || galonaje <= 0)
                {
                    MessageBox.Show("Ingrese un galonaje válido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGalonaje.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}