using Datos;
using Entidades;
using System.Data;
using System.Windows.Forms;

namespace ExamenSegundoParcial
{
    public partial class ClientesForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }

        ClienteDB clienteDB = new ClienteDB();
        Cliente miCliente = new Cliente();

        private void SalirButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void ClientesForm_Load(object sender, System.EventArgs e)
        {
            TraerClientes();
        }

        private void TraerClientes()
        {
            DataTable dt = new DataTable();
            dt = clienteDB.DevolverClientes();
            ClientesDataGridView.DataSource = dt;

        }

        private void AgregarButton_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(IdentidadTextBox.Text))
            {
                errorProvider1.SetError(IdentidadTextBox, "Ingrese el número de identidad del cliente.");
                IdentidadTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(NombreTextBox.Text))
            {
                errorProvider1.SetError(NombreTextBox, "Ingrese el nombre del cliente.");
                NombreTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(TelefonoTextBox.Text))
            {
                errorProvider1.SetError(TelefonoTextBox, "Ingrese el número de télefono del cliente.");
                TelefonoTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(CorreoTextBox.Text))
            {
                errorProvider1.SetError(CorreoTextBox, "Ingrese el correo electrónico del cliente.");
                CorreoTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(DireccionTextBox.Text))
            {
                errorProvider1.SetError(DireccionTextBox, "Ingrese la dirección del cliente.");
                DireccionTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            //Insertamos en la Base de Datos


            miCliente.Identidad = IdentidadTextBox.Text;
            miCliente.Nombre = NombreTextBox.Text;
            miCliente.Telefono = TelefonoTextBox.Text;
            miCliente.Correo = CorreoTextBox.Text;
            miCliente.Direccion = DireccionTextBox.Text;
            miCliente.EstaActivo = EstaActivoCheckBox.Checked;

            bool inserto = clienteDB.Insertar(miCliente);
            if (inserto)
            {
                MessageBox.Show("Registro guardado con éxito.");
                LimpiarControles();
                TraerClientes();

            }
            else
            {
                MessageBox.Show("No se pudo guardar el registro.");
            }
        }

        private void LimpiarControles()
        {
            IdentidadTextBox.Clear();
            NombreTextBox.Clear();
            TelefonoTextBox.Clear();
            CorreoTextBox.Clear();
            DireccionTextBox.Clear();
            EstaActivoCheckBox.Checked = false;
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            LimpiarControles();
            IdentidadTextBox.Focus();
        }

        private void ClientesForm_Activated(object sender, System.EventArgs e)
        {
            IdentidadTextBox.Focus();
        }
    }
}
