﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using SistemaBase.Clases;

namespace SistemaBase
{
    public partial class FrmListadoVentascs : FormBase
    {
        public FrmListadoVentascs()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            Buscar();
        }

        private void Buscar()
        {
            Double Total = 0;
            cFunciones fun = new cFunciones();
            DateTime FechaDesde = daFechaDesde.Value;
            DateTime FechaHasta = daFechaDesde.Value;
            cVenta venta = new Clases.cVenta();
            Int32? CodUsuario = null;
            if (cmbUsuario.SelectedIndex>0)
            {
                CodUsuario = Convert.ToInt32(cmbUsuario.SelectedValue);
            }
            DataTable trdo = venta.GetVentasxFecha(FechaDesde, FechaHasta, CodUsuario);
            Total = fun.TotalizarColumna(trdo, "Total");
            trdo = fun.TablaaMiles(trdo, "Total");
            trdo = fun.TablaaFechas(trdo, "Fecha");
            Grilla.DataSource = trdo;
            fun.AnchoColumnas(Grilla, "20;20;40;20");
            Grilla.Columns[0].HeaderText = "Nro Venta ";
            txtTotal.Text = fun.SepararDecimales(Total.ToString());
            txtCantidad.Text = trdo.Rows.Count.ToString();
           
        }

        private void FrmListadoVentascs_Load(object sender, EventArgs e)
        {
            CargarUsuario();
            Buscar();

        }

        private void CargarUsuario()
        {
            cFunciones fun = new cFunciones();
            fun.LlenarCombo(cmbUsuario, "Usuario", "Nombre", "CodUsuario");
        }
    }
}
