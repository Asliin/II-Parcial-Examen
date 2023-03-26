namespace Entidades
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string CodigoMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public DetalleFactura()
        {
        }

        public DetalleFactura(int id, int idFactura, string codigoMantenimiento, string descripcion, decimal precio, int cantidad, decimal total)
        {
            Id = id;
            IdFactura = idFactura;
            CodigoMantenimiento = codigoMantenimiento;
            Descripcion = descripcion;
            Precio = precio;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
