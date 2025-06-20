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
    public partial class FrmActualizarStock : FormBase 
    {
        public FrmActualizarStock()
        {
            InitializeComponent();
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
            string Codigo = txtCodigoBarra.Text;
            string Nombre = "";
            cProducto prod = new cProducto();
            DataTable trdo = prod.GetProductoxCodBarra(Codigo);
            if (trdo.Rows.Count>0)
            {
                if (trdo.Rows[0]["CodProducto"].ToString ()!="")
                {
                    txtCodProducto.Text = trdo.Rows[0]["CodProducto"].ToString();
                    Nombre = trdo.Rows[0]["Nombre"].ToString();
                    txtNombre.Text = Nombre;
                    txtPrecio.Text = trdo.Rows[0]["Precio"].ToString();
                    txtCodigo.Text = trdo.Rows[0]["Codigo"].ToString();
                    txtStock.Text = trdo.Rows[0]["stock"].ToString();
                }
            }
        }

        private void btnGrabarPrecio_Click(object sender, EventArgs e)
        {
            if (txtCodProducto.Text =="")
            {
                MessageBox.Show("Debe ingresar un producto ");
                return;
            }
            if (txtNuevoPrecio.Text =="")
            {
                MessageBox.Show("Debe ingresar un nuevo precio ");
                return;
            }
            cFunciones fun = new cFunciones();
            Int32 CodProducto = Convert.ToInt32(txtCodProducto.Text);
            Double Precio = 0; 
            Precio = Convert.ToDouble(txtNuevoPrecio.Text);
            cProducto prod = new cProducto();
            prod.ActualizarPoroducto(CodProducto, Precio);
            MessageBox.Show("Datos guardados correctamente ");
            txtPrecio.Text = fun.FormatoEnteroMiles(Precio.ToString());
        }

        private void btnGuardarStock_Click(object sender, EventArgs e)
        {
            if (txtCodProducto.Text == "")
            {
                MessageBox.Show("Debe ingresar un producto ");
                return;
            }  

            if (txtNuevoStock.Text == "")
            {
                MessageBox.Show("Debe ingresar el stock ");
                return;
            }
             
            cFunciones fun = new cFunciones();
            Int32 CodProducto = Convert.ToInt32(txtCodProducto.Text);
            int Stock = Convert.ToInt32(txtNuevoStock.Text);
            cProducto prod = new Clases.cProducto();
            prod.ActualizarStock(CodProducto, Stock);
            MessageBox.Show("Datos guardados correctamente ");
            BuscarProductoxCodigo(CodProducto);
        }

        private void BuscarProductoxCodigo(Int32 CodProducto)
        {
            cProducto prod = new Clases.cProducto();
            DataTable trdo = prod.GetProductoxCodigo(CodProducto);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodProducto"].ToString() != "")
                {
                    txtCodProducto.Text = trdo.Rows[0]["CodProducto"].ToString();
                    string  Nombre = trdo.Rows[0]["Nombre"].ToString();
                    txtNombre.Text = Nombre;
                    txtPrecio.Text = trdo.Rows[0]["Precio"].ToString();
                    txtCodigo.Text = trdo.Rows[0]["Codigo"].ToString();
                    txtStock.Text = trdo.Rows[0]["stock"].ToString();
                }
            }
        }
    }
}
