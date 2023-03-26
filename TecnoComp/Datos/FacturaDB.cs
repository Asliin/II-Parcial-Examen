using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class FacturaDB
    {
        string cadena = "server=localhost; user=root; database=examensegundoparcial; password=Aslinphotographmyep1610@;";

        public bool Guardar(TicketFactura factura, List<DetalleFactura> detalles)
        {
            bool inserto = false;
            int idFactura = 0;
            try
            {
                StringBuilder sqlFactura = new StringBuilder();
                sqlFactura.Append(" INSERT INTO facturatickets (Fecha, IdentidadCliente, CodigoUsuario, SubTotal, Impuesto, Descuento, Total) VALUES (@Fecha, @IdentidadCliente, @CodigoUsuario, @SubTotal, @Impuesto, @Descuento, @Total); ");
                sqlFactura.Append(" SELECT LAST_INSERT_ID(); ");

                StringBuilder sqlDetalle = new StringBuilder();
                sqlDetalle.Append(" INSERT INTO detallefactura (IdFactura, CodigoMantenimiento, Precio, Cantidad, Total) VALUES (@IdFactura, @CodigoMantenimiento, @Precio, @Cantidad, @Total); ");


                using (MySqlConnection con = new MySqlConnection(cadena))
                {
                    con.Open();

                    MySqlTransaction tran = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    try
                    {
                        using (MySqlCommand cmd1 = new MySqlCommand(sqlFactura.ToString(), con, tran))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;

                            cmd1.Parameters.Add("@Fecha", MySqlDbType.DateTime).Value = factura.Fecha;
                            cmd1.Parameters.Add("@IdentidadCliente", MySqlDbType.VarChar, 25).Value = factura.IdentidadCliente;
                            cmd1.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = factura.CodigoUsuario;
                            cmd1.Parameters.Add("@SubTotal", MySqlDbType.Decimal).Value = factura.SubTotal;
                            cmd1.Parameters.Add("@Impuesto", MySqlDbType.Decimal).Value = factura.Impuesto;
                            cmd1.Parameters.Add("@Descuento", MySqlDbType.Decimal).Value = factura.Descuento;
                            cmd1.Parameters.Add("@Total", MySqlDbType.Decimal).Value = factura.Total;
                            idFactura = Convert.ToInt32(cmd1.ExecuteScalar());
                        }

                        foreach (DetalleFactura detalle in detalles)
                        {
                            using (MySqlCommand cmd2 = new MySqlCommand(sqlDetalle.ToString(), con, tran))
                            {
                                cmd2.CommandType = System.Data.CommandType.Text;
                                cmd2.Parameters.Add("@IdFactura", MySqlDbType.Int32).Value = idFactura;
                                cmd2.Parameters.Add("@CodigoMantenimiento", MySqlDbType.VarChar, 80).Value = detalle.CodigoMantenimiento;
                                cmd2.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = detalle.Precio;
                                cmd2.Parameters.Add("@Cantidad", MySqlDbType.Int32).Value = detalle.Cantidad;
                                cmd2.Parameters.Add("@Total", MySqlDbType.Decimal).Value = detalle.Total;
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                        inserto = true;
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        inserto = false;
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return inserto;
        }
    }
}
