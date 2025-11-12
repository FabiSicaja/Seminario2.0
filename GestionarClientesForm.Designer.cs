namespace INSELEC
{
    partial class GestionarClientesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionarClientesForm));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.panelForm = new System.Windows.Forms.Panel();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.labelNit = new System.Windows.Forms.Label();
            this.txtContactos = new System.Windows.Forms.TextBox();
            this.labelContactos = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.labelDireccion = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.panelForm.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 81);
            this.panelHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(423, 81);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gestión de Clientes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContent.Controls.Add(this.panelGrid);
            this.panelContent.Controls.Add(this.panelForm);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 81);
            this.panelContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1000, 443);
            this.panelContent.TabIndex = 1;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvClientes);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 180);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelGrid.Size = new System.Drawing.Size(1000, 263);
            this.panelGrid.TabIndex = 2;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientes.Location = new System.Drawing.Point(20, 20);
            this.dgvClientes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersWidth = 51;
            this.dgvClientes.RowTemplate.Height = 24;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(960, 223);
            this.dgvClientes.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.Controls.Add(this.txtNit);
            this.panelForm.Controls.Add(this.labelNit);
            this.panelForm.Controls.Add(this.txtContactos);
            this.panelForm.Controls.Add(this.labelContactos);
            this.panelForm.Controls.Add(this.txtDireccion);
            this.panelForm.Controls.Add(this.labelDireccion);
            this.panelForm.Controls.Add(this.txtNombre);
            this.panelForm.Controls.Add(this.labelNombre);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelForm.Size = new System.Drawing.Size(1000, 180);
            this.panelForm.TabIndex = 1;
            // 
            // txtNit
            // 
            this.txtNit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNit.Location = new System.Drawing.Point(533, 76);
            this.txtNit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(433, 30);
            this.txtNit.TabIndex = 2;
            // 
            // labelNit
            // 
            this.labelNit.AutoSize = true;
            this.labelNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelNit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNit.Location = new System.Drawing.Point(441, 79);
            this.labelNit.Name = "labelNit";
            this.labelNit.Size = new System.Drawing.Size(41, 23);
            this.labelNit.TabIndex = 7;
            this.labelNit.Text = "NIT:";
            // 
            // txtContactos
            // 
            this.txtContactos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContactos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContactos.Location = new System.Drawing.Point(124, 116);
            this.txtContactos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtContactos.Name = "txtContactos";
            this.txtContactos.Size = new System.Drawing.Size(843, 30);
            this.txtContactos.TabIndex = 4;
            // 
            // labelContactos
            // 
            this.labelContactos.AutoSize = true;
            this.labelContactos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelContactos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelContactos.Location = new System.Drawing.Point(32, 119);
            this.labelContactos.Name = "labelContactos";
            this.labelContactos.Size = new System.Drawing.Size(91, 23);
            this.labelContactos.TabIndex = 5;
            this.labelContactos.Text = "Contactos:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDireccion.Location = new System.Drawing.Point(124, 76);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(297, 30);
            this.txtDireccion.TabIndex = 1;
            // 
            // labelDireccion
            // 
            this.labelDireccion.AutoSize = true;
            this.labelDireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelDireccion.Location = new System.Drawing.Point(32, 79);
            this.labelDireccion.Name = "labelDireccion";
            this.labelDireccion.Size = new System.Drawing.Size(85, 23);
            this.labelDireccion.TabIndex = 3;
            this.labelDireccion.Text = "Dirección:";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(124, 36);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(843, 30);
            this.txtNombre.TabIndex = 0;
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelNombre.Location = new System.Drawing.Point(32, 39);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(77, 23);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre:";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnGuardar);
            this.panelFooter.Controls.Add(this.btnEliminar);
            this.panelFooter.Controls.Add(this.btnCerrar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 524);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelFooter.Size = new System.Drawing.Size(1000, 80);
            this.panelFooter.TabIndex = 3;
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
            this.btnGuardar.Location = new System.Drawing.Point(615, 18);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 39);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(35)))), ((int)(((byte)(51)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(740, 20);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 39);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            this.btnCerrar.Location = new System.Drawing.Point(865, 20);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 39);
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // GestionarClientesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 604);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(799, 598);
            this.Name = "GestionarClientesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INSELEC S.A. - Gestión de Clientes";
            this.panelHeader.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label labelNit;
        private System.Windows.Forms.TextBox txtContactos;
        private System.Windows.Forms.Label labelContactos;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label labelDireccion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCerrar;
    }
}