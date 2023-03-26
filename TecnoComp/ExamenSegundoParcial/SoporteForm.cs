using Datos;
using Entidades;
using System;
using System.Data;
using System.Windows.Forms;

namespace ExamenSegundoParcial
{
    public partial class SoporteForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public SoporteForm()
        {
            InitializeComponent();
        }

        SoporteDB soporteDB = new SoporteDB();
        Soporte miSoporte = new Soporte();

        private void SalirButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            LimpiarControles();
            CodigoTextBox.Focus();
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            EstaActivoCheckBox.Checked = false;
        }

        private void SoporteForm_Activated(object sender, System.EventArgs e)
        {
            CodigoTextBox.Focus();
        }

        private void TraerSoporte()
        {
            DataTable dt = new DataTable();
            dt = soporteDB.DevolverTipoSoporte();
            SoporteDataGridView.DataSource = dt;
        }

        private void SoporteForm_Load(object sender, System.EventArgs e)
        {
            TraerSoporte();
        }

        private void AgregarButton_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoTextBox.Text))
            {
                errorProvider1.SetError(CodigoTextBox, "Ingrese el código del soporte.");
                CodigoTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                errorProvider1.SetError(DescripcionTextBox, "Ingrese la descripción del soporte.");
                DescripcionTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(PrecioTextBox.Text))
            {
                errorProvider1.SetError(PrecioTextBox, "Ingrese el precio del servicio.");
                PrecioTextBox.Focus();
                return;
            }
            errorProvider1.Clear();


            //Insertamos en la Base de Datos


            miSoporte.Codigo = CodigoTextBox.Text;
            miSoporte.Descripcion = DescripcionTextBox.Text;
            miSoporte.Precio = Convert.ToDecimal(PrecioTextBox.Text);
            miSoporte.EstaActivo = EstaActivoCheckBox.Checked;

            bool inserto = soporteDB.Insertar(miSoporte);
            if (inserto)
            {
                MessageBox.Show("Registro guardado con éxito.");
                LimpiarControles();
                TraerSoporte();

            }
            else
            {
                MessageBox.Show("No se pudo guardar el registro.");
            }
        }

        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                errorProvider1.SetError(PrecioTextBox, "Ingrese valores numéricos.");
            }
            else
            {
                e.Handled = false;
                errorProvider1.Clear();
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled |= true;
            }
        }
    }
}
