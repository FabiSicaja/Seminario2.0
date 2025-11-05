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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IngresarGastoForm));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblOrden = new System.Windows.Forms.Label();
            this.lblOrdenTitulo = new System.Windows.Forms.Label();
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
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelHeader.Controls.Add(this.lblOrden);
            this.panelHeader.Controls.Add(this.lblOrdenTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 81);
            this.panelHeader.TabIndex = 0;
            // 
            // lblOrden
            // 
            this.lblOrden.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblOrden.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOrden.ForeColor = System.Drawing.Color.White;
            this.lblOrden.Location = new System.Drawing.Point(700, 0);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(300, 81);
            this.lblOrden.TabIndex = 1;
            this.lblOrden.Text = "Orden: -";
            this.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrdenTitulo
            // 
            this.lblOrdenTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblOrdenTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdenTitulo.ForeColor = System.Drawing.Color.White;
            this.lblOrdenTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblOrdenTitulo.Name = "lblOrdenTitulo";
            this.lblOrdenTitulo.Size = new System.Drawing.Size(300, 81);
            this.lblOrdenTitulo.TabIndex = 0;
            this.lblOrdenTitulo.Text = "Ingreso de Gasto";
            this.lblOrdenTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOrdenTitulo.Click += new System.EventHandler(this.lblOrdenTitulo_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
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
            this.panelContent.Location = new System.Drawing.Point(0, 81);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(1000, 519);
            this.panelContent.TabIndex = 1;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProveedor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProveedor.Location = new System.Drawing.Point(533, 116);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(434, 25);
            this.txtProveedor.TabIndex = 7;
            // 
            // labelProveedor
            // 
            this.labelProveedor.AutoSize = true;
            this.labelProveedor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelProveedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelProveedor.Location = new System.Drawing.Point(441, 119);
            this.labelProveedor.Name = "labelProveedor";
            this.labelProveedor.Size = new System.Drawing.Size(75, 19);
            this.labelProveedor.TabIndex = 50;
            this.labelProveedor.Text = "Proveedor:";
            // 
            // txtNit
            // 
            this.txtNit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNit.Location = new System.Drawing.Point(533, 76);
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(434, 25);
            this.txtNit.TabIndex = 6;
            // 
            // labelNit
            // 
            this.labelNit.AutoSize = true;
            this.labelNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelNit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNit.Location = new System.Drawing.Point(441, 79);
            this.labelNit.Name = "labelNit";
            this.labelNit.Size = new System.Drawing.Size(33, 19);
            this.labelNit.TabIndex = 48;
            this.labelNit.Text = "NIT:";
            // 
            // txtNoFactura
            // 
            this.txtNoFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoFactura.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNoFactura.Location = new System.Drawing.Point(533, 36);
            this.txtNoFactura.Name = "txtNoFactura";
            this.txtNoFactura.Size = new System.Drawing.Size(434, 25);
            this.txtNoFactura.TabIndex = 5;
            // 
            // labelNoFactura
            // 
            this.labelNoFactura.AutoSize = true;
            this.labelNoFactura.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelNoFactura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNoFactura.Location = new System.Drawing.Point(441, 39);
            this.labelNoFactura.Name = "labelNoFactura";
            this.labelNoFactura.Size = new System.Drawing.Size(79, 19);
            this.labelNoFactura.TabIndex = 46;
            this.labelNoFactura.Text = "No. factura:";
            // 
            // txtSerie
            // 
            this.txtSerie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSerie.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSerie.Location = new System.Drawing.Point(124, 116);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(297, 25);
            this.txtSerie.TabIndex = 3;
            // 
            // labelSerie
            // 
            this.labelSerie.AutoSize = true;
            this.labelSerie.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelSerie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSerie.Location = new System.Drawing.Point(32, 119);
            this.labelSerie.Name = "labelSerie";
            this.labelSerie.Size = new System.Drawing.Size(41, 19);
            this.labelSerie.TabIndex = 44;
            this.labelSerie.Text = "Serie:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(124, 76);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(297, 25);
            this.dtpFecha.TabIndex = 2;
            // 
            // labelFecha
            // 
            this.labelFecha.AutoSize = true;
            this.labelFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelFecha.Location = new System.Drawing.Point(32, 79);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(47, 19);
            this.labelFecha.TabIndex = 42;
            this.labelFecha.Text = "Fecha:";
            // 
            // cmbTipoGasto
            // 
            this.cmbTipoGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoGasto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoGasto.FormattingEnabled = true;
            this.cmbTipoGasto.Location = new System.Drawing.Point(124, 36);
            this.cmbTipoGasto.Name = "cmbTipoGasto";
            this.cmbTipoGasto.Size = new System.Drawing.Size(297, 25);
            this.cmbTipoGasto.TabIndex = 1;
            this.cmbTipoGasto.SelectedIndexChanged += new System.EventHandler(this.cmbTipoGasto_SelectedIndexChanged);
            // 
            // labelTipo
            // 
            this.labelTipo.AutoSize = true;
            this.labelTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTipo.Location = new System.Drawing.Point(32, 39);
            this.labelTipo.Name = "labelTipo";
            this.labelTipo.Size = new System.Drawing.Size(95, 19);
            this.labelTipo.TabIndex = 40;
            this.labelTipo.Text = "Tipo de gasto:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.Location = new System.Drawing.Point(124, 156);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(843, 100);
            this.txtDescripcion.TabIndex = 8;
            this.txtDescripcion.Text = "";
            // 
            // labelConcepto
            // 
            this.labelConcepto.AutoSize = true;
            this.labelConcepto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelConcepto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelConcepto.Location = new System.Drawing.Point(32, 159);
            this.labelConcepto.Name = "labelConcepto";
            this.labelConcepto.Size = new System.Drawing.Size(71, 19);
            this.labelConcepto.TabIndex = 38;
            this.labelConcepto.Text = "Concepto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMonto.Location = new System.Drawing.Point(124, 272);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(297, 25);
            this.txtMonto.TabIndex = 9;
            // 
            // labelMonto
            // 
            this.labelMonto.AutoSize = true;
            this.labelMonto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelMonto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMonto.Location = new System.Drawing.Point(32, 275);
            this.labelMonto.Name = "labelMonto";
            this.labelMonto.Size = new System.Drawing.Size(54, 19);
            this.labelMonto.TabIndex = 36;
            this.labelMonto.Text = "Monto:";
            // 
            // pnlCombustible
            // 
            this.pnlCombustible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCombustible.BackColor = System.Drawing.Color.White;
            this.pnlCombustible.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCombustible.Controls.Add(this.txtGalonaje);
            this.pnlCombustible.Controls.Add(this.labelGalonaje);
            this.pnlCombustible.Controls.Add(this.cmbTipoCombustible);
            this.pnlCombustible.Controls.Add(this.labelTipoComb);
            this.pnlCombustible.Location = new System.Drawing.Point(445, 260);
            this.pnlCombustible.Name = "pnlCombustible";
            this.pnlCombustible.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCombustible.Size = new System.Drawing.Size(522, 116);
            this.pnlCombustible.TabIndex = 10;
            // 
            // txtGalonaje
            // 
            this.txtGalonaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGalonaje.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGalonaje.Location = new System.Drawing.Point(150, 68);
            this.txtGalonaje.Name = "txtGalonaje";
            this.txtGalonaje.Size = new System.Drawing.Size(347, 25);
            this.txtGalonaje.TabIndex = 12;
            // 
            // labelGalonaje
            // 
            this.labelGalonaje.AutoSize = true;
            this.labelGalonaje.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelGalonaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelGalonaje.Location = new System.Drawing.Point(20, 71);
            this.labelGalonaje.Name = "labelGalonaje";
            this.labelGalonaje.Size = new System.Drawing.Size(65, 19);
            this.labelGalonaje.TabIndex = 2;
            this.labelGalonaje.Text = "Galonaje:";
            // 
            // cmbTipoCombustible
            // 
            this.cmbTipoCombustible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoCombustible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCombustible.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoCombustible.FormattingEnabled = true;
            this.cmbTipoCombustible.Location = new System.Drawing.Point(150, 28);
            this.cmbTipoCombustible.Name = "cmbTipoCombustible";
            this.cmbTipoCombustible.Size = new System.Drawing.Size(347, 25);
            this.cmbTipoCombustible.TabIndex = 11;
            // 
            // labelTipoComb
            // 
            this.labelTipoComb.AutoSize = true;
            this.labelTipoComb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelTipoComb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTipoComb.Location = new System.Drawing.Point(20, 31);
            this.labelTipoComb.Name = "labelTipoComb";
            this.labelTipoComb.Size = new System.Drawing.Size(116, 19);
            this.labelTipoComb.TabIndex = 0;
            this.labelTipoComb.Text = "Tipo combustible:";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnCancelar);
            this.panelFooter.Controls.Add(this.btnGuardar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 600);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20);
            this.panelFooter.Size = new System.Drawing.Size(1000, 80);
            this.panelFooter.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(767, 20);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 40);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(883, 20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 40);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // IngresarGastoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "IngresarGastoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INSELEC S.A. - Ingresar Gasto";
            this.Load += new System.EventHandler(this.IngresarGastoForm_Load);
            this.panelHeader.ResumeLayout(false);
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