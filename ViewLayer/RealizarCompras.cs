﻿using ProyectoFinal.ControlerLayer;
using ProyectoFinal.DataLayer;
using ProyectoFinal.ViewLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.ViewLayer
{
    public partial class RealizarCompras : Form
    {
        private List<string> asientosSeleccionados = new List<string>();

        public List<string> AsientosSeleccionados { get => asientosSeleccionados; set => asientosSeleccionados = value; }

        public RealizarCompras()
        {
            InitializeComponent();
            txtBusquedaNombre.Text = "Nombre";
            txtBusquedaNombre.ForeColor = Color.Gray;
            CargarGrilla();
        }

        private void TxtBusquedaNombre_MouseEnter(object sender, EventArgs e)
        {
            if (txtBusquedaNombre.Text == "Nombre")
            {
                txtBusquedaNombre.Text = string.Empty;
                txtBusquedaNombre.ForeColor = Color.Black;
            }
        }

        private void TxtBusquedaNombre_MouseLeave(object sender, EventArgs e)
        {
            if (txtBusquedaNombre.Text == "" && !txtBusquedaNombre.Focused)
            {
                txtBusquedaNombre.Text = "Nombre";
                txtBusquedaNombre.ForeColor = Color.Gray;
            }
        }
        private void TxtBusquedaNombre_Enter(object sender, EventArgs e)
        {
            if (txtBusquedaNombre.Text == "Nombre")
            {
                txtBusquedaNombre.Text = "";
                txtBusquedaNombre.ForeColor = Color.Black;
            }
        }

        private void TxtBusquedaNombre_Leave(object sender, EventArgs e)
        {
            if (txtBusquedaNombre.Text == "")
            {
                txtBusquedaNombre.Text = "Nombre";
                txtBusquedaNombre.ForeColor = Color.Gray;
            }
        }

        private void CargarGrilla()
        {
            string[] datos = { "Nombre", "Precio", "Id" };
            dgvRealizarCompra.Columns.Clear();
            dgvRealizarCompra.Rows.Clear();
            foreach (string items in datos)
            {
                dgvRealizarCompra.Columns.Add(items, items);
                if (items == "Id")
                {
                    dgvRealizarCompra.Columns["Id"].Visible = false;
                }
            }
            LlenarGrilla(new GetData().GetEspectaculos());
        }




        private void DgvRealizarCompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
       
        private void LlenarGrilla(List<Espectaculo> espectaculos)
        {
            dgvRealizarCompra.Rows.Clear();
            foreach (var item1 in espectaculos)
            {

                if (item1.EstadoEspectaculo)
                {
                    item1.Compania = new GetData().GetByIdCompañia(item1.Companiaid);
                    dgvRealizarCompra.Rows.Add(item1.NombreEspectaculo, item1.PrecioEspectaculo, item1.Id);
                }

            }
        }
        private void PrepareToPaint(object sender, PaintEventArgs e)
        {
            List<LocalidadAsiento> asientos;
            Control control = sender as Control;
            string sectorName = control.Text;
            if (dgvRealizarCompra.CurrentCell != null)
            {
                int currentRow = dgvRealizarCompra.CurrentCell.RowIndex;
                long espectaculoId = long.Parse(dgvRealizarCompra.Rows[currentRow].Cells["Id"].Value.ToString());
                asientos = new GetData().GetAsientosBySector(sectorName, espectaculoId);
                DrawOnGroupBox(control, e, asientos);
            }
            else
            {
                asientos = new GetData().GetAsientosBySector(sectorName);
                DrawOnGroupBox(control, e, asientos);
            }
        }
        private void DrawOnGroupBox(Control groupBox, PaintEventArgs e, List<LocalidadAsiento> asientos)
        {
            try
            {
                Graphics g = e.Graphics;
                int count = asientos.Count;
                // Calcular el tamaño de cada rectángulo
                double areaGroupBox = Math.Ceiling((double)(groupBox.ClientSize.Width * groupBox.ClientSize.Height));
                areaGroupBox = Math.Ceiling(areaGroupBox - (areaGroupBox * 0.05));
                int areaSquare = (int)Math.Ceiling((double)areaGroupBox / asientos.Count);
                int side = (int)Math.Sqrt(areaSquare);
                int space = (side / 4);
                side = side - space;
                int rowIndex = 12;
                int columnIndex = space;
                foreach (var asiento in asientos)
                {
                    if (columnIndex + side <= groupBox.Width)
                    {

                        if (asiento.EstadoAsiento)
                        {
                            g.FillRectangle(Brushes.Green, columnIndex, rowIndex, side, side);
                            g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                            columnIndex = columnIndex + side + space;
                            if (asiento.NumeroAsiento == 282 || asiento.NumeroAsiento == 281 || asiento.NumeroAsiento == 283)
                            {
                                Console.WriteLine($"pinta asiento x:{columnIndex} y: {rowIndex} width:  {groupBox.Width}  side:  {side} groupbox: {groupBox.Text} numeroasiento: {asiento.NumeroAsiento}");
                                g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                            }
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Red, columnIndex, rowIndex, side, side);
                            g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                            columnIndex = columnIndex + side + space;
                            if (asiento.NumeroAsiento == 282 || asiento.NumeroAsiento == 281 || asiento.NumeroAsiento == 283)
                            {
                                Console.WriteLine($"pinta asiento x:{columnIndex} y: {rowIndex} width:  {groupBox.Width}  side:  {side} groupbox: {groupBox.Text} numeroasiento: {asiento.NumeroAsiento}");
                            }
                        }
                        //g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                        //columnIndex = columnIndex + side + space;
                    }
                    else if (rowIndex + side <= groupBox.Height)
                    {
                        if (asiento.EstadoAsiento)
                        {
                            g.FillRectangle(Brushes.Green, columnIndex, rowIndex, side, side);
                            g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                            columnIndex = columnIndex + side + space;
                            columnIndex = space;
                            rowIndex = rowIndex + side + space;
                            if (asiento.NumeroAsiento == 282 || asiento.NumeroAsiento == 281 || asiento.NumeroAsiento == 283)
                            {
                                Console.WriteLine($"pinta asiento x:{columnIndex} y: {rowIndex} width:  {groupBox.Width}  side:  {side} groupbox: {groupBox.Text} numeroasiento: {asiento.NumeroAsiento}");
                            }
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Red, columnIndex, rowIndex, side, side);
                            g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                            columnIndex = columnIndex + side + space;
                            columnIndex = space;
                            rowIndex = rowIndex + side + space;
                            if (asiento.NumeroAsiento == 282 || asiento.NumeroAsiento == 281 || asiento.NumeroAsiento == 283)
                            {
                                Console.WriteLine($"pinta asiento x:{columnIndex} y: {rowIndex} width:   {groupBox.Width}   side:   {side}  groupbox:  {groupBox.Text} numeroasiento: {asiento.NumeroAsiento}");


                            }
                        }
                        //g.DrawString(asiento.NumeroAsiento.ToString(), new Font("Arial", 8), Brushes.White, new PointF(columnIndex + (side / 4), rowIndex + (side / 4)));
                    }

                }
            }
            catch (DivideByZeroException error)
            {
                MessageBox.Show($"Error: {error}");
            }
        }
        private void DgvCellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex;
            if (fila <= dgvRealizarCompra.RowCount - 2)
            {
                gbSA.Invalidate();
                gbSC.Invalidate();
            }

        }

        private void LblMetodopago_Click(object sender, EventArgs e)
        {

        }

        private void TxtBusquedaNombre_TextChanged(object sender, EventArgs e)
        {
            new PropsTexBox().txtBusquedaNombre_TextChanged(sender, e, txtBusquedaNombre, dgvRealizarCompra, "Nombre");
        }

        private void DgvRealizarCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnComprar_Click(object sender, EventArgs e)
        {

        }

        private void TxtCantidadAsientos_TextChanged(object sender, EventArgs e)
        {
            int aux;            
            try
            {
                aux = int.Parse(txtCantidadAsientos.Text);
                if (aux < 1 || aux > 10)
                {
                    lblInfoCantidadAsientos.ForeColor = Color.Red;
                    lblInfoCantidadAsientos.Text = "Por favor ingrese un numero\ncomprendido entre 1 y 10";
                }
                else
                {
                    lblInfoCantidadAsientos.ForeColor = Color.Black;
                    lblInfoCantidadAsientos.Text = "Ingrese la cantidad de asientos (del 1 al 10)";
                }

            }
            catch (System.FormatException error)
            {
                lblInfoCantidadAsientos.ForeColor = Color.Red;
                lblInfoCantidadAsientos.Text = "Por favor ingrese un numero";                               
            }

        }

        private void TxtAsientosSeleccionados_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnSeleccionAsientos_Click(object sender, EventArgs e)
        {
            float precio;
            asientosSeleccionados.Clear();
            long espectaculoID = long.Parse(dgvRealizarCompra.CurrentRow.Cells["Id"].Value.ToString());
            bool asientosOK = true;
            List<string> asientosDisponibles = new GetData().GetAsientosDisponibles(espectaculoID);
            foreach (var item in txtAsientosSeleccionados.Text.Split(new char[] { ' ', ',', '.', '/', '-' }, StringSplitOptions.RemoveEmptyEntries))
            {
                AsientosSeleccionados.Add(item);
            }
            if (int.Parse(txtCantidadAsientos.Text) == AsientosSeleccionados.Count)
            {
                foreach (var asientoSeleccionado in AsientosSeleccionados)
                {
                    if (!asientosDisponibles.Contains(asientoSeleccionado))
                    {
                        lblInfoAsientosSelccionados.ForeColor = Color.Red;
                        lblInfoAsientosSelccionados.Text = "Uno o mas asientos seleccionados\nno estan disponibles";
                        asientosOK = false;
                        break;
                    }
                }
                if (asientosOK)
                {
                    lblInfoAsientosSelccionados.ForeColor = Color.Green;
                    lblInfoAsientosSelccionados.Text = "Asientos seleccionados";
                    precio = new GetData().calcularPrecio(asientosSeleccionados, espectaculoID);
                    lblValorimporte.Text = $"${precio}";
                }
                    
            }
            else
            {
                lblInfoAsientosSelccionados.ForeColor = Color.Red;
                lblInfoAsientosSelccionados.Text = "La cantidad de asientos seleccionados\nno coincide";
            }
        }
    }
}
