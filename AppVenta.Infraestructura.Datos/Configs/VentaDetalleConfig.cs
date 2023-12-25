using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVenta.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppVenta.Infraestructura.Datos.Configs
{
    class VentaDetalleConfig : IEntityTypeConfiguration<VentaDetalle>
    {
        public void Configure(EntityTypeBuilder<VentaDetalle> builder)
        {
            builder.ToTable("tblVentaDetalles");
            builder.HasKey(ventaDetalle => ventaDetalle.ventaDetalleId);

            builder.HasOne(detalle => detalle.producto)
                .WithMany(producto => producto.ventaDetalles);

            builder.HasOne(detalle => detalle.venta)
                .WithMany(venta => venta.ventaDetalles);
        }
    }
}
