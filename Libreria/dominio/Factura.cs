using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int idFormaPago { get; set; }
        public string FormaPago { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
        public decimal Total()
        {
            return Detalles.Sum(d => d.Subtotal());

        }
        public override string ToString()
        {
            string detallesStr = "";
            if (Detalles != null && Detalles.Count > 0)
            {
                foreach (var d in Detalles)
                {
                    detallesStr += $"    {d}\n"; 
                }
            }
            else
            {
                detallesStr = "    Sin detalles\n";
            }

            return $"Factura: {NroFactura}, Fecha: {Fecha.ToShortDateString()}, Cliente: {Cliente}, Forma de pago: {FormaPago}\n{detallesStr}" +
                   $"Total: {Total():0.00}";
        }
    }
}
