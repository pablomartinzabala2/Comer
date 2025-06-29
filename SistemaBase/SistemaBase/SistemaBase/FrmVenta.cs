﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBase.Clases;
using System.Data.SqlClient;

namespace SistemaBase
{
    public partial class FrmVenta : FormBase 
    {
        cFunciones fun;
        Boolean PuedeAgregarCodigoBarra;
        Boolean PuedeAgregarCodigoBarra2;
        DataTable tbDetalle;
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            string Codigo = txtCodigoBarra.Text;
            string Nombre = "";
            cProducto prod = new cProducto();
            DataTable trdo = prod.GetProductoxCodBarra(Codigo);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodProducto"].ToString() != "")
                {
                    txtCodProducto.Text = trdo.Rows[0]["CodProducto"].ToString();
                    Nombre = trdo.Rows[0]["Nombre"].ToString();
                    txtNombre.Text = Nombre;
                    txtPrecio.Text = trdo.Rows[0]["Precio"].ToString();
                    //txtCodigo.Text = trdo.Rows[0]["Codigo"].ToString();
                    txtStock.Text = trdo.Rows[0]["stock"].ToString();
                    if (txtPrecio.Text != "")
                    {
                        txtPrecio.Text = fun.SepararDecimales(txtPrecio.Text);
                        //  txtPrecio.Text = fun.FormatoEnteroMiles(txtPrecio.Text);
                    }
                    txtCantidad.Text = "1";
                    txtCantidad.Focus();
                    PuedeAgregarCodigoBarra = true;
                }
            }
        }
        
        private void Agregar()
        {  
            if (txtCodProducto.Text == "")
            {
                Mensaje("Debe ingresar un producto para continuar");
                return;
            }
            if (txtPrecio.Text == "")
            {
                Mensaje("Debe ingresar un precio para continuar");
                return;
            }
            // string Col = "CodProducto;Nombre;Cantidad;Precio;SubTotal"; 
            
            Int32 CodProducto = Convert.ToInt32(txtCodProducto.Text);
            string Nombre = txtNombre.Text;
            string Precio = "0";
            string Cantidad = "1";
            Double SubTotal = 0;
            if (txtCantidad.Text != "")
            {
                Cantidad = txtCantidad.Text;
            }

            ModificarCantidad(CodProducto,Convert.ToInt32 (Cantidad));
            //se utiliza por si las dudas vuelve
            // a ingresar el mismo proucto

            if (txtCantidad.Text != "")
            {
                Cantidad = txtCantidad.Text;
            }

            if (txtPrecio.Text != "")
            {
                Precio = txtPrecio.Text;
            }
            SubTotal = Convert.ToDouble(Cantidad) * Convert.ToDouble(Precio);

            string val = CodProducto + ";"+ Nombre;
            val = val + ";" + Cantidad + ";" + Precio;
            val = val + ";" + fun.SepararDecimales (SubTotal.ToString ());
            tbDetalle = fun.AgregarFilas(tbDetalle, val);
            Grilla.DataSource = tbDetalle;
            fun.AnchoColumnas(Grilla, "0;40;20;20;20");
            Double Total = fun.TotalizarColumna(tbDetalle, "SubTotal");
            txtTotal.Text = fun.SepararDecimales (Total.ToString());
            Limpiar();
            txtCodigoBarra.Focus();
            PuedeAgregarCodigoBarra = false;
            PuedeAgregarCodigoBarra2 = false;
        }

        private void Limpiar()
        {
            txtCodigo.Text = "";
            txtCodProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtCodigoBarra.Text = "";
            txtNombre.Text ="";
            txtStock.Text = "";
            
        }

        private void ModificarCantidad(int CodProducto, int Canatidad)
        {
            
            int Codigo = 0;
            int stock = 0;
            int b = 0;
            
            for (int i = 0; i < tbDetalle.Rows.Count ; i++)
            {
                Codigo = Convert.ToInt32(tbDetalle.Rows[i]["CodProducto"].ToString());
                if (Codigo == CodProducto)
                {
                    b = 1;
                    stock =Convert.ToInt32 (tbDetalle.Rows[i]["Cantidad"]);
                    fun.EliminarFila(tbDetalle, "CodProducto", CodProducto.ToString());
                   
                }
            }

            if (b ==1)
            {
                stock = stock + Canatidad;
                txtCantidad.Text = stock.ToString();        
            }
            
        }

        private void Inicialiar()
        {         
            fun = new cFunciones();
            string Col = "CodProducto;Nombre;Cantidad;Precio;SubTotal";
            tbDetalle = new DataTable();
            tbDetalle = fun.CrearTabla(Col);
            txtCodigoBarra.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {        
            Agregar();
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            Inicialiar();
            CargarNumeroVenta();
            fun.EstiloBotones(btnGrabar);
            fun.EstiloBotones(btnCancelar);
            fun.EstiloBotones(btnAnular);
            PuedeAgregarCodigoBarra = false;
            PuedeAgregarCodigoBarra2 = false;
            BuscarUsuario();
            
        }

        private void CargarNumeroVenta()
        {
            int CodVenta = 0;
            cVenta venta = new Clases.cVenta();
            CodVenta = venta.GetMaxNumeroVenta();
            CodVenta = CodVenta + 1;
            lBlNumeroVenta.Text = "Venta Número " + CodVenta.ToString();
        }

        private void BuscarUsuario()
        {
            int CodUsuario = Principal.CodUsuarioLogueado;
            cUsuario user = new cUsuario();
            string Nombre = user.GetNombreUsuarioxCodUsuario(CodUsuario);
            lblUsuario.Text = "Usuario " + Nombre;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un elelemnto");
                return;
            }
            string CodProducto = Grilla.CurrentRow.Cells[0].Value.ToString();
            tbDetalle = fun.EliminarFila(tbDetalle, "CodProducto", CodProducto);
            Grilla.DataSource = tbDetalle;
            Double Total = fun.TotalizarColumna(tbDetalle, "Precio");
            txtTotal.Text = Total.ToString();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (PuedeAgregarCodigoBarra == true)
                {
                    if (PuedeAgregarCodigoBarra2 ==true)
                    {
                        Agregar();
                        txtCodigoBarra.Focus();
                    }
                    else
                    {
                        PuedeAgregarCodigoBarra2 = true;
                    }
                    
                    
                }
               
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (tbDetalle.Rows.Count <1)
            {
                MessageBox.Show("Debe ingresar una venta para continuar");
                return;
            }
            int CodUsuario =  Principal.CodUsuarioLogueado;
            cFunciones fun = new cFunciones();
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.GetConexion());
            con.Open();
            Transaccion = con.BeginTransaction();
            
            Int32 CodVenta = 0;
            DateTime Fecha = daFecha.Value;
            cVenta venta = new Clases.cVenta();
            Double Total = 0;
            if(txtTotal.Text !="")
            {
                Total = fun.ToDouble (txtTotal.Text);
                
            }
            try
            {
                CodVenta = venta.InsertarVenta(con, Transaccion, Total, Fecha, CodUsuario);
                GrabarDetalle(CodVenta, con, Transaccion);
                Transaccion.Commit();
                con.Close();
                Mensaje("Datos grabados correctamente");
                tbDetalle.Clear();
                Grilla.DataSource = tbDetalle;
                Limpiar();
                CargarNumeroVenta();
                txtTotal.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error en el proceso ");
                MessageBox.Show(ex.Message.ToString());
                Transaccion.Rollback();
                con.Close();

            }

        }

        private void GrabarDetalle(Int32 CodVenta, SqlConnection con , SqlTransaction tran)
        {
            cProducto prod = new cProducto();
            cDetalleVentacs detalle = new cDetalleVentacs();
            Int32 CodProducto = 0;
            Int32 Cantidad = 0;
            Double Precio = 0;
            Double Subtotal = 0;
            for (int i = 0; i < tbDetalle.Rows.Count; i++)
            {
                CodProducto = Convert.ToInt32 (tbDetalle.Rows[i]["CodProducto"].ToString());
                Cantidad = Convert.ToInt32(tbDetalle.Rows[i]["Cantidad"].ToString());
                Precio = fun.ToDouble(tbDetalle.Rows[i]["Precio"].ToString());
                Subtotal = fun.ToDouble(tbDetalle.Rows[i]["Subtotal"].ToString());
                detalle.InsertarDetalle(con, tran, CodVenta, CodProducto, Cantidad, Precio, Subtotal);
                prod.ActualizarStockTransaccion(con, tran, CodProducto, Cantidad);
            }
        }

        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (PuedeAgregarCodigoBarra == true)
                {
                    if (PuedeAgregarCodigoBarra2 == true)
                    {
                        Agregar();
                    }
                    else
                    {
                        PuedeAgregarCodigoBarra2 = true;
                    }
                }

                txtCodigo.Focus();
            }
            */
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            string Codigo = txtCodigo.Text;
            string Nombre = "";
            cProducto prod = new cProducto();
            DataTable trdo = prod.GetProductoxCodIGOInterno(Codigo);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodProducto"].ToString() != "")
                {
                    txtCodProducto.Text = trdo.Rows[0]["CodProducto"].ToString();
                    Nombre = trdo.Rows[0]["Nombre"].ToString();
                    txtNombre.Text = Nombre;
                    txtPrecio.Text = trdo.Rows[0]["Precio"].ToString();
                    txtCodigo.Text = trdo.Rows[0]["Codigo"].ToString();
                    txtStock.Text = trdo.Rows[0]["stock"].ToString();
                    if (txtPrecio.Text != "")
                    {
                        txtPrecio.Text = fun.SepararDecimales(txtPrecio.Text);
                        //  txtPrecio.Text = fun.FormatoEnteroMiles(txtPrecio.Text);
                    }
                    txtCantidad.Text = "1";
                    txtCantidad.Focus();
                    PuedeAgregarCodigoBarra = true;
                    PuedeAgregarCodigoBarra2 = true;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            tbDetalle.Clear();
            Grilla.DataSource = tbDetalle;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Principal.CodProoducto = 0;
            FrmBuscarProductocs frm = new SistemaBase.FrmBuscarProductocs();
            frm.FormClosing += new FormClosingEventHandler(Continuar);
            frm.ShowDialog();
        }

        private void Continuar(object sender, FormClosingEventArgs e)
        {
            if (Principal.CodProoducto != 0)
            {
                Int32 CodProducto = Convert.ToInt32(Principal.CodProoducto);
                BuscarProductoxCodigo(CodProducto);
            }
        }

        private void BuscarProductoxCodigo(Int32 CodProducto)
        {
            int b = 0;
            cFunciones fun = new cFunciones();
            cProducto prod = new Clases.cProducto();
            DataTable trdo = prod.GetProductoxCodigo(CodProducto);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodProducto"].ToString() != "")
                {
                    b = 1;
                    txtCodProducto.Text = trdo.Rows[0]["CodProducto"].ToString();
                    string Nombre = trdo.Rows[0]["Nombre"].ToString();
                    txtNombre.Text = Nombre;
                    txtPrecio.Text = trdo.Rows[0]["Precio"].ToString();
                    if (txtPrecio.Text != "")
                    {
                        txtPrecio.Text = fun.SepararDecimales(txtPrecio.Text);
                        //  txtPrecio.Text = fun.FormatoEnteroMiles(txtPrecio.Text);
                    }
                    txtCodigo.Text = trdo.Rows[0]["Codigo"].ToString();
                    txtStock.Text = trdo.Rows[0]["stock"].ToString();
                    // txtCodigoBarra.Text = trdo.Rows[0]["CodigoBarra"].ToString();
                    
                }
            }
            if (b == 1)
                txtCantidad.Focus();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            FrmAnularVenta frm = new SistemaBase.FrmAnularVenta();
            frm.ShowDialog();
        }
    }
}
