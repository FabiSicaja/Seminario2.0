namespace INSELEC
{
    partial class GestionarUsuariosForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionarUsuariosForm));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.panelForm = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.rbTechnician = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
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
            this.panelHeader.Size = new System.Drawing.Size(852, 81);
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
            this.label1.Size = new System.Drawing.Size(436, 81);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gestión de Usuarios";
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
            this.panelContent.Size = new System.Drawing.Size(852, 443);
            this.panelContent.TabIndex = 1;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvUsuarios);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 180);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(20);
            this.panelGrid.Size = new System.Drawing.Size(852, 263);
            this.panelGrid.TabIndex = 2;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.Location = new System.Drawing.Point(20, 20);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowHeadersWidth = 51;
            this.dgvUsuarios.RowTemplate.Height = 24;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(812, 223);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            this.dgvUsuarios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellDoubleClick);
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.Controls.Add(this.label5);
            this.panelForm.Controls.Add(this.rbAdmin);
            this.panelForm.Controls.Add(this.rbTechnician);
            this.panelForm.Controls.Add(this.label3);
            this.panelForm.Controls.Add(this.txtPassword);
            this.panelForm.Controls.Add(this.label2);
            this.panelForm.Controls.Add(this.txtUsername);
            this.panelForm.Controls.Add(this.btnGuardar);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(20);
            this.panelForm.Size = new System.Drawing.Size(852, 180);
            this.panelForm.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(32, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "Tipo de Usuario:";
            // 
            // rbAdmin
            // 
            this.rbAdmin.AutoSize = true;
            this.rbAdmin.Checked = true;
            this.rbAdmin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rbAdmin.Location = new System.Drawing.Point(200, 95);
            this.rbAdmin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(81, 27);
            this.rbAdmin.TabIndex = 4;
            this.rbAdmin.TabStop = true;
            this.rbAdmin.Text = "Admin";
            this.rbAdmin.UseVisualStyleBackColor = true;
            // 
            // rbTechnician
            // 
            this.rbTechnician.AutoSize = true;
            this.rbTechnician.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbTechnician.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rbTechnician.Location = new System.Drawing.Point(295, 95);
            this.rbTechnician.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbTechnician.Name = "rbTechnician";
            this.rbTechnician.Size = new System.Drawing.Size(87, 27);
            this.rbTechnician.TabIndex = 5;
            this.rbTechnician.TabStop = true;
            this.rbTechnician.Text = "Técnico";
            this.rbTechnician.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(441, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(556, 36);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(263, 30);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(32, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Usuario:";
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(124, 36);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(311, 30);
            this.txtUsername.TabIndex = 1;
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
            this.btnGuardar.Location = new System.Drawing.Point(672, 128);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 39);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnEditar);
            this.panelFooter.Controls.Add(this.btnEliminar);
            this.panelFooter.Controls.Add(this.btnCerrar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 524);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20);
            this.panelFooter.Size = new System.Drawing.Size(852, 80);
            this.panelFooter.TabIndex = 3;
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(467, 20);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(120, 39);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
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
            this.btnEliminar.Location = new System.Drawing.Point(592, 20);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 39);
            this.btnEliminar.TabIndex = 1;
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
            this.btnCerrar.Location = new System.Drawing.Point(717, 20);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 39);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // GestionarUsuariosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(852, 604);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(699, 499);
            this.Name = "GestionarUsuariosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "INSELEC S.A. - Gestión de Usuarios";
            this.Load += new System.EventHandler(this.GestionarUsuariosForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbTechnician;
        private System.Windows.Forms.RadioButton rbAdmin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnEditar;
    }
}