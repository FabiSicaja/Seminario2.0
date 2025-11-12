using INSELEC;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnGestionarProveedores = new System.Windows.Forms.Button();
            this.btnGestionarClientes = new System.Windows.Forms.Button();
            this.btnGestionarUsuarios = new System.Windows.Forms.Button();
            this.btnAnularOrden = new System.Windows.Forms.Button();
            this.btnModificarOrden = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnCerrarOrden = new System.Windows.Forms.Button();
            this.btnHistorialEliminaciones = new System.Windows.Forms.Button();
            this.btnVerGastos = new System.Windows.Forms.Button();
            this.btnCrearOrden = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.labelLogo = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelUserInfo = new System.Windows.Forms.Panel();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelCerradas = new System.Windows.Forms.Panel();
            this.labelTotalCerradas = new System.Windows.Forms.Label();
            this.labelCerradas = new System.Windows.Forms.Label();
            this.panelAbiertas = new System.Windows.Forms.Panel();
            this.labelTotalAbiertas = new System.Windows.Forms.Label();
            this.labelAbiertas = new System.Windows.Forms.Label();
            this.panelTotal = new System.Windows.Forms.Panel();
            this.labelTotalOrdenes = new System.Windows.Forms.Label();
            this.labelOrdenesCount = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelUserInfo.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.panelStats.SuspendLayout();
            this.panelCerradas.SuspendLayout();
            this.panelAbiertas.SuspendLayout();
            this.panelTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(55)))));
            this.panelSidebar.Controls.Add(this.panelMenu);
            this.panelSidebar.Controls.Add(this.panelLogo);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(280, 800);
            this.panelSidebar.TabIndex = 0;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelMenu.Controls.Add(this.btnLogout);
            this.panelMenu.Controls.Add(this.btnGestionarProveedores);
            this.panelMenu.Controls.Add(this.btnGestionarClientes);
            this.panelMenu.Controls.Add(this.btnGestionarUsuarios);
            this.panelMenu.Controls.Add(this.btnAnularOrden);
            this.panelMenu.Controls.Add(this.btnModificarOrden);
            this.panelMenu.Controls.Add(this.btnReporte);
            this.panelMenu.Controls.Add(this.btnCerrarOrden);
            this.panelMenu.Controls.Add(this.btnHistorialEliminaciones);
            this.panelMenu.Controls.Add(this.btnVerGastos);
            this.panelMenu.Controls.Add(this.btnCrearOrden);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 121);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Padding = new System.Windows.Forms.Padding(11, 0, 11, 20);
            this.panelMenu.Size = new System.Drawing.Size(280, 679);
            this.panelMenu.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(60)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(11, 599);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(258, 60);
            this.btnLogout.TabIndex = 100;
            this.btnLogout.Text = "🚪 Cerrar Sesión";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnGestionarProveedores
            // 
            this.btnGestionarProveedores.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionarProveedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionarProveedores.FlatAppearance.BorderSize = 0;
            this.btnGestionarProveedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnGestionarProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarProveedores.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarProveedores.ForeColor = System.Drawing.Color.White;
            this.btnGestionarProveedores.Location = new System.Drawing.Point(11, 450);
            this.btnGestionarProveedores.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGestionarProveedores.Name = "btnGestionarProveedores";
            this.btnGestionarProveedores.Size = new System.Drawing.Size(258, 50);
            this.btnGestionarProveedores.TabIndex = 9;
            this.btnGestionarProveedores.Text = "🏢 Gestionar Proveedores";
            this.btnGestionarProveedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionarProveedores.UseVisualStyleBackColor = false;
            this.btnGestionarProveedores.Click += new System.EventHandler(this.btnGestionarProveedores_Click);
            // 
            // btnGestionarClientes
            // 
            this.btnGestionarClientes.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionarClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionarClientes.FlatAppearance.BorderSize = 0;
            this.btnGestionarClientes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnGestionarClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarClientes.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarClientes.ForeColor = System.Drawing.Color.White;
            this.btnGestionarClientes.Location = new System.Drawing.Point(11, 400);
            this.btnGestionarClientes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGestionarClientes.Name = "btnGestionarClientes";
            this.btnGestionarClientes.Size = new System.Drawing.Size(258, 50);
            this.btnGestionarClientes.TabIndex = 8;
            this.btnGestionarClientes.Text = "👥 Gestionar Clientes";
            this.btnGestionarClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionarClientes.UseVisualStyleBackColor = false;
            this.btnGestionarClientes.Click += new System.EventHandler(this.btnGestionarClientes_Click);
            // 
            // btnGestionarUsuarios
            // 
            this.btnGestionarUsuarios.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionarUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionarUsuarios.FlatAppearance.BorderSize = 0;
            this.btnGestionarUsuarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnGestionarUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarUsuarios.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnGestionarUsuarios.Location = new System.Drawing.Point(11, 350);
            this.btnGestionarUsuarios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGestionarUsuarios.Name = "btnGestionarUsuarios";
            this.btnGestionarUsuarios.Size = new System.Drawing.Size(258, 50);
            this.btnGestionarUsuarios.TabIndex = 7;
            this.btnGestionarUsuarios.Text = "👤 Gestionar Usuarios";
            this.btnGestionarUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionarUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionarUsuarios.Click += new System.EventHandler(this.btnGestionarUsuarios_Click);
            // 
            // btnAnularOrden
            // 
            this.btnAnularOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnAnularOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAnularOrden.FlatAppearance.BorderSize = 0;
            this.btnAnularOrden.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnAnularOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnularOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnularOrden.ForeColor = System.Drawing.Color.White;
            this.btnAnularOrden.Location = new System.Drawing.Point(11, 300);
            this.btnAnularOrden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAnularOrden.Name = "btnAnularOrden";
            this.btnAnularOrden.Size = new System.Drawing.Size(258, 50);
            this.btnAnularOrden.TabIndex = 6;
            this.btnAnularOrden.Text = "❌ Anular Orden";
            this.btnAnularOrden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnularOrden.UseVisualStyleBackColor = false;
            this.btnAnularOrden.Click += new System.EventHandler(this.btnAnularOrden_Click);
            // 
            // btnModificarOrden
            // 
            this.btnModificarOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnModificarOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnModificarOrden.FlatAppearance.BorderSize = 0;
            this.btnModificarOrden.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnModificarOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarOrden.ForeColor = System.Drawing.Color.White;
            this.btnModificarOrden.Location = new System.Drawing.Point(11, 250);
            this.btnModificarOrden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModificarOrden.Name = "btnModificarOrden";
            this.btnModificarOrden.Size = new System.Drawing.Size(258, 50);
            this.btnModificarOrden.TabIndex = 5;
            this.btnModificarOrden.Text = "✏️ Modificar Orden";
            this.btnModificarOrden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificarOrden.UseVisualStyleBackColor = false;
            this.btnModificarOrden.Click += new System.EventHandler(this.btnModificarOrden_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.BackColor = System.Drawing.Color.Transparent;
            this.btnReporte.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReporte.FlatAppearance.BorderSize = 0;
            this.btnReporte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.ForeColor = System.Drawing.Color.White;
            this.btnReporte.Location = new System.Drawing.Point(11, 200);
            this.btnReporte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(258, 50);
            this.btnReporte.TabIndex = 4;
            this.btnReporte.Text = "📊 Generar Reporte";
            this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btnCerrarOrden
            // 
            this.btnCerrarOrden.BackColor = System.Drawing.Color.Transparent;
            this.btnCerrarOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCerrarOrden.FlatAppearance.BorderSize = 0;
            this.btnCerrarOrden.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnCerrarOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarOrden.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarOrden.ForeColor = System.Drawing.Color.White;
            this.btnCerrarOrden.Location = new System.Drawing.Point(11, 150);
            this.btnCerrarOrden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrarOrden.Name = "btnCerrarOrden";
            this.btnCerrarOrden.Size = new System.Drawing.Size(258, 50);
            this.btnCerrarOrden.TabIndex = 3;
            this.btnCerrarOrden.Text = "🔒 Cerrar Orden";
            this.btnCerrarOrden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarOrden.UseVisualStyleBackColor = false;
            this.btnCerrarOrden.Click += new System.EventHandler(this.btnCerrarOrden_Click);
            // 
            // btnHistorialEliminaciones
            // 
            this.btnHistorialEliminaciones.BackColor = System.Drawing.Color.Transparent;
            this.btnHistorialEliminaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistorialEliminaciones.FlatAppearance.BorderSize = 0;
            this.btnHistorialEliminaciones.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnHistorialEliminaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialEliminaciones.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialEliminaciones.ForeColor = System.Drawing.Color.White;
            this.btnHistorialEliminaciones.Location = new System.Drawing.Point(11, 100);
            this.btnHistorialEliminaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHistorialEliminaciones.Name = "btnHistorialEliminaciones";
            this.btnHistorialEliminaciones.Size = new System.Drawing.Size(258, 50);
            this.btnHistorialEliminaciones.TabIndex = 2;
            this.btnHistorialEliminaciones.Text = "🗑️ Historial Eliminaciones";
            this.btnHistorialEliminaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorialEliminaciones.UseVisualStyleBackColor = false;
            this.btnHistorialEliminaciones.Click += new System.EventHandler(this.btnHistorialEliminaciones_Click);
            // 
            // btnVerGastos
            // 
            this.btnVerGastos.BackColor = System.Drawing.Color.Transparent;
            this.btnVerGastos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVerGastos.FlatAppearance.BorderSize = 0;
            this.btnVerGastos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(75)))));
            this.btnVerGastos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerGastos.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerGastos.ForeColor = System.Drawing.Color.White;
            this.btnVerGastos.Location = new System.Drawing.Point(11, 50);
            this.btnVerGastos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVerGastos.Name = "btnVerGastos";
            this.btnVerGastos.Size = new System.Drawing.Size(258, 50);
            this.btnVerGastos.TabIndex = 1;
            this.btnVerGastos.Text = "💰 Ver Gastos";
            this.btnVerGastos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerGastos.UseVisualStyleBackColor = false;
            this.btnVerGastos.Click += new System.EventHandler(this.btnVerGastos_Click);
            // 
            // btnCrearOrden
            // 
            this.btnCrearOrden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCrearOrden.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCrearOrden.FlatAppearance.BorderSize = 0;
            this.btnCrearOrden.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCrearOrden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearOrden.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearOrden.ForeColor = System.Drawing.Color.White;
            this.btnCrearOrden.Location = new System.Drawing.Point(11, 0);
            this.btnCrearOrden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrearOrden.Name = "btnCrearOrden";
            this.btnCrearOrden.Size = new System.Drawing.Size(258, 50);
            this.btnCrearOrden.TabIndex = 0;
            this.btnCrearOrden.Text = "➕ Crear Orden";
            this.btnCrearOrden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCrearOrden.UseVisualStyleBackColor = false;
            this.btnCrearOrden.Click += new System.EventHandler(this.btnCrearOrden_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelLogo.Controls.Add(this.labelLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(280, 121);
            this.panelLogo.TabIndex = 0;
            // 
            // labelLogo
            // 
            this.labelLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelLogo.ForeColor = System.Drawing.Color.White;
            this.labelLogo.Location = new System.Drawing.Point(0, 0);
            this.labelLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(280, 121);
            this.labelLogo.TabIndex = 0;
            this.labelLogo.Text = "INSELEC S.A.";
            this.labelLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLogo.Click += new System.EventHandler(this.labelLogo_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.panelUserInfo);
            this.panelHeader.Controls.Add(this.panelSearch);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(280, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1020, 100);
            this.panelHeader.TabIndex = 1;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // panelUserInfo
            // 
            this.panelUserInfo.Controls.Add(this.labelWelcome);
            this.panelUserInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelUserInfo.Location = new System.Drawing.Point(720, 0);
            this.panelUserInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelUserInfo.Name = "panelUserInfo";
            this.panelUserInfo.Size = new System.Drawing.Size(300, 100);
            this.panelUserInfo.TabIndex = 3;
            // 
            // labelWelcome
            // 
            this.labelWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelWelcome.Location = new System.Drawing.Point(0, 0);
            this.labelWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(300, 100);
            this.labelWelcome.TabIndex = 1;
            this.labelWelcome.Text = "Bienvenido: [Usuario]";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWelcome.Click += new System.EventHandler(this.labelWelcome_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.btnBuscar);
            this.panelSearch.Controls.Add(this.txtBuscarCliente);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSearch.Location = new System.Drawing.Point(340, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(381, 100);
            this.panelSearch.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(272, 33);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 34);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBuscarCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscarCliente.Location = new System.Drawing.Point(7, 34);
            this.txtBuscarCliente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(256, 30);
            this.txtBuscarCliente.TabIndex = 4;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(340, 100);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Gestión de Órdenes";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContent.Controls.Add(this.dgvOrdenes);
            this.panelContent.Controls.Add(this.panelStats);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(280, 100);
            this.panelContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1020, 700);
            this.panelContent.TabIndex = 2;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenes.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrdenes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrdenes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrdenes.ColumnHeadersHeight = 45;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrdenes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenes.EnableHeadersVisualStyles = false;
            this.dgvOrdenes.Location = new System.Drawing.Point(0, 140);
            this.dgvOrdenes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrdenes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOrdenes.RowHeadersWidth = 51;
            this.dgvOrdenes.RowTemplate.Height = 35;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(1020, 560);
            this.dgvOrdenes.TabIndex = 2;
            this.dgvOrdenes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenes_CellContentClick);
            this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenes_CellDoubleClick);
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.Transparent;
            this.panelStats.Controls.Add(this.panelCerradas);
            this.panelStats.Controls.Add(this.panelAbiertas);
            this.panelStats.Controls.Add(this.panelTotal);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 0);
            this.panelStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.panelStats.Size = new System.Drawing.Size(1020, 140);
            this.panelStats.TabIndex = 1;
            // 
            // panelCerradas
            // 
            this.panelCerradas.BackColor = System.Drawing.Color.White;
            this.panelCerradas.Controls.Add(this.labelTotalCerradas);
            this.panelCerradas.Controls.Add(this.labelCerradas);
            this.panelCerradas.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCerradas.Location = new System.Drawing.Point(680, 20);
            this.panelCerradas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCerradas.Name = "panelCerradas";
            this.panelCerradas.Size = new System.Drawing.Size(320, 120);
            this.panelCerradas.TabIndex = 2;
            // 
            // labelTotalCerradas
            // 
            this.labelTotalCerradas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTotalCerradas.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalCerradas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.labelTotalCerradas.Location = new System.Drawing.Point(0, 39);
            this.labelTotalCerradas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTotalCerradas.Name = "labelTotalCerradas";
            this.labelTotalCerradas.Size = new System.Drawing.Size(320, 81);
            this.labelTotalCerradas.TabIndex = 6;
            this.labelTotalCerradas.Text = "0";
            this.labelTotalCerradas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCerradas
            // 
            this.labelCerradas.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCerradas.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCerradas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.labelCerradas.Location = new System.Drawing.Point(0, 0);
            this.labelCerradas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCerradas.Name = "labelCerradas";
            this.labelCerradas.Size = new System.Drawing.Size(320, 39);
            this.labelCerradas.TabIndex = 5;
            this.labelCerradas.Text = "ÓRDENES CERRADAS";
            this.labelCerradas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCerradas.Click += new System.EventHandler(this.labelCerradas_Click);
            // 
            // panelAbiertas
            // 
            this.panelAbiertas.BackColor = System.Drawing.Color.White;
            this.panelAbiertas.Controls.Add(this.labelTotalAbiertas);
            this.panelAbiertas.Controls.Add(this.labelAbiertas);
            this.panelAbiertas.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAbiertas.Location = new System.Drawing.Point(340, 20);
            this.panelAbiertas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelAbiertas.Name = "panelAbiertas";
            this.panelAbiertas.Size = new System.Drawing.Size(340, 120);
            this.panelAbiertas.TabIndex = 1;
            // 
            // labelTotalAbiertas
            // 
            this.labelTotalAbiertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTotalAbiertas.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalAbiertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.labelTotalAbiertas.Location = new System.Drawing.Point(0, 39);
            this.labelTotalAbiertas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTotalAbiertas.Name = "labelTotalAbiertas";
            this.labelTotalAbiertas.Size = new System.Drawing.Size(340, 81);
            this.labelTotalAbiertas.TabIndex = 4;
            this.labelTotalAbiertas.Text = "0";
            this.labelTotalAbiertas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAbiertas
            // 
            this.labelAbiertas.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAbiertas.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbiertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.labelAbiertas.Location = new System.Drawing.Point(0, 0);
            this.labelAbiertas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAbiertas.Name = "labelAbiertas";
            this.labelAbiertas.Size = new System.Drawing.Size(340, 39);
            this.labelAbiertas.TabIndex = 3;
            this.labelAbiertas.Text = "ÓRDENES ABIERTAS";
            this.labelAbiertas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTotal
            // 
            this.panelTotal.BackColor = System.Drawing.Color.White;
            this.panelTotal.Controls.Add(this.labelTotalOrdenes);
            this.panelTotal.Controls.Add(this.labelOrdenesCount);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTotal.Location = new System.Drawing.Point(20, 20);
            this.panelTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(320, 120);
            this.panelTotal.TabIndex = 0;
            // 
            // labelTotalOrdenes
            // 
            this.labelTotalOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTotalOrdenes.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalOrdenes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.labelTotalOrdenes.Location = new System.Drawing.Point(0, 39);
            this.labelTotalOrdenes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTotalOrdenes.Name = "labelTotalOrdenes";
            this.labelTotalOrdenes.Size = new System.Drawing.Size(320, 81);
            this.labelTotalOrdenes.TabIndex = 2;
            this.labelTotalOrdenes.Text = "0";
            this.labelTotalOrdenes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOrdenesCount
            // 
            this.labelOrdenesCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelOrdenesCount.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrdenesCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.labelOrdenesCount.Location = new System.Drawing.Point(0, 0);
            this.labelOrdenesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOrdenesCount.Name = "labelOrdenesCount";
            this.labelOrdenesCount.Size = new System.Drawing.Size(320, 39);
            this.labelOrdenesCount.TabIndex = 1;
            this.labelOrdenesCount.Text = "TOTAL DE ÓRDENES";
            this.labelOrdenesCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 800);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(999, 598);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INSELEC S.A. - Sistema de Gestión";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminForm_FormClosed);
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelUserInfo.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelCerradas.ResumeLayout(false);
            this.panelAbiertas.ResumeLayout(false);
            this.panelTotal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnGestionarProveedores;
        private System.Windows.Forms.Button btnGestionarClientes;
        private System.Windows.Forms.Button btnGestionarUsuarios;
        private System.Windows.Forms.Button btnAnularOrden;
        private System.Windows.Forms.Button btnModificarOrden;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnCerrarOrden;
        private System.Windows.Forms.Button btnHistorialEliminaciones;
        private System.Windows.Forms.Button btnVerGastos;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label labelLogo;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelUserInfo;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Panel panelCerradas;
        private System.Windows.Forms.Label labelTotalCerradas;
        private System.Windows.Forms.Label labelCerradas;
        private System.Windows.Forms.Panel panelAbiertas;
        private System.Windows.Forms.Label labelTotalAbiertas;
        private System.Windows.Forms.Label labelAbiertas;
        private System.Windows.Forms.Panel panelTotal;
        private System.Windows.Forms.Label labelTotalOrdenes;
        private System.Windows.Forms.Label labelOrdenesCount;
    }
}