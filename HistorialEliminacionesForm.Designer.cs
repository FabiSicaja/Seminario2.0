namespace Proyecto
{
    partial class HistorialEliminacionesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistorialEliminacionesForm));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Controls.Add(this.btnBuscar);
            this.panelHeader.Controls.Add(this.txtBuscar);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 81);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(455, 81);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Historial de Eliminaciones";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(897, 30);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 36);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(620, 32);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(271, 30);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContent.Controls.Add(this.dgvHistorial);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 81);
            this.panelContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(1000, 440);
            this.panelContent.TabIndex = 1;
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.Location = new System.Drawing.Point(20, 20);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.RowTemplate.Height = 24;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(960, 400);
            this.dgvHistorial.TabIndex = 1;
            this.dgvHistorial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorial_CellContentClick);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnCerrar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 521);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20);
            this.panelFooter.Size = new System.Drawing.Size(1000, 80);
            this.panelFooter.TabIndex = 2;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(880, 20);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 39);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // HistorialEliminacionesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 601);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(799, 499);
            this.Name = "HistorialEliminacionesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "INSELEC S.A. - Historial de Eliminaciones";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnCerrar;
    }
}