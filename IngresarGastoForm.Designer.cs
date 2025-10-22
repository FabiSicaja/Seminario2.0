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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblOrdenTitulo = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.labelProveedor = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.labelNit = new System.Windows.Forms.Label();
            this.txtNoFactura = new System.Windows.Forms.TextBox();
            this.labelNoFactura = new System.Windows.Forms.Label();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.labelSerie = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.labelFecha = new System.Windows.Forms.Label();
            this.cmbTipoGasto = new System.Windows.Forms.ComboBox();
            this.labelTipo = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.labelConcepto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.labelMonto = new System.Windows.Forms.Label();
            this.pnlCombustible = new System.Windows.Forms.Panel();
            this.txtGalonaje = new System.Windows.Forms.TextBox();
            this.labelGalonaje = new System.Windows.Forms.Label();
            this.cmbTipoCombustible = new System.Windows.Forms.ComboBox();
            this.labelTipoComb = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.pnlCombustible.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblOrden);
            this.panelHeader.Controls.Add(this.lblOrdenTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblOrdenTitulo
            // 
            this.lblOrdenTitulo.AutoSize = true;
            this.lblOrdenTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOrdenTitulo.ForeColor = System.Drawing.Color.White;
            this.lblOrdenTitulo.Location = new System.Drawing.Point(20, 18);
            this.lblOrdenTitulo.Name = "lblOrdenTitulo";
            this.lblOrdenTitulo.Size = new System.Drawing.Size(178, 28);
            this.lblOrdenTitulo.TabIndex = 0;
            this.lblOrdenTitulo.Text = "Ingreso de Gasto";
            // 
            // lblOrden
            // 
            this.lblOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrden.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrden.ForeColor = System.Drawing.Color.White;
            this.lblOrden.Location = new System.Drawing.Point(610, 18);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(270, 24);
            this.lblOrden.TabIndex = 1;
            this.lblOrden.Text = "Orden: -";
            this.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Controls.Add(this.txtProveedor);
            this.panelContent.Controls.Add(this.labelProveedor);
            this.panelContent.Controls.Add(this.txtNit);
            this.panelContent.Controls.Add(this.labelNit);
            this.panelContent.Controls.Add(this.txtNoFactura);
            this.panelContent.Controls.Add(this.labelNoFactura);
            this.panelContent.Controls.Add(this.txtSerie);
            this.panelContent.Controls.Add(this.labelSerie);
            this.panelContent.Controls.Add(this.dtpFecha);
            this.panelContent.Controls.Add(this.labelFecha);
            this.panelContent.Controls.Add(this.cmbTipoGasto);
            this.panelContent.Controls.Add(this.labelTipo);
            this.panelContent.Controls.Add(this.txtDescripcion);
            this.panelContent.Controls.Add(this.labelConcepto);
            this.panelContent.Controls.Add(this.txtMonto);
            this.panelContent.Controls.Add(this.labelMonto);
            this.panelContent.Controls.Add(this.pnlCombustible);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 60);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(900, 380);
            this.panelContent.TabIndex = 1;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(560, 114);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(300, 22);
            this.txtProveedor.TabIndex = 7;
            // 
            // labelProveedor
            // 
            this.labelProveedor.AutoSize = true;
            this.labelProveedor.Location = new System.Drawing.Point(470, 117);
            this.labelProveedor.Name = "labelProveedor";
            this.labelProveedor.Size = new System.Drawing.Size(76, 16);
            this.labelProveedor.TabIndex = 50;
            this.labelProveedor.Text = "Proveedor:";
            // 
            // txtNit
            // 
            this.txtNit.Location = new System.Drawing.Point(560, 74);
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(300, 22);
            this.txtNit.TabIndex = 6;
            // 
            // labelNit
            // 
            this.labelNit.AutoSize = true;
            this.labelNit.Location = new System.Drawing.Point(470, 77);
            this.labelNit.Name = "labelNit";
            this.labelNit.Size = new System.Drawing.Size(29, 16);
            this.labelNit.TabIndex = 48;
            this.labelNit.Text = "NIT:";
            // 
            // txtNoFactura
            // 
            this.txtNoFactura.Location = new System.Drawing.Point(560, 34);
            this.txtNoFactura.Name = "txtNoFactura";
            this.txtNoFactura.Size = new System.Drawing.Size(300, 22);
            this.txtNoFactura.TabIndex = 5;
            // 
            // labelNoFactura
            // 
            this.labelNoFactura.AutoSize = true;
            this.labelNoFactura.Location = new System.Drawing.Point(470, 37);
            this.labelNoFactura.Name = "labelNoFactura";
            this.labelNoFactura.Size = new System.Drawing.Size(74, 16);
            this.labelNoFactura.TabIndex = 46;
            this.labelNoFactura.Text = "No. factura:";
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(120, 114);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(300, 22);
            this.txtSerie.TabIndex = 3;
            // 
            // labelSerie
            // 
            this.labelSerie.AutoSize = true;
            this.labelSerie.Location = new System.Drawing.Point(30, 117);
            this.labelSerie.Name = "labelSerie";
            this.labelSerie.Size = new System.Drawing.Size(41, 16);
            this.labelSerie.TabIndex = 44;
            this.labelSerie.Text = "Serie:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpFecha.Location = new System.Drawing.Point(120, 74);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(300, 22);
            this.dtpFecha.TabIndex = 2;
            // 
            // labelFecha
            // 
            this.labelFecha.AutoSize = true;
            this.labelFecha.Location = new System.Drawing.Point(30, 77);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(47, 16);
            this.labelFecha.TabIndex = 42;
            this.labelFecha.Text = "Fecha:";
            // 
            // cmbTipoGasto
            // 
            this.cmbTipoGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoGasto.FormattingEnabled = true;
            this.cmbTipoGasto.Location = new System.Drawing.Point(120, 34);
            this.cmbTipoGasto.Name = "cmbTipoGasto";
            this.cmbTipoGasto.Size = new System.Drawing.Size(300, 24);
            this.cmbTipoGasto.TabIndex = 1;
            this.cmbTipoGasto.SelectedIndexChanged += new System.EventHandler(this.cmbTipoGasto_SelectedIndexChanged);
            // 
            // labelTipo
            // 
            this.labelTipo.AutoSize = true;
            this.labelTipo.Location = new System.Drawing.Point(30, 37);
            this.labelTipo.Name = "labelTipo";
            this.labelTipo.Size = new System.Drawing.Size(87, 16);
            this.labelTipo.TabIndex = 40;
            this.labelTipo.Text = "Tipo de gasto:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(120, 154);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(740, 80);
            this.txtDescripcion.TabIndex = 8;
            this.txtDescripcion.Text = "";
            // 
            // labelConcepto
            // 
            this.labelConcepto.AutoSize = true;
            this.labelConcepto.Location = new System.Drawing.Point(30, 157);
            this.labelConcepto.Name = "labelConcepto";
            this.labelConcepto.Size = new System.Drawing.Size(70, 16);
            this.labelConcepto.TabIndex = 38;
            this.labelConcepto.Text = "Concepto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(120, 250);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(300, 22);
            this.txtMonto.TabIndex = 9;
            // 
            // labelMonto
            // 
            this.labelMonto.AutoSize = true;
            this.labelMonto.Location = new System.Drawing.Point(30, 253);
            this.labelMonto.Name = "labelMonto";
            this.labelMonto.Size = new System.Drawing.Size(50, 16);
            this.labelMonto.TabIndex = 36;
            this.labelMonto.Text = "Monto:";
            // 
            // pnlCombustible
            // 
            this.pnlCombustible.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCombustible.Controls.Add(this.txtGalonaje);
            this.pnlCombustible.Controls.Add(this.labelGalonaje);
            this.pnlCombustible.Controls.Add(this.cmbTipoCombustible);
            this.pnlCombustible.Controls.Add(this.labelTipoComb);
            this.pnlCombustible.Location = new System.Drawing.Point(470, 240);
            this.pnlCombustible.Name = "pnlCombustible";
            this.pnlCombustible.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCombustible.Size = new System.Drawing.Size(390, 100);
            this.pnlCombustible.TabIndex = 10;
            // 
            // txtGalonaje
            // 
            this.txtGalonaje.Location = new System.Drawing.Point(120, 58);
            this.txtGalonaje.Name = "txtGalonaje";
            this.txtGalonaje.Size = new System.Drawing.Size(240, 22);
            this.txtGalonaje.TabIndex = 12;
            // 
            // labelGalonaje
            // 
            this.labelGalonaje.AutoSize = true;
            this.labelGalonaje.Location = new System.Drawing.Point(20, 61);
            this.labelGalonaje.Name = "labelGalonaje";
            this.labelGalonaje.Size = new System.Drawing.Size(64, 16);
            this.labelGalonaje.TabIndex = 2;
            this.labelGalonaje.Text = "Galonaje:";
            // 
            // cmbTipoCombustible
            // 
            this.cmbTipoCombustible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCombustible.FormattingEnabled = true;
            this.cmbTipoCombustible.Location = new System.Drawing.Point(120, 20);
            this.cmbTipoCombustible.Name = "cmbTipoCombustible";
            this.cmbTipoCombustible.Size = new System.Drawing.Size(240, 24);
            this.cmbTipoCombustible.TabIndex = 11;
            // 
            // labelTipoComb
            // 
            this.labelTipoComb.AutoSize = true;
            this.labelTipoComb.Location = new System.Drawing.Point(20, 23);
            this.labelTipoComb.Name = "labelTipoComb";
            this.labelTipoComb.Size = new System.Drawing.Size(98, 16);
            this.labelTipoComb.TabIndex = 0;
            this.labelTipoComb.Text = "Tipo combustible:";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFooter.Controls.Add(this.btnCancelar);
            this.panelFooter.Controls.Add(this.btnGuardar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 440);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20);
            this.panelFooter.Size = new System.Drawing.Size(900, 70);
            this.panelFooter.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(646, 18);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 34);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(770, 18);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 34);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // IngresarGastoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(900, 510);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Name = "IngresarGastoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ingresar Gasto";
            this.Load += new System.EventHandler(this.IngresarGastoForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.pnlCombustible.ResumeLayout(false);
            this.pnlCombustible.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblOrdenTitulo;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.ComboBox cmbTipoGasto;
        private System.Windows.Forms.Label labelTipo;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label labelFecha;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label labelSerie;
        private System.Windows.Forms.TextBox txtNoFactura;
        private System.Windows.Forms.Label labelNoFactura;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label labelNit;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Label labelProveedor;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.Label labelConcepto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label labelMonto;
        private System.Windows.Forms.Panel pnlCombustible;
        private System.Windows.Forms.ComboBox cmbTipoCombustible;
        private System.Windows.Forms.Label labelTipoComb;
        private System.Windows.Forms.TextBox txtGalonaje;
        private System.Windows.Forms.Label labelGalonaje;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}