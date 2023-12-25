using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVenta.Dominio
{
    public class VentaDetalle
    {
        public Guid ventaDetalleId { get; set; }
        public Guid ventaId { get; set; }
        public Guid productoId { get; set; }
        public decimal costoUnitario { get; set; }
        public decimal precioUnitario { get; set; }
        public int cantidad { get; set; }
        public decimal subTotal { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
        public Producto producto { get; set; }
        public Venta venta { get; set; }

    }
}
