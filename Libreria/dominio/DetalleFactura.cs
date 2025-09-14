using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Dominio
{
    public class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public int IdFactura { get; set; }
        public int idArticulo { get; set; }
        public int Cantidad { get; set; }
        public string nombreArticulo { get; set; }  
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal ()
        {

            return Cantidad * PrecioUnitario;

        }
        public override string ToString()
        {
            return $" Articulos: {nombreArticulo}, Cantidad: {Cantidad}, Precio Unitario: {PrecioUnitario}, SUBTOTAL: {Subtotal():0.00}";
        }
    }
}
