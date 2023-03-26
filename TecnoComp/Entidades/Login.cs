namespace Entidades
{
    public class Login
    {
        public string CodigoUsuario { get; set; }
        public string Password { get; set; }

        public Login()
        {
        }

        public Login(string codigoUsuario, string password)
        {
            CodigoUsuario = codigoUsuario;
            Password = password;
        }
    }
}
