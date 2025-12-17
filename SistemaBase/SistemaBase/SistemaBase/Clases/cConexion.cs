using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBase.Clases
{
    public static class cConexion
    {
        public static string GetConexion()
        {
            //SISTEMA PERFUMES DESKTOP-TRC4UMG
          //  string cadena = "Data Source=DESKTOP-TRC4UMG;Initial Catalog=SISTEMA;Integrated Security=True;TrustServerCertificate=True;";
          

            //nueva cadena de conexion   
              string cadena = "Data Source=DESKTOP-PICJCLR\\SQLEXPRESS;Initial Catalog=COMERCIO;Integrated Security=True";
            //DESKTOP-I0OF5F9\SQLEXPRESS
            //string cadena = "Data Source=DESKTOP-I0OF5F9\\SQLEXPRESS;Initial Catalog=JOY;Integrated Security=True";
            return cadena;
        }
    }
}
