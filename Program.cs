using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyecto.Data
{
    public static class Database
    {
        private static readonly string Server = "192.168.1.100"; // IP de tu servidor
        private static readonly string DatabaseName = "inselectdb";
        private static readonly string User = "root";
        private static readonly string Password = "admin";

        // 🔌 ÚNICO ConnectionString para MySQL
        private static readonly string ConnectionString =
            $"Server={Server};Database={DatabaseName};User Id={User};Password={Password};SslMode=Disabled;";

        public static MySqlConnection GetConnection()
        {
            string connectionString = "server=localhost;user=root;password=admin;database=inselectdb;SslMode=Disabled;AllowPublicKeyRetrieval=True;";
            return new MySqlConnection(connectionString);
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
