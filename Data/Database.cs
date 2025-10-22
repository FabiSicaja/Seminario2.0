using System;
using System.Data.SQLite;
using System.IO;

namespace Proyecto.Data
{
    public static class Database
    {
        // 🔒 Ruta fija a tu DB existente
        private static readonly string DbFile = @"C:\Proyectos\Seminario2\bin\Debug\Proyecto.db";
        private static readonly string ConnectionString = $"Data Source={DbFile};Version=3;";

        public static SQLiteConnection GetConnection() => new SQLiteConnection(ConnectionString);

        public static void InitializeDatabase()
        {
            try
            {
                // Asegura que exista la carpeta y el archivo
                var folder = Path.GetDirectoryName(DbFile);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                if (!File.Exists(DbFile)) SQLiteConnection.CreateFile(DbFile);

                using (var conn = GetConnection())
                {
                    conn.Open();

                    // Activar FKs para esta conexión
                    using (var fkCmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", conn))
                        fkCmd.ExecuteNonQuery();

                    // ---------- TABLAS ----------
                    var createTechnicians = @"
                        CREATE TABLE IF NOT EXISTS Technicians (
                            id_technician INTEGER PRIMARY KEY AUTOINCREMENT,
                            nombre        TEXT NOT NULL,
                            telefono      TEXT
                        );";

                    var createUsuarios = @"
                        CREATE TABLE IF NOT EXISTS Usuarios (
                            id_usuario INTEGER PRIMARY KEY AUTOINCREMENT,
                            username   TEXT NOT NULL UNIQUE,
                            password   TEXT NOT NULL,
                            tipo       TEXT NOT NULL CHECK (tipo IN ('Admin','Technician'))
                        );";

                    var createClientes = @"
                        CREATE TABLE IF NOT EXISTS Clientes (
                            id_cliente INTEGER PRIMARY KEY AUTOINCREMENT,
                            nombre     TEXT NOT NULL,
                            telefono   TEXT,
                            email      TEXT,
                            direccion  TEXT,
                            nit        TEXT,
                            contactos  TEXT
                        );";

                    var createProveedores = @"
                        CREATE TABLE IF NOT EXISTS Proveedores (
                            id_proveedor        INTEGER PRIMARY KEY AUTOINCREMENT,
                            nombre              TEXT NOT NULL,
                            nit                 TEXT,
                            telefono            TEXT,
                            email               TEXT,
                            direccion           TEXT,
                            productos_servicios TEXT
                        );";

                    var createOrdenes = @"
                        CREATE TABLE IF NOT EXISTS Ordenes (
                            id_orden      INTEGER PRIMARY KEY AUTOINCREMENT,
                            descripcion   TEXT NOT NULL,
                            fecha_inicio  DATE NOT NULL,
                            fecha_fin     DATE,
                            estado        TEXT NOT NULL CHECK (estado IN ('Abierta','En Proceso','Cerrada','Anulada')) DEFAULT 'Abierta',
                            id_cliente    INTEGER NULL,
                            id_technician INTEGER NULL
                            -- id_technician se deja opcional solo por compatibilidad con formularios viejos
                        );";

                    var createOrdenTechnicians = @"
                        CREATE TABLE IF NOT EXISTS OrdenTechnicians (
                            id_orden      INTEGER NOT NULL,
                            id_technician INTEGER NOT NULL,
                            PRIMARY KEY (id_orden, id_technician),
                            FOREIGN KEY (id_orden)      REFERENCES Ordenes(id_orden)      ON DELETE CASCADE,
                            FOREIGN KEY (id_technician) REFERENCES Technicians(id_technician)
                        );";

                    var createGastos = @"
                        CREATE TABLE IF NOT EXISTS Gastos (
                            id_gasto         INTEGER PRIMARY KEY AUTOINCREMENT,
                            id_orden         INTEGER NOT NULL,
                            monto            REAL NOT NULL,
                            fecha            DATE NOT NULL,
                            tipo_gasto       TEXT,
                            serie            TEXT,
                            no_factura       TEXT,
                            nit              TEXT,
                            proveedor        TEXT,
                            tipo_combustible TEXT,
                            galonaje         REAL,
                            descripcion      TEXT,
                            FOREIGN KEY (id_orden) REFERENCES Ordenes(id_orden) ON DELETE CASCADE
                        );";

                    // Ejecutar creaciones
                    foreach (var sql in new[]
                    {
                        createTechnicians, createUsuarios, createClientes, createProveedores,
                        createOrdenes, createOrdenTechnicians, createGastos
                    })
                    {
                        using (var cmd = new SQLiteCommand(sql, conn))
                            cmd.ExecuteNonQuery();
                    }

                    InsertSampleData(conn);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "No se pudo inicializar la base de datos.\n\nDetalle: " + ex.Message,
                    "Error de inicialización",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                throw;
            }
        }

        private static void InsertSampleData(SQLiteConnection conn)
        {
            // Si ya hay usuarios, asumimos que ya hay datos sembrados
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Usuarios;", conn))
            {
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0) return;
            }

            // Technicians de ejemplo
            var insertTech = @"
                INSERT INTO Technicians (nombre, telefono) VALUES
                ('Juan Pérez',  '1234-5678'),
                ('María García','8765-4321'),
                ('Carlos López','5555-1234');";

            // Usuarios de ejemplo (sin id_technician)
            var insertUsers = @"
                INSERT INTO Usuarios (username, password, tipo) VALUES
                ('admin','123','Admin'),
                ('juan','123','Technician'),
                ('maria','123','Technician'),
                ('carlos','123','Technician');";

            // Ordenes de ejemplo
            var insertOrders = @"
                INSERT INTO Ordenes (descripcion, fecha_inicio, estado)
                VALUES
                ('Mantenimiento preventivo sistema HVAC', date('now','-30 day'), 'Abierta'),
                ('Reparación de equipo de refrigeración', date('now','-20 day'), 'En Proceso'),
                ('Instalación de nuevo sistema eléctrico', date('now','-60 day'), 'Cerrada');";

            // Relación muchos-a-muchos
            var insertOrdenTech = @"
                INSERT INTO OrdenTechnicians (id_orden, id_technician) VALUES
                (1,1),(1,2),(2,2),(3,3);";

            // Gastos de ejemplo
            var insertGastos = @"
                INSERT INTO Gastos (id_orden, descripcion, monto, fecha, tipo_gasto)
                VALUES
                (1, 'Refrigerante R410A', 150.75, date('now','-29 day'), 'Material'),
                (1, 'Filtros de aire',     45.50, date('now','-29 day'), 'Material'),
                (2, 'Compresor nuevo',    320.00, date('now','-19 day'), 'Repuesto');";

            foreach (var sql in new[] { insertTech, insertUsers, insertOrders, insertOrdenTech, insertGastos })
            {
                using (var cmd = new SQLiteCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }
        }
    }
}