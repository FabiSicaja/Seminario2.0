using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;

namespace Proyecto.Data
{
    public static class DatabaseMySQL
    {
        private static readonly string Server = "localhost"; // IP de tu servidor
        private static readonly string DatabaseName = "inselectdb";
        private static readonly string User = "root";
        private static readonly string Password = "Admin123";

        // 🔌 ÚNICO ConnectionString para MySQL
        private static readonly string ConnectionString =
            $"Server={Server};Database={DatabaseName};User Id={User};Password={Password};SslMode=Disabled;";

        public static MySqlConnection GetConnection()
        {
            string connectionString = "server=localhost;user=root;password=Admin123;database=inselectdb;SslMode=Disabled;AllowPublicKeyRetrieval=True;";
            return new MySqlConnection(connectionString);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
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
