using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVenta.Dominio;
using AppVenta.Dominio.Interfaces;
using AppVenta.Aplicaciones.Interfaces;
using AppVenta.Dominio.Interfaces.Repositorios;
using AppVenta.Dominio.DTOs;

namespace AppVenta.Aplicaciones.Servicios
{
    public class VentaServicio : IServicioMovimiento<Venta, Guid>
    {
        IRepositorioMovimiento<Venta, Guid> repoVenta;
        IRepositorioBase<Producto, Guid> repoProducto;
        IRepositorioDetalle<VentaDetalle, Guid> repoDetalle;
        
        public VentaServicio(
            IRepositorioMovimiento<Venta, Guid> _repoVenta,
            IRepositorioBase<Producto, Guid> _repoProducto,
            IRepositorioDetalle<VentaDetalle, Guid> _repoDetalle
        ){
            this.repoVenta = _repoVenta;
            this.repoProducto = _repoProducto;
            this.repoDetalle = _repoDetalle;
        }

        public Venta Agregar(Venta entidad)
        {
            if(entidad == null)
                throw new ArgumentNullException("El 'Venta' es requerido");

            entidad.ventaDetalles.ForEach(detalle => {
                var productoSeleccionado = repoProducto.SeleccionarPorID(detalle.productoId);
                
                if(productoSeleccionado == null)
                    throw new ArgumentNullException("El 'Producto' no existe");

                detalle.productoId = detalle.productoId;
                detalle.costoUnitario = productoSeleccionado.costo;
                detalle.precioUnitario = productoSeleccionado.precio;
                detalle.cantidad = detalle.cantidad;
                detalle.subTotal = detalle.cantidad * detalle.precioUnitario;
                detalle.impuesto = detalle.subTotal * 0.14m;
                detalle.total = detalle.subTotal + detalle.impuesto;
                repoDetalle.Agregar(detalle);

                productoSeleccionado.cantidadStock -= detalle.cantidad;
                repoProducto.Editar(productoSeleccionado);

                entidad.subTotal += detalle.subTotal;
                entidad.impuesto += detalle.impuesto;
                entidad.total += detalle.total;
            });

            var resultado = repoVenta.Agregar(entidad);
            repoVenta.Guardar();
            return entidad;
        }

        private Venta ConvertirDTOaEntidad(VentaDTO ventaDTO)
        {
            var venta = new Venta
            {
                ventaId = Guid.NewGuid(), // O asignar de otra manera si es necesario
                fecha = ventaDTO.fecha,
                concepto = ventaDTO.concepto,
                anulado = ventaDTO.anulado,
                ventaDetalles = new List<VentaDetalle>()
            };

            foreach (var detalleDTO in ventaDTO.VentaDetalles)
            {
                var producto = repoProducto.SeleccionarPorID(detalleDTO.productoId);
                if (producto == null)
                {
                    throw new ArgumentException($"Producto con ID {detalleDTO.productoId} no encontrado.");
                }

                var detalle = new VentaDetalle
                {
                    ventaDetalleId = Guid.NewGuid(),
                    productoId = detalleDTO.productoId,
                    costoUnitario = producto.costo,
                    precioUnitario = producto.precio,
                    cantidad = detalleDTO.cantidad,
                    subTotal = detalleDTO.cantidad * producto.precio,
                    impuesto = detalleDTO.cantidad * producto.precio * 0.14m,
                    total = detalleDTO.cantidad * producto.precio * 1.14m
                };

                venta.ventaDetalles.Add(detalle);
            }

            return venta;
        }

        public Venta AgregarDesdeDTO(VentaDTO ventaDTO)
        {
            if(ventaDTO == null)
                throw new ArgumentNullException("El 'Venta' es requerido");

            var venta = ConvertirDTOaEntidad(ventaDTO);
            return Agregar(venta);
        }   

        public void Anular(Guid entidadId)
        {
            repoVenta.Anular(entidadId);
            repoVenta.Guardar();
        }

        public List<Venta> Listar()
        {
            return repoVenta.Listar();
        }

        public Venta SeleccionarPorID(Guid entidadId)
        {
            return repoVenta.SeleccionarPorID(entidadId);
        }
    }
}
