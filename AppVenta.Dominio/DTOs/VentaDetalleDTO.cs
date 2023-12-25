using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVenta.Dominio.DTOs
{
    public class VentaDetalleDTO
    {
        public Guid productoId { get; set; }
        public int cantidad { get; set; }
    }
}
