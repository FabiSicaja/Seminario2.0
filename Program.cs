using Proyecto.Data;
using System;
using System.Windows.Forms;

namespace Proyecto
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1) Inicializar BD y asegurar tablas ANTES del login
            try
            {
                Database.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo inicializar la base de datos.\n\nDetalle: " + ex.Message,
                    "Error de inicialización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // 2) Abrir Login
            Application.Run(new LoginForm());
        }
    }
}
