using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVenta.Dominio;
using AppVenta.Dominio.Interfaces.Repositorios;
using AppVenta.Infraestructura.Datos.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppVenta.Infraestructura.Datos.Repositorios
{
    public class VentaRepositorio : IRepositorioMovimiento<Venta, Guid>
    {
        private VentaContexto db;

        public VentaRepositorio(VentaContexto _db)
        {
            db = _db;
        }

        public Venta Agregar(Venta entidad)
        {
            entidad.ventaId = Guid.NewGuid();
            db.Ventas.Add(entidad);
            return entidad;
        }

        public void Anular(Guid entidadId)
        {
            var ventaSeleccionada = db.Ventas.Where(venta => venta.ventaId == entidadId).FirstOrDefault();

            if(ventaSeleccionada == null)
                throw new ArgumentNullException("La 'Venta' no existe");

            ventaSeleccionada.anulado = true;
            db.Entry(ventaSeleccionada).State = EntityState.Modified;
        }

        public void Guardar()
        {
            db.SaveChanges();
        }

        public List<Venta> Listar()
        {
            return db.Ventas.ToList();
        }

        public Venta SeleccionarPorID(Guid entidadId)
        {
            var ventaSeleccionada = db.Ventas.Where(venta => venta.ventaId == entidadId).FirstOrDefault();

            if(ventaSeleccionada == null)
                throw new ArgumentNullException("La 'Venta' no existe");

            return ventaSeleccionada;
        }
    }
}
