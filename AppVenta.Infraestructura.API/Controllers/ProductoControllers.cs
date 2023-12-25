using Microsoft.AspNetCore.Mvc;
using AppVenta.Dominio;
using AppVenta.Aplicaciones.Servicios;
using AppVenta.Infraestructura.Datos.Contextos;
using AppVenta.Infraestructura.Datos.Repositorios;
using AppVenta.Aplicaciones.Servicio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppVenta.Infraestructura.API.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoControllers : ControllerBase
    {

        ProductoServicio CrearServicio()
        {
            VentaContexto db = new VentaContexto();
            ProductoRepositorio repositorio = new ProductoRepositorio(db);
            ProductoServicio servicio = new ProductoServicio(repositorio);
            return servicio;
        }

        // GET: api/<ProductoControllers>
        [HttpGet]
        public ActionResult <List<Producto>> Get()
        {
            var servicio = CrearServicio();
            return Ok(servicio.Listar());
        }

        // GET api/<ProductoControllers>/5
        [HttpGet("{id}")]
        public ActionResult <Producto> Get(Guid id)
        {
            var servicio = CrearServicio();
            return Ok(servicio.SeleccionarPorID(id));
        }

        // POST api/<ProductoControllers>
        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            var servicio = CrearServicio();
            servicio.Agregar(producto);
            return Ok("Producto agregado correctamente");
        }

        // PUT api/<ProductoControllers>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Producto producto)
        {
            var servicio = CrearServicio();
            producto.productoId = id;
            if (id != producto.productoId)
            {
                return BadRequest("No se pudo actualizar el producto");
            }
            servicio.Editar(producto);
            return Ok("Producto actualizado correctamente");
        } 

        // DELETE api/<ProductoControllers>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var servicio = CrearServicio();
            servicio.Eliminar(new Producto { productoId = id });
            return Ok("Producto eliminado correctamente");
        }
    }
}
