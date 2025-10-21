namespace Proyecto
{
    partial class IngresarGastoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // IngresarGastoForm
            // 
            this.ClientSize = new System.Drawing.Size(1117, 498);
            this.Name = "IngresarGastoForm";
            //this.Load += new System.EventHandler(this.IngresarGastoForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblOrdenTitulo;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label labelTipo;
        private System.Windows.Forms.ComboBox cmbTipoGasto;
        private System.Windows.Forms.Label labelFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label labelSerie;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label labelNoFactura;
        private System.Windows.Forms.TextBox txtNoFactura;
        private System.Windows.Forms.Label labelNit;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label labelProveedor;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Label labelConcepto;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.Label labelMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Panel pnlCombustible;
        private System.Windows.Forms.Label labelTipoComb;
        private System.Windows.Forms.ComboBox cmbTipoCombustible;
        private System.Windows.Forms.Label labelGalonaje;
        private System.Windows.Forms.TextBox txtGalonaje;
        private System.Windows.Forms.Button btnGuardar;
    }
}
