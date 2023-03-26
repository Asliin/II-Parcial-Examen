using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace ExamenSegundoParcial
{
    public partial class BuscarSoporteForm : Form
    {
        public BuscarSoporteForm()
        {
            InitializeComponent();
        }

        public Soporte soporte = new Soporte();
        SoporteDB soporteDB = new SoporteDB();

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (SoportesDataGridView.RowCount > 0)
            {
                if (SoportesDataGridView.SelectedRows.Count > 0)
                {
                    soporte.Codigo = SoportesDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                    soporte.Descripcion = SoportesDataGridView.CurrentRow.Cells["Descripcion"].Value.ToString();
                    soporte.Precio = Convert.ToDecimal(SoportesDataGridView.CurrentRow.Cells["Precio"].Value);
                    soporte.EstaActivo = Convert.ToBoolean(SoportesDataGridView.CurrentRow.Cells["EstaActivo"].Value);
                    Close();
                }
            }
        }

        private void BuscarSoporteForm_Load(object sender, EventArgs e)
        {
            SoportesDataGridView.DataSource = soporteDB.DevolverTipoSoporte();
        }

        private void DescripcionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SoportesDataGridView.DataSource = null;
            SoportesDataGridView.DataSource = soporteDB.DevolverPorDescripcion(DescripcionTextBox.Text);
        }
    }
}
