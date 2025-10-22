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
            txtNit.Leave += txtNit_Leave;   // autocompletar proveedor
        }

        private void IngresarGastoForm_Load(object sender, EventArgs e)
        {
            cmbTipoGasto.Items.Clear();
            cmbTipoGasto.Items.AddRange(new object[] { "Bienes", "Servicios", "Combustible" });
            cmbTipoGasto.SelectedIndex = 0;

            cmbTipoCombustible.Items.Clear();
            cmbTipoCombustible.Items.AddRange(new object[] {
                "Gasolina Regular","Gasolina Super","Diésel","Gasolina V-Power"
            });

            dtpFecha.Value = DateTime.Today;
            pnlCombustible.Visible = false;
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

        // Autocompletar proveedor al salir del NIT
        private void txtNit_Leave(object sender, EventArgs e)
        {
            string nit = (txtNit.Text ?? "").Trim();
            if (nit == "") return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    const string sql = "SELECT nombre FROM Proveedores WHERE nit = @nit LIMIT 1;";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nit", nit);
                        var r = cmd.ExecuteScalar();
                        if (r != null && r != DBNull.Value)
                            txtProveedor.Text = r.ToString();  // encontrado
                        // si no existe, se deja escribir normal
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo consultar el proveedor por NIT. " + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    const string sql = @"
                        INSERT INTO Gastos (
                            id_orden, id_technician, tipo_gasto, fecha, serie, no_factura, 
                            nit, proveedor, descripcion, monto, tipo_combustible, galonaje
                        ) VALUES (
                            @idOrden, @idTechnician, @tipoGasto, @fecha, @serie, @noFactura,
                            @nit, @proveedor, @descripcion, @monto, @tipoCombustible, @galonaje
                        );";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", _idOrden);
                        cmd.Parameters.AddWithValue("@tipoGasto", cmbTipoGasto.Text.Trim());
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());

                        // id del técnico (si hay)
                        if (Session.TechnicianId.HasValue)
                            cmd.Parameters.AddWithValue("@idTechnician", Session.TechnicianId.Value);
                        else
                            cmd.Parameters.AddWithValue("@idTechnician", DBNull.Value);

                        // opcionales
                        cmd.Parameters.AddWithValue("@serie", string.IsNullOrWhiteSpace(txtSerie.Text) ? (object)DBNull.Value : txtSerie.Text.Trim());
                        cmd.Parameters.AddWithValue("@noFactura", string.IsNullOrWhiteSpace(txtNoFactura.Text) ? (object)DBNull.Value : txtNoFactura.Text.Trim());
                        cmd.Parameters.AddWithValue("@nit", string.IsNullOrWhiteSpace(txtNit.Text) ? (object)DBNull.Value : txtNit.Text.Trim());
                        cmd.Parameters.AddWithValue("@proveedor", string.IsNullOrWhiteSpace(txtProveedor.Text) ? (object)DBNull.Value : txtProveedor.Text.Trim());

                        if (!decimal.TryParse(txtMonto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var monto) || monto <= 0)
                        {
                            MessageBox.Show("Ingrese un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMonto.Focus(); return;
                        }
                        cmd.Parameters.AddWithValue("@monto", monto);

                        if (pnlCombustible.Visible)
                        {
                            cmd.Parameters.AddWithValue("@tipoCombustible",
                                string.IsNullOrWhiteSpace(cmbTipoCombustible.Text) ? (object)DBNull.Value : cmbTipoCombustible.Text.Trim());

                            if (!decimal.TryParse(txtGalonaje.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var gal) || gal <= 0)
                            {
                                MessageBox.Show("Ingrese un galonaje válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtGalonaje.Focus(); return;
                            }
                            cmd.Parameters.AddWithValue("@galonaje", gal);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@tipoCombustible", DBNull.Value);
                            cmd.Parameters.AddWithValue("@galonaje", DBNull.Value);
                        }

                        int n = cmd.ExecuteNonQuery();
                        if (n > 0)
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
                cmbTipoGasto.Focus(); return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese una descripción/concepto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus(); return false;
            }
            if (!decimal.TryParse(txtMonto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus(); return false;
            }
            if (pnlCombustible.Visible)
            {
                if (string.IsNullOrWhiteSpace(cmbTipoCombustible.Text))
                {
                    MessageBox.Show("Seleccione el tipo de combustible.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTipoCombustible.Focus(); return false;
                }
                if (!decimal.TryParse(txtGalonaje.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var gal) || gal <= 0)
                {
                    MessageBox.Show("Ingrese un galonaje válido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGalonaje.Focus(); return false;
                }
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e) => Close();
    }
}