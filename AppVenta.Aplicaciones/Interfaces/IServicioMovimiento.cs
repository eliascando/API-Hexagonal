using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVenta.Dominio.Interfaces;

namespace AppVenta.Aplicaciones.Interfaces
{
    public interface IServicioMovimiento<TEntidad, TEntidadID>
        : IAgregar<TEntidad>, IListar<TEntidad, TEntidadID>
    {
        void Anular(TEntidadID entidadId);
    }
}
