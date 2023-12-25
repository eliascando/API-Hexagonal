using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVenta.Dominio.DTOs
{
    public class VentaDTO
    {
        public DateTime fecha { get; set; }
        public string concepto { get; set; }
        public bool anulado { get; set; }
        public List<VentaDetalleDTO> VentaDetalles { get; set; }
    }
}
