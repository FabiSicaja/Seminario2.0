using System;
using System.Data.SQLite;
using System.IO;

namespace Proyecto.Data
{
    public static class Database
    {
        // Cambia el nombre para que coincida con lo que usan los formularios
        private static string _connectionString = "Data Source=Proyecto.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public static void InitializeDatabase()
        {
            if (!File.Exists("Proyecto.db"))
            {
                SQLiteConnection.CreateFile("Proyecto.db");
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                // Crear tabla de usuarios (actualizada para coincidir con tu sistema)
                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Usuarios (
                        id_usuario INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT UNIQUE NOT NULL,
                        password TEXT NOT NULL,
                        tipo TEXT CHECK(tipo IN ('Admin','Technician')) NOT NULL,
                        id_technician INTEGER NULL,
                        created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (id_technician) REFERENCES Technicians(id_technician)
                    )";

                // Crear tabla de gastos (versión simplificada para empezar)
                string createGastosTable = @"
                    CREATE TABLE IF NOT EXISTS Gastos (
                        id_gasto INTEGER PRIMARY KEY AUTOINCREMENT,
                        id_orden INTEGER NOT NULL,
                        descripcion TEXT NOT NULL,
                        monto REAL NOT NULL,
                        fecha DATE NOT NULL,
                        created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (id_orden) REFERENCES Ordenes(id_orden)
                    )";

                    // Tabla Technicians
                    string createTechniciansTable = @"
                    CREATE TABLE IF NOT EXISTS Technicians (
                        id_technician INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        telefono TEXT
                    );";

                        // Tabla Ordenes (CON id_technician)
                        string createOrdenesTable = @"
                    CREATE TABLE IF NOT EXISTS Ordenes (
                        id_orden INTEGER PRIMARY KEY AUTOINCREMENT,
                        descripcion TEXT NOT NULL,
                        fecha_inicio DATE NOT NULL,
                        fecha_fin DATE,
                        id_technician INTEGER,
                        estado TEXT CHECK(estado IN ('Abierta','En Proceso','Cerrada')) NOT NULL DEFAULT 'Abierta',
                        FOREIGN KEY (id_technician) REFERENCES Technicians(id_technician)
                    );";

                string createCLientes = @"
                    CREATE TABLE IF NOT EXISTS Clientes (
                        id_cliente INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        telefono TEXT,
                        email TEXT,
                        direccion TEXT,
                        fecha_registro DATE NOT NULL
                    );";

                string createProveedores = @"

                    CREATE TABLE IF NOT EXISTS Proveedores (
                        id_proveedor INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT NOT NULL,
                        contacto TEXT,
                        telefono TEXT,
                        email TEXT,
                        direccion TEXT,
                        productos_servicios TEXT,
                        fecha_registro DATE NOT NULL
                    );";


                using (var cmd = new SQLiteCommand(createUsersTable, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createTechniciansTable, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createOrdenesTable, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createGastosTable, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createCLientes, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createProveedores, conn))
                    cmd.ExecuteNonQuery();

                // Insertar datos de prueba
                InsertSampleData(conn);
            }
        }

        private static void InsertSampleData(SQLiteConnection conn)
        {
            // Verificar si ya existen datos
            string checkData = "SELECT COUNT(*) FROM Usuarios";
            using (var cmd = new SQLiteCommand(checkData, conn))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0) return;
            }

            // Insertar técnicos de ejemplo
            string insertTechnicians = @"
                INSERT INTO Technicians (nombre, telefono, email, especialidad) VALUES 
                ('Juan Pérez', '1234-5678', 'juan@empresa.com', 'HVAC'),
                ('María García', '8765-4321', 'maria@empresa.com', 'Electricidad'),
                ('Carlos López', '5555-1234', 'carlos@empresa.com', 'Refrigeración');";

            // Insertar usuarios de ejemplo (password: '123')
            string insertUsuarios = @"
                INSERT INTO Usuarios (username, password, tipo, id_technician) VALUES 
                ('admin', '123', 'Admin', NULL),
                ('juan', '123', 'Technician', 1),
                ('maria', '123', 'Technician', 2),
                ('carlos', '123', 'Technician', 3);";

            // Insertar órdenes de ejemplo
            string insertOrdenes = @"
                INSERT INTO Ordenes (descripcion, fecha_inicio, id_technician, estado) VALUES 
                ('Mantenimiento preventivo sistema HVAC', '2024-01-15', 1, 'Abierta'),
                ('Reparación de equipo de refrigeración', '2024-01-16', 2, 'En Proceso'),
                ('Instalación de nuevo sistema eléctrico', '2024-01-10', 3, 'Cerrada');";

            // Insertar gastos de ejemplo
            string insertGastos = @"
                INSERT INTO Gastos (id_orden, descripcion, monto, fecha) VALUES 
                (1, 'Refrigerante R410A', 150.75, '2024-01-15'),
                (1, 'Filtros de aire', 45.50, '2024-01-15'),
                (2, 'Compresor nuevo', 320.00, '2024-01-16');";

            using (var cmd = new SQLiteCommand(insertTechnicians, conn))
                cmd.ExecuteNonQuery();

            using (var cmd = new SQLiteCommand(insertUsuarios, conn))
                cmd.ExecuteNonQuery();

            using (var cmd = new SQLiteCommand(insertOrdenes, conn))
                cmd.ExecuteNonQuery();

            using (var cmd = new SQLiteCommand(insertGastos, conn))
                cmd.ExecuteNonQuery();
        }
    }
}