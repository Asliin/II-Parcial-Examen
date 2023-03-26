using System;

namespace Entidades
{
    public class TicketFactura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdentidadCliente { get; set; }
        public string CodigoUsuario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }

        public TicketFactura()
        {
        }

        public TicketFactura(int id, DateTime fecha, string identidadCliente, string codigoUsuario, decimal subTotal, decimal impuesto, decimal descuento, decimal total)
        {
            Id = id;
            Fecha = fecha;
            IdentidadCliente = identidadCliente;
            CodigoUsuario = codigoUsuario;
            SubTotal = subTotal;
            Impuesto = impuesto;
            Descuento = descuento;
            Total = total;
        }
    }
}
