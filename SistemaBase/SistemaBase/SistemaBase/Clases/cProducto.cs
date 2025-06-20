using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SistemaBase.Clases
{
    public class cProducto
    {
        public DataTable GetProductoxCodBarra(string CodigoBarra)
        {
            string sql = " select * from Producto ";
            sql = sql + " where CodigoBarra=" + "'" + CodigoBarra + "'";
            DataTable trdo = cDb.GetDatatable(sql);
            return trdo;
        }

        public DataTable GetProductoxCodigo(Int32 CodProducto)
        {
            string sql = " select * from Producto ";
            sql = sql + " where CodProducto=" + "'" + CodProducto + "'";
            DataTable trdo = cDb.GetDatatable(sql);
            return trdo;
        }

        public void ActualizarPoroducto(int CodProducto, Double Precio)
        {
            string sql = "update Producto ";
            sql = sql + " set Precio =" + Precio.ToString().Replace(",", ".");
            sql = sql + " where CodProducto =" + CodProducto.ToString();
            cDb.Grabar(sql);
        }

        public void ActualizarStock(int CodProducto ,int Cantidad)
        {
            string sql = "";
            sql = "update Producto ";
            sql = sql + " set Stock = isnull(stock,0) + " + Cantidad.ToString();
            sql = sql + " where CodProducto =" + CodProducto.ToString();
            cDb.Grabar(sql);
        }
    }
}
