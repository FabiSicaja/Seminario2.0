using Proyecto_de_Seminario;
using Proyecto;

namespace Proyecto
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnGestionarProveedores = new System.Windows.Forms.Button();
            this.btnGestionarClientes = new System.Windows.Forms.Button();
            this.btnGestionarUsuarios = new System.Windows.Forms.Button();
            this.btnAnularOrden = new System.Windows.Forms.Button();
            this.btnModificarOrden = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.labelLogo = new System.Windows.Forms.Label();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnCerrarOrden = new System.Windows.Forms.Button();
            this.btnHistorialEliminaciones = new System.Windows.Forms.Button();
            this.btnVerGastos = new System.Windows.Forms.Button();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.labelTotalCerradas = new System.Windows.Forms.Label();
            this.labelCerradas = new System.Windows.Forms.Label();
            this.labelTotalAbiertas = new System.Windows.Forms.Label();
            this.labelAbiertas = new System.Windows.Forms.Label();
            this.labelTotalOrdenes = new System.Windows.Forms.Label();
            this.labelOrdenesCount = new System.Windows.Forms.Label();
            this.labelOrdenes = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(188, 575);
            this.panelSidebar.TabIndex = 0;
            this.panelSidebar.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 648);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(251, 60);
            this.btnLogout.TabIndex = 100;
            this.btnLogout.Text = "   Cerrar Sesión";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnGestionarProveedores
            // 
            this.btnGestionarProveedores.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionarProveedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionarProveedores.FlatAppearance.BorderSize = 0;
            this.btnGestionarProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarProveedores.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnGestionarProveedores.ForeColor = System.Drawing.Color.White;
            this.btnGestionarProveedores.Location = new System.Drawing.Point(0, 570);
            this.btnGestionarProveedores.Margin = new System.Windows.Forms.Padding(0);
            this.btnGestionarProveedores.Name = "btnGestionarProveedores";
            this.btnGestionarProveedores.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnGestionarProveedores.Size = new System.Drawing.Size(251, 40);
            this.btnGestionarProveedores.TabIndex = 12;
            this.btnGestionarProveedores.Text = "Gestionar Proveedores";
            this.btnGestionarProveedores.UseVisualStyleBackColor = true;
            this.btnGestionarProveedores.Click += new System.EventHandler(this.btnGestionarProveedores_Click);
            // 
            // btnGestionarClientes
            // 
            this.btnGestionarClientes.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionarClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionarClientes.FlatAppearance.BorderSize = 0;
            this.btnGestionarClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarClientes.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnGestionarClientes.ForeColor = System.Drawing.Color.White;
            this.btnGestionarClientes.Location = new System.Drawing.Point(0, 519);
            this.btnGestionarClientes.Margin = new System.Windows.Forms.Padding(0);
            this.btnGestionarClientes.Name = "btnGestionarClientes";
            this.btnGestionarClientes.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnGestionarClientes.Size = new System.Drawing.Size(251, 51);
            this.btnGestionarClientes.TabIndex = 11;
            this.btnGestionarClientes.Text = "Gestionar Clientes";
            this.btnGestionarClientes.UseVisualStyleBackColor = true;
            this.btnGestionarClientes.Click += new System.EventHandler(this.btnGestionarClientes_Click);
            // 
            // btnGestionarUsuarios
            // 
            this.btnGestionarUsuarios.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionarUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionarUsuarios.FlatAppearance.BorderSize = 0;
            this.btnGestionarUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarUsuarios.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnGestionarUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnGestionarUsuarios.Location = new System.Drawing.Point(0, 460);
            this.btnGestionarUsuarios.Margin = new System.Windows.Forms.Padding(0);
            this.btnGestionarUsuarios.Name = "btnGestionarUsuarios";
            this.btnGestionarUsuarios.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnGestionarUsuarios.Size = new System.Drawing.Size(251, 59);
            this.btnGestionarUsuarios.TabIndex = 10;
            this.btnGestionarUsuarios.Text = "Gestionar Usuarios";
            this.btnGestionarUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionarUsuarios.Click += new System.EventHandler(this.btnGestionarUsuarios_Click);
            // 
            // btnAnularOrden
            // 
            this.btnAnularOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnAnularOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAnularOrden.FlatAppearance.BorderSize = 0;
            this.btnAnularOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnularOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnAnularOrden.ForeColor = System.Drawing.Color.White;
            this.btnAnularOrden.Location = new System.Drawing.Point(0, 400);
            this.btnAnularOrden.Margin = new System.Windows.Forms.Padding(0);
            this.btnAnularOrden.Name = "btnAnularOrden";
            this.btnAnularOrden.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnAnularOrden.Size = new System.Drawing.Size(251, 60);
            this.btnAnularOrden.TabIndex = 9;
            this.btnAnularOrden.Text = "   Anular Orden";
            this.btnAnularOrden.UseVisualStyleBackColor = false;
            this.btnAnularOrden.Click += new System.EventHandler(this.btnAnularOrden_Click);
            // 
            // btnModificarOrden
            // 
            this.btnModificarOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnModificarOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnModificarOrden.FlatAppearance.BorderSize = 0;
            this.btnModificarOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnModificarOrden.ForeColor = System.Drawing.Color.White;
            this.btnModificarOrden.Location = new System.Drawing.Point(0, 340);
            this.btnModificarOrden.Margin = new System.Windows.Forms.Padding(0);
            this.btnModificarOrden.Name = "btnModificarOrden";
            this.btnModificarOrden.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnModificarOrden.Size = new System.Drawing.Size(251, 60);
            this.btnModificarOrden.TabIndex = 8;
            this.btnModificarOrden.Text = "   Modificar Orden";
            this.btnModificarOrden.UseVisualStyleBackColor = false;
            this.btnModificarOrden.Click += new System.EventHandler(this.btnModificarOrden_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(0, 526);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(188, 49);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "   Cerrar Sesión";
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.labelLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 240);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(251, 100);
            this.panelLogo.TabIndex = 6;
            // 
            // labelLogo
            // 
            this.labelLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelLogo.ForeColor = System.Drawing.Color.White;
            this.labelLogo.Location = new System.Drawing.Point(0, 0);
            this.labelLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(188, 81);
            this.labelLogo.TabIndex = 0;
            this.labelLogo.Text = "INSELEC, S.A.";
            this.labelLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLogo.Click += new System.EventHandler(this.labelLogo_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.BackColor = System.Drawing.Color.Transparent;
            this.btnReporte.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReporte.FlatAppearance.BorderSize = 0;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnReporte.ForeColor = System.Drawing.Color.White;
            this.btnReporte.Location = new System.Drawing.Point(0, 180);
            this.btnReporte.Margin = new System.Windows.Forms.Padding(0);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnReporte.Size = new System.Drawing.Size(251, 60);
            this.btnReporte.TabIndex = 5;
            this.btnReporte.Text = "   Generar Reporte";
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btnCerrarOrden
            // 
            this.btnCerrarOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnCerrarOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCerrarOrden.FlatAppearance.BorderSize = 0;
            this.btnCerrarOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnCerrarOrden.ForeColor = System.Drawing.Color.White;
            this.btnCerrarOrden.Location = new System.Drawing.Point(0, 120);
            this.btnCerrarOrden.Margin = new System.Windows.Forms.Padding(0);
            this.btnCerrarOrden.Name = "btnCerrarOrden";
            this.btnCerrarOrden.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnCerrarOrden.Size = new System.Drawing.Size(251, 60);
            this.btnCerrarOrden.TabIndex = 4;
            this.btnCerrarOrden.Text = "   Cerrar Orden";
            this.btnCerrarOrden.UseVisualStyleBackColor = false;
            this.btnCerrarOrden.Click += new System.EventHandler(this.btnCerrarOrden_Click);
            // 
            // btnHistorialEliminaciones
            // 
            this.btnHistorialEliminaciones.BackColor = System.Drawing.Color.Transparent;
            this.btnHistorialEliminaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistorialEliminaciones.FlatAppearance.BorderSize = 0;
            this.btnHistorialEliminaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialEliminaciones.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnHistorialEliminaciones.ForeColor = System.Drawing.Color.White;
            this.btnHistorialEliminaciones.Location = new System.Drawing.Point(0, 60);
            this.btnHistorialEliminaciones.Margin = new System.Windows.Forms.Padding(0);
            this.btnHistorialEliminaciones.Name = "btnHistorialEliminaciones";
            this.btnHistorialEliminaciones.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnHistorialEliminaciones.Size = new System.Drawing.Size(251, 60);
            this.btnHistorialEliminaciones.TabIndex = 3;
            this.btnHistorialEliminaciones.Text = "   Historial de Eliminaciones";
            this.btnHistorialEliminaciones.UseVisualStyleBackColor = true;
            this.btnHistorialEliminaciones.Click += new System.EventHandler(this.btnHistorialEliminaciones_Click);
            // 
            // btnVerGastos
            // 
            this.btnVerGastos.BackColor = System.Drawing.Color.Transparent;
            this.btnVerGastos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVerGastos.FlatAppearance.BorderSize = 0;
            this.btnVerGastos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerGastos.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnVerGastos.ForeColor = System.Drawing.Color.White;
            this.btnVerGastos.Location = new System.Drawing.Point(0, 0);
            this.btnVerGastos.Margin = new System.Windows.Forms.Padding(0);
            this.btnVerGastos.Name = "btnVerGastos";
            this.btnVerGastos.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnVerGastos.Size = new System.Drawing.Size(251, 60);
            this.btnVerGastos.TabIndex = 2;
            this.btnVerGastos.Text = "   Ver Gastos";
            this.btnVerGastos.UseVisualStyleBackColor = false;
            this.btnVerGastos.Click += new System.EventHandler(this.btnVerGastos_Click);
            // 
            // btnCrearOrden
            //
            this.btnCrearOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnCrearOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCrearOrden.FlatAppearance.BorderSize = 0;
            this.btnCrearOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnCrearOrden.ForeColor = System.Drawing.Color.White;
            this.btnCrearOrden.Location = new System.Drawing.Point(0, 0); // el Dock.Top ignora Y
            this.btnCrearOrden.Margin = new System.Windows.Forms.Padding(0);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnCrearOrden.Size = new System.Drawing.Size(251, 60);   // ← antes estaba 0
            this.btnCrearOrden.TabIndex = 1;
            this.btnCrearOrden.Text = "   Crear Orden";
            this.btnCrearOrden.UseVisualStyleBackColor = false;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // apilar controles del sidebar (último agregado arriba con Dock.Top)
            // 
            this.panelSidebar.Controls.Add(this.btnGestionarProveedores);
            this.panelSidebar.Controls.Add(this.btnGestionarClientes);
            this.panelSidebar.Controls.Add(this.btnGestionarUsuarios);
            this.panelSidebar.Controls.Add(this.btnAnularOrden);
            this.panelSidebar.Controls.Add(this.btnModificarOrden);
            this.panelSidebar.Controls.Add(this.panelLogo);
            this.panelSidebar.Controls.Add(this.btnReporte);
            this.panelSidebar.Controls.Add(this.btnCerrarOrden);
            this.panelSidebar.Controls.Add(this.btnHistorialEliminaciones);
            this.panelSidebar.Controls.Add(this.btnVerGastos);
            this.panelSidebar.Controls.Add(this.btnCrearOrden);   // ← Queda ARRIBA
            this.panelSidebar.Controls.Add(this.btnLogout);       // ← Siempre abajo
            this.panelSidebar.ResumeLayout(false);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.btnBuscar);
            this.panelHeader.Controls.Add(this.txtBuscarCliente);
            this.panelHeader.Controls.Add(this.labelWelcome);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(251, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(714, 71);
            this.panelHeader.TabIndex = 1;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Location = new System.Drawing.Point(856, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 27);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscarCliente.Location = new System.Drawing.Point(590, 28);
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(260, 25);
            this.txtBuscarCliente.TabIndex = 4;
            // 
            // labelWelcome
            // 
            this.labelWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWelcome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelWelcome.Location = new System.Drawing.Point(609, 6);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(331, 19);
            this.labelWelcome.TabIndex = 1;
            this.labelWelcome.Text = "Bienvenido: [Usuario]";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(29, 27);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(267, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Gestión de Órdenes";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.dgvOrdenes);
            this.panelContent.Controls.Add(this.panelStats);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(251, 87);
            this.panelContent.Margin = new System.Windows.Forms.Padding(0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(714, 504);
            this.panelContent.TabIndex = 2;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenes.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenes.Location = new System.Drawing.Point(0, 93);
            this.dgvOrdenes.Margin = new System.Windows.Forms.Padding(0);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.RowHeadersWidth = 51;
            this.dgvOrdenes.RowTemplate.Height = 24;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(952, 528);
            this.dgvOrdenes.TabIndex = 2;
            this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenes_CellDoubleClick);
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.White;
            this.panelStats.Controls.Add(this.labelTotalCerradas);
            this.panelStats.Controls.Add(this.labelCerradas);
            this.panelStats.Controls.Add(this.labelTotalAbiertas);
            this.panelStats.Controls.Add(this.labelAbiertas);
            this.panelStats.Controls.Add(this.labelTotalOrdenes);
            this.panelStats.Controls.Add(this.labelOrdenesCount);
            this.panelStats.Controls.Add(this.labelOrdenes);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 0);
            this.panelStats.Margin = new System.Windows.Forms.Padding(0);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(714, 76);
            this.panelStats.TabIndex = 1;
            // 
            // labelTotalCerradas
            // 
            this.labelTotalCerradas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalCerradas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTotalCerradas.ForeColor = System.Drawing.Color.Green;
            this.labelTotalCerradas.Location = new System.Drawing.Point(612, 32);
            this.labelTotalCerradas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalCerradas.Name = "labelTotalCerradas";
            this.labelTotalCerradas.Size = new System.Drawing.Size(88, 20);
            this.labelTotalCerradas.TabIndex = 6;
            this.labelTotalCerradas.Text = "0";
            this.labelTotalCerradas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCerradas
            // 
            this.labelCerradas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCerradas.AutoSize = true;
            this.labelCerradas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCerradas.Location = new System.Drawing.Point(612, 11);
            this.labelCerradas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCerradas.Name = "labelCerradas";
            this.labelCerradas.Size = new System.Drawing.Size(126, 15);
            this.labelCerradas.TabIndex = 5;
            this.labelCerradas.Text = "Órdenes Cerradas";
            // 
            // labelTotalAbiertas
            // 
            this.labelTotalAbiertas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTotalAbiertas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTotalAbiertas.ForeColor = System.Drawing.Color.Orange;
            this.labelTotalAbiertas.Location = new System.Drawing.Point(354, 32);
            this.labelTotalAbiertas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalAbiertas.Name = "labelTotalAbiertas";
            this.labelTotalAbiertas.Size = new System.Drawing.Size(88, 20);
            this.labelTotalAbiertas.TabIndex = 4;
            this.labelTotalAbiertas.Text = "0";
            this.labelTotalAbiertas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAbiertas
            // 
            this.labelAbiertas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelAbiertas.AutoSize = true;
            this.labelAbiertas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelAbiertas.Location = new System.Drawing.Point(354, 11);
            this.labelAbiertas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAbiertas.Name = "labelAbiertas";
            this.labelAbiertas.Size = new System.Drawing.Size(121, 15);
            this.labelAbiertas.TabIndex = 3;
            this.labelAbiertas.Text = "Órdenes Abiertas";
            // 
            // labelTotalOrdenes
            // 
            this.labelTotalOrdenes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTotalOrdenes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.labelTotalOrdenes.Location = new System.Drawing.Point(22, 32);
            this.labelTotalOrdenes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalOrdenes.Name = "labelTotalOrdenes";
            this.labelTotalOrdenes.Size = new System.Drawing.Size(88, 20);
            this.labelTotalOrdenes.TabIndex = 2;
            this.labelTotalOrdenes.Text = "0";
            this.labelTotalOrdenes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOrdenesCount
            // 
            this.labelOrdenesCount.AutoSize = true;
            this.labelOrdenesCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelOrdenesCount.Location = new System.Drawing.Point(22, 11);
            this.labelOrdenesCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOrdenesCount.Name = "labelOrdenesCount";
            this.labelOrdenesCount.Size = new System.Drawing.Size(120, 15);
            this.labelOrdenesCount.TabIndex = 1;
            this.labelOrdenesCount.Text = "Total de Órdenes";
            // 
            // labelOrdenes
            // 
            this.labelOrdenes.AutoSize = true;
            this.labelOrdenes.Location = new System.Drawing.Point(22, 11);
            this.labelOrdenes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOrdenes.Name = "labelOrdenes";
            this.labelOrdenes.Size = new System.Drawing.Size(0, 13);
            this.labelOrdenes.TabIndex = 0;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenes.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenes.Location = new System.Drawing.Point(0, 0);
            this.dgvOrdenes.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.RowHeadersWidth = 51;
            this.dgvOrdenes.RowTemplate.Height = 24;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(714, 504);
            this.dgvOrdenes.TabIndex = 0;
            this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenes_CellDoubleClick);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 575);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(980, 600);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INSELEC, S.A - Gestión de Órdenes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminForm_FormClosed);
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label labelLogo;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnCerrarOrden;
        private System.Windows.Forms.Button btnHistorialEliminaciones;
        private System.Windows.Forms.Button btnVerGastos;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button btnModificarOrden;
        private System.Windows.Forms.Button btnAnularOrden;
        private System.Windows.Forms.Button btnGestionarUsuarios;
        private System.Windows.Forms.Button btnGestionarClientes;
        private System.Windows.Forms.Button btnGestionarProveedores;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.Button btnBuscar;

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label labelTotalCerradas;
        private System.Windows.Forms.Label labelCerradas;
        private System.Windows.Forms.Label labelTotalAbiertas;
        private System.Windows.Forms.Label labelAbiertas;
        private System.Windows.Forms.Label labelTotalOrdenes;
        private System.Windows.Forms.Label labelOrdenesCount;
        private System.Windows.Forms.Label labelOrdenes;
    }
}