using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExamenSegundoParcial
{
    public partial class FacturaForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }

        Cliente micliente = null;
        ClienteDB clienteDB = new ClienteDB();
        Soporte miSoporte = null;
        SoporteDB soporteDB = new SoporteDB();
        List<DetalleFactura> listaDetalles = new List<DetalleFactura>();
        FacturaDB facturaDB = new FacturaDB();

        decimal subTotal = 0;
        decimal impuesto = 0;
        decimal totalAPagar = 0;
        decimal descuento = 0;

        private void LimpiarControles()
        {
            micliente = null;
            miSoporte = null;
            listaDetalles = null;
            FechaDateTimePicker.Value = DateTime.Now;
            IdentidadTextBox.Clear();
            NombreClienteTextBox.Clear();
            CodigoSoporteTextBox.Clear();
            DescripcionSoporteTextBox.Clear();
            PrecioTextBox.Clear();
            CantidadTextBox.Clear();
            DetalleDataGridView.DataSource = null;
            subTotal = 0;
            impuesto = 0;
            descuento = 0;
            totalAPagar = 0;
            SubTotalTextBox.Clear();
            ImpuestoTextBox.Clear();
            DescuentoTextBox.Clear();
            TotalTexBox.Clear();

        }

        private void IdentidadTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(IdentidadTextBox.Text))
            {
                micliente = new Cliente();
                micliente = clienteDB.DevolverClientePorIdentidad(IdentidadTextBox.Text);
                NombreClienteTextBox.Text = micliente.Nombre;
            }
            else
            {
                micliente = null;
                NombreClienteTextBox.Clear();
            }
        }

        private void FacturaForm_Load_1(object sender, EventArgs e)
        {
            UsuarioTextBox.Text = System.Threading.Thread.CurrentPrincipal.Identity.Name;
        }

        private void GuardarButton_Click_1(object sender, EventArgs e)
        {
            TicketFactura miFactura = new TicketFactura();
            miFactura.Fecha = FechaDateTimePicker.Value;
            miFactura.IdentidadCliente = micliente.Identidad;
            miFactura.CodigoUsuario = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            miFactura.SubTotal = subTotal;
            miFactura.Impuesto = impuesto;
            miFactura.Descuento = descuento;
            miFactura.Total = totalAPagar;


            bool inserto = facturaDB.Guardar(miFactura, listaDetalles);
            if (inserto)
            {
                MessageBox.Show("Ticket registrado exitosamente.");
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("No se pudo registrar el ticket.");
            }
        }

        private void BuscarClienteButton_Click_1(object sender, EventArgs e)
        {
            BuscarClienteForm buscarClienteForm = new BuscarClienteForm();
            buscarClienteForm.ShowDialog();
            micliente = buscarClienteForm.cliente;
            IdentidadTextBox.Text = micliente.Identidad;
            NombreClienteTextBox.Text = micliente.Nombre;
        }

        private void BuscarSoporteButton_Click(object sender, EventArgs e)
        {
            BuscarSoporteForm form = new BuscarSoporteForm();
            form.ShowDialog();
            miSoporte = new Soporte();
            miSoporte = form.soporte;
            CodigoSoporteTextBox.Text = miSoporte.Codigo;
            DescripcionSoporteTextBox.Text = miSoporte.Descripcion;
            PrecioTextBox.Text = miSoporte.Precio.ToString();
        }

        private void CodigoSoporteTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CodigoSoporteTextBox.Text))
            {
                miSoporte = new Soporte();
                miSoporte = soporteDB.DevolverSoportePorCodigo(CodigoSoporteTextBox.Text);
                DescripcionSoporteTextBox.Text = miSoporte.Descripcion;
                PrecioTextBox.Text = miSoporte.Precio.ToString();
            }
            else
            {
                miSoporte = null;
                DescripcionSoporteTextBox.Clear();
                PrecioTextBox.Clear();
            }
        }

        private void CantidadTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                DetalleFactura detalle = new DetalleFactura();
                detalle.CodigoMantenimiento = miSoporte.Codigo;
                detalle.Precio = miSoporte.Precio;
                detalle.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detalle.Total = miSoporte.Precio * Convert.ToDecimal(CantidadTextBox.Text);
                detalle.Descripcion = miSoporte.Descripcion;


                subTotal += detalle.Total;
                impuesto = subTotal * 0.15M;
                totalAPagar = subTotal + impuesto;

                listaDetalles.Add(detalle);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = listaDetalles;
                SubTotalTextBox.Text = subTotal.ToString("N2");
                ImpuestoTextBox.Text = impuesto.ToString("N2");
                TotalTexBox.Text = totalAPagar.ToString("N2");

                miSoporte = null;
                CodigoSoporteTextBox.Clear();
                DescripcionSoporteTextBox.Clear();
                PrecioTextBox.Clear();
                CantidadTextBox.Clear();
                CodigoSoporteTextBox.Focus();
            }

        }

        private void DescuentoTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled |= true;
            }
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(DescuentoTextBox.Text))
            {
                descuento = Convert.ToDecimal(DescuentoTextBox.Text);
                totalAPagar = totalAPagar - descuento;
                TotalTexBox.Text = totalAPagar.ToString();
            }
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string linea = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                int ydetalles = 250;
                Bitmap bitmap = Properties.Resources.logo;
                Image image = bitmap;
                e.Graphics.DrawImage(image, 280, 10);

                e.Graphics.DrawString("Cliente: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(10, 200));
                e.Graphics.DrawString(micliente.Nombre, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(80, 200));

                e.Graphics.DrawString("Fecha: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(550, 200));
                e.Graphics.DrawString(FechaDateTimePicker.Value.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(610, 200)); ;

                e.Graphics.DrawString(linea, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 230));

                e.Graphics.DrawString("Codigo", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(10, ydetalles));
                e.Graphics.DrawString("Tipo Soporte", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(127, ydetalles));
                e.Graphics.DrawString("Cantidad", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(420, ydetalles));
                e.Graphics.DrawString("Precio", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(550, ydetalles));
                e.Graphics.DrawString("Total", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(700, ydetalles));

                foreach (DetalleFactura item in listaDetalles)
                {
                    ydetalles = ydetalles + 25;
                    e.Graphics.DrawString(item.CodigoMantenimiento, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, ydetalles));
                    e.Graphics.DrawString(item.Descripcion, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(127, ydetalles));
                    e.Graphics.DrawString(item.Cantidad.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(420, ydetalles));
                    e.Graphics.DrawString(item.Precio.ToString("N2"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, ydetalles));
                    e.Graphics.DrawString(item.Total.ToString("N2"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(700, ydetalles));
                }
                e.Graphics.DrawString(linea, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, ydetalles + 20));

                e.Graphics.DrawString("Sub Total: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(600, ydetalles + 50));
                e.Graphics.DrawString(subTotal.ToString("N2"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(700, ydetalles + 50));
                e.Graphics.DrawString("ISV: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(653, ydetalles + 75));
                e.Graphics.DrawString(impuesto.ToString("N2"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(700, ydetalles + 75));
                e.Graphics.DrawString("Descuento: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(591, ydetalles + 100));
                e.Graphics.DrawString(descuento.ToString("N2"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(700, ydetalles + 100));
                e.Graphics.DrawString("Total: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(640, ydetalles + 125));
                e.Graphics.DrawString(totalAPagar.ToString("N2"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(700, ydetalles + 125));
            }
            catch (Exception)
            {
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }
    }
}