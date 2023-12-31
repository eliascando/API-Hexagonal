﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVenta.Dominio
{
    public class Venta
    {
        public Guid ventaId { get; set; }
        public DateTime fecha { get; set; }
        public string concepto { get; set; }
        public decimal subTotal { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
        public Boolean anulado { get; set; }
        public List<VentaDetalle> ventaDetalles { get; set; }
    }
}
