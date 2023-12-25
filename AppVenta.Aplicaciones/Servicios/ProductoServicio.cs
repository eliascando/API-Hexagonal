using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVenta.Dominio;
using AppVenta.Dominio.Interfaces.Repositorios;
using AppVenta.Aplicaciones.Interfaces;

namespace AppVenta.Aplicaciones.Servicio
{
    public class ProductoServicio : IServicioBase<Producto, Guid>{

        private readonly IRepositorioBase<Producto, Guid> repoProducto;

        public ProductoServicio(IRepositorioBase<Producto, Guid> _repoProducto)
        {
            repoProducto = _repoProducto;
        }

        public Producto Agregar(Producto entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException("El 'Producto' es requerido");
            
            var resultado = repoProducto.Agregar(entidad);
            repoProducto.Guardar();
            return resultado;
        }

        public void Editar(Producto entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException("El 'Producto' es requerido");

            repoProducto.Editar(entidad);
            repoProducto.Guardar();
        }

        public void Eliminar(Producto entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException("El 'Producto' es requerido");

            repoProducto.Eliminar(entidad);
            repoProducto.Guardar();
        }

        public List<Producto> Listar()
        {
            return repoProducto.Listar();
        }

        public Producto SeleccionarPorID(Guid entidadId)
        {
            return repoProducto.SeleccionarPorID(entidadId);
        }
    }
}
