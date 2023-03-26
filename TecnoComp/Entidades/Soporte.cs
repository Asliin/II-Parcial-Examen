namespace Entidades
{
    public class Soporte
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool EstaActivo { get; set; }

        public Soporte()
        {
        }

        public Soporte(string codigo, string descripcion, decimal precio, bool estaActivo)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Precio = precio;
            EstaActivo = estaActivo;
        }
    }
}
