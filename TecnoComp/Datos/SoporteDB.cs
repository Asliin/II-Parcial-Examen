using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace Datos
{
    public class SoporteDB
    {
        string cadena = "server=localhost; user=root; database=examensegundoparcial; password=Aslinphotographmyep1610@;";

        public DataTable DevolverTipoSoporte()
        {
            DataTable dataTable = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM mantenimiento ");
                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        MySqlDataReader dr = comando.ExecuteReader();
                        dataTable.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dataTable;
        }

        public bool Insertar(Soporte soporte)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO mantenimiento VALUES ");
                sql.Append(" (@Codigo, @Descripcion, @Precio, @EstaActivo);");

                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = soporte.Codigo;
                        comando.Parameters.Add("@Descripcion", MySqlDbType.VarChar, 200).Value = soporte.Descripcion;
                        comando.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = soporte.Precio;
                        comando.Parameters.Add("@EstaActivo", MySqlDbType.Bit).Value = soporte.EstaActivo;
                        comando.ExecuteNonQuery();
                        inserto = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return inserto;
        }

        public DataTable DevolverPorDescripcion(string descripcion)
        {
            DataTable dataTable = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM mantenimiento WHERE Descripcion LIKE '%" + descripcion + "%' ");
                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        MySqlDataReader dr = comando.ExecuteReader();
                        dataTable.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dataTable;
        }

        public Soporte DevolverSoportePorCodigo(string codigo)
        {
            Soporte soporte = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM mantenimiento WHERE Codigo = @Codigo ");
                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = codigo;
                        MySqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            soporte = new Soporte();

                            soporte.Codigo = codigo;
                            soporte.Descripcion = dr["Descripcion"].ToString();
                            soporte.Precio = Convert.ToDecimal(dr["Precio"]);
                            soporte.EstaActivo = Convert.ToBoolean(dr["EstaActivo"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return soporte;
        }
    }
}
