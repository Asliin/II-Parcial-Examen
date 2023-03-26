using Datos;
using System;
using System.Data;

namespace ExamenSegundoParcial
{
    public partial class UsuariosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
        }

        UsuarioDB usuarioDB = new UsuarioDB();

        private void SalirButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            TraerUsuarios();
        }
        private void TraerUsuarios()
        {
            DataTable dt = new DataTable();
            dt = usuarioDB.DevolverUsuarios();
            UsuariosDataGridView.DataSource = dt;
        }
    }
}
