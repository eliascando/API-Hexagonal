using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVenta.Dominio.Interfaces;

namespace AppVenta.Aplicaciones.Interfaces
{
    public interface IServicioBase<TEntidad, TEntidadID>
        : IAgregar<TEntidad>, IListar<TEntidad, TEntidadID>, IEditar<TEntidad>, IEliminar<TEntidad>
    {

    }
}
