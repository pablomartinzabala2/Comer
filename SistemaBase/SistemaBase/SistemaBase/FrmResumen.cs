using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBase.Clases;

namespace SistemaBase
{
    public partial class FrmResumen : FormBase
    {
        public FrmResumen()
        {
            InitializeComponent();
        }

        private void FrmResumen_Load(object sender, EventArgs e)
        {
            InicializarFecha();
            Buscar();
        }

        private void Buscar()
        {
            cFunciones fun = new cFunciones();
            DateTime FechaDesde = daFechaDesde.Value;
            DateTime FechaHasta = daFechaHasta.Value; 
            cVenta venta = new Clases.cVenta();
            Int32? CodUsuario = null;
            if (cmbUsuario.SelectedIndex > 0)
            {
                CodUsuario = Convert.ToInt32(cmbUsuario.SelectedValue);
            }

            DataTable trdo = venta.GetVentaResumida(FechaDesde, FechaHasta, CodUsuario);
            trdo = fun.TablaaMiles(trdo, "Total");
            Grilla.DataSource = trdo;
            fun.AnchoColumnas(Grilla, "75;25");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void InicializarFecha()
        {   
            DateTime Hoy = DateTime.Now;
            int Mes = Hoy.Month;
            int Year = Hoy.Year;
            string sFecha = "01/" + Mes.ToString () + "/" + Year.ToString();
            DateTime FechaDesde = Convert.ToDateTime(sFecha);
            daFechaDesde.Value = FechaDesde;
            DateTime FechaHasta = FechaDesde;
            FechaHasta = FechaHasta.AddMonths(1);
            daFechaHasta.Value = FechaHasta;
        }
    }
}
