using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace ExamenSegundoParcial
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
            CodigoUsuarioTextBox.Focus();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (CodigoUsuarioTextBox.Text == string.Empty)
            {
                errorProvider1.SetError(CodigoUsuarioTextBox, "Ingrese un usuario.");
                return;
            }
            errorProvider1.Clear();
            if (PasswordTextBox.Text == "")
            {
                errorProvider1.SetError(PasswordTextBox, "Ingrese la contraseña.");
                return;
            }
            errorProvider1.Clear();

            //Validar en la base de datos
            Login login = new Login(CodigoUsuarioTextBox.Text, PasswordTextBox.Text);

            UsuarioDB usuarioDB = new UsuarioDB();
            Usuario usuario = new Usuario();

            usuario = usuarioDB.Autenticar(login);

            if (usuario != null)
            {
                if (usuario.EstaActivo)
                {
                    //Crea la sesión
                    System.Security.Principal.GenericIdentity identidad = new System.Security.Principal.GenericIdentity(usuario.CodigoUsuario);
                    System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(identidad, new string[] { usuario.Rol });
                    System.Threading.Thread.CurrentPrincipal = principal;

                    //Mandarlo al menú
                    Menu menuFormulario = new Menu();
                    this.Hide();
                    menuFormulario.Show();
                }
                else
                {
                    MessageBox.Show("Error", "El usuario está inactivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("La dirección de correo electrónico o la contraseña que ha introducido no son correctas.", "Acceso inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CodigoUsuarioTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;
                CodigoUsuarioTextBox.Focus();
            }
        }

        private void MostrarButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.PasswordChar == '*')
            {
                PasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextBox.PasswordChar = '*';
            }
        }
    }
}
