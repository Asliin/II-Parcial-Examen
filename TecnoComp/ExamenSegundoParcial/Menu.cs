namespace ExamenSegundoParcial
{
    public partial class Menu : Syncfusion.Windows.Forms.Office2010Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ManteniminetoToolStrip_Click(object sender, System.EventArgs e)
        {
            FacturaForm mantenimientoForm = new FacturaForm();
            mantenimientoForm.MdiParent = this;
            mantenimientoForm.Show();
        }

        private void SoporteToolStrip_Click(object sender, System.EventArgs e)
        {
            SoporteForm soporteForm = new SoporteForm();
            soporteForm.MdiParent = this;
            soporteForm.Show();
        }

        private void UsuariosToolStrip_Click(object sender, System.EventArgs e)
        {
            UsuariosForm usuariosForm = new UsuariosForm();
            usuariosForm.MdiParent = this;
            usuariosForm.Show();
        }

        private void ClientesToolStrip_Click(object sender, System.EventArgs e)
        {
            ClientesForm clientesForm = new ClientesForm();
            clientesForm.MdiParent = this;
            clientesForm.Show();
        }
    }
}
