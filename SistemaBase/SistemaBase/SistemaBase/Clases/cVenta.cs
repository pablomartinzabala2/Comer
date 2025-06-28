using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace SistemaBase.Clases
{
    public class cVenta
    {
        public Int32 InsertarVenta(SqlConnection con, SqlTransaction Transaccion, double Total, DateTime Fecha,int CodUsuario)
        {
            string sql = "insert into Venta(";
            sql = sql + "Fecha,CodUsuario,Total)";
            sql = sql + " values (";
            sql = sql + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + CodUsuario.ToString();
            sql = sql + "," + Total.ToString().Replace(",", ".");
            sql = sql + ")";
            return cDb.EjecutarEscalarTransaccion(con, Transaccion, sql);
        }
    }
}
