using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVenta.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntidad, TEntidadID>
        : IAgregar<TEntidad>, IListar<TEntidad, TEntidadID>, 
          IEditar<TEntidad>, IEliminar<TEntidad>, ITransaccion
    {

    }
}
