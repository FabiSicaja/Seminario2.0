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
            // Configurar valores por defecto
            //txtIVA.Text = "12"; // IVA por defecto del 12%
            CalcularSubtotal();
        }

        private void CalcularSubtotal()
        {
            try
            {
                if (decimal.TryParse(txtMonto.Text, out decimal monto) )
                    //&&
                    //decimal.TryParse(txtIVA.Text, out decimal iva))
                {
                    //decimal subtotal = monto * (1 + iva / 100);
                    //txtSubtotal.Text = subtotal.ToString("F2");
                }
            }
            catch (Exception)
            {
                //txtSubtotal.Text = "0.00";
            }
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            CalcularSubtotal();
        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            CalcularSubtotal();
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
                    string query = @"
                        INSERT INTO Gastos (
                            id_orden, tipo_gasto, fecha, serie, no_factura, 
                            nit, proveedor, descripcion, monto, tipo_combustible, 
                            galonaje, iva, subtotal
                        ) VALUES (
                            @idOrden, @tipoGasto, @fecha, @serie, @noFactura,
                            @nit, @proveedor, @descripcion, @monto, @tipoCombustible,
                            @galonaje, @iva, @subtotal
                        )";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idOrden", _idOrden);
                        cmd.Parameters.AddWithValue("@tipoGasto", cmbTipoGasto.Text);
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@serie", txtSerie.Text);
                        cmd.Parameters.AddWithValue("@noFactura", txtNoFactura.Text);
                        //cmd.Parameters.AddWithValue("@nit", txtNIT.Text);
                        cmd.Parameters.AddWithValue("@proveedor", txtProveedor.Text);
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        cmd.Parameters.AddWithValue("@monto", decimal.Parse(txtMonto.Text));
                        //cmd.Parameters.AddWithValue("@tipoCombustible", txtTipoCombustible.Text);
                        cmd.Parameters.AddWithValue("@galonaje", txtGalonaje.Text);
                        //cmd.Parameters.AddWithValue("@iva", decimal.Parse(txtIVA.Text));
                        //cmd.Parameters.AddWithValue("@subtotal", decimal.Parse(txtSubtotal.Text));

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Gasto ingresado exitosamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
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
            if (string.IsNullOrEmpty(cmbTipoGasto.Text))
            {
                MessageBox.Show("Seleccione el tipo de gasto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoGasto.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese una descripción", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}