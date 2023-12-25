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
    class ProductoConfig: IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("tblPoductos");
            builder.HasKey(producto => producto.productoId);

            builder.HasMany(producto => producto.ventaDetalles)
                .WithOne(ventaDetalle => ventaDetalle.producto);
        }
    }
}
