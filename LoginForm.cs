using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Data;
using Proyecto;

namespace Proyecto
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Database.InitializeDatabase();
            //Database.CreateTables();

            txtUsuario.Focus();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtClave.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Campos requeridos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            btnIngresar.Text = "Verificando...";
            btnIngresar.Enabled = false;

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_usuario, username, tipo, id_technician FROM Usuarios WHERE username = @username AND password = @password";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session.UserId = reader.GetInt32(0);
                                Session.Username = reader.GetString(1);
                                Session.UserType = reader.GetString(2);
                                Session.TechnicianId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);

                                MessageBox.Show($"¡Bienvenido, {Session.Username}!", "Login exitoso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (Session.UserType == "Admin")
                                {
                                    AdminForm adminForm = new AdminForm();
                                    adminForm.Show();
                                }
                                else
                                {
                                    TechnicianForm technicianForm = new TechnicianForm();
                                    technicianForm.Show();
                                }

                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos", "Error de autenticación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtClave.Clear();
                                txtClave.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                btnIngresar.Text = "INGRESAR";
                btnIngresar.Enabled = true;
            }
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.LightYellow;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.White;
        }

        private void txtClave_Enter(object sender, EventArgs e)
        {
            txtClave.BackColor = Color.LightYellow;
        }

        private void txtClave_Leave(object sender, EventArgs e)
        {
            txtClave.BackColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}