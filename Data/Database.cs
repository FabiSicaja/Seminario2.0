using System;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.IO;

namespace Proyecto.Data
{
    public static class DatabaseMySQL
    {
        private static readonly string Server = "192.168.1.100"; // IP de tu servidor
        private static readonly string DatabaseName = "inselectdb";
        private static readonly string User = "app_user";
        private static readonly string Password = "admin123";

        // 🔌 ÚNICO ConnectionString para MySQL
        private static readonly string ConnectionString =
            $"Server={Server};Database={DatabaseName};User Id={User};Password={Password};SslMode=none;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static void TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    System.Windows.Forms.MessageBox.Show("Conexión exitosa a MySQL 🎉");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }
    }
}
