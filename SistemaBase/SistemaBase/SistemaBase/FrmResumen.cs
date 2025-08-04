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
            Buscar();
        }

        private void Buscar()
        {
            cFunciones fun = new cFunciones();
            DateTime FechaDesde = daFechaDesde.Value;
            DateTime FechaHasta = daFechaDesde.Value;
            cVenta venta = new Clases.cVenta();
            Int32? CodUsuario = null;
            if (cmbUsuario.SelectedIndex > 0)
            {
                CodUsuario = Convert.ToInt32(cmbUsuario.SelectedValue);
            }

            DataTable trdo = venta.GetVentaResumida(FechaDesde, FechaHasta, CodUsuario);
            Grilla.DataSource = trdo;
        }
    }
}
