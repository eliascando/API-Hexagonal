using Microsoft.AspNetCore.Mvc;
using AppVenta.Dominio;
using AppVenta.Aplicaciones.Servicios;
using AppVenta.Infraestructura.Datos.Contextos;
using AppVenta.Infraestructura.Datos.Repositorios;
using AppVenta.Aplicaciones.Servicio;
using AppVenta.Dominio.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppVenta.Infraestructura.API.Controllers
{
    [Route("api/venta")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        VentaServicio CrearServicio()
        {
            VentaContexto db = new VentaContexto();
            VentaRepositorio repoVenta = new VentaRepositorio(db);
            ProductoRepositorio repoProducto = new ProductoRepositorio(db);
            VentaDetalleRepositorio repoDetalle = new VentaDetalleRepositorio(db);
            VentaServicio servicio = new VentaServicio(repoVenta, repoProducto, repoDetalle);

            return servicio;
        }

        // GET: api/<VentaController>
        [HttpGet]
        public ActionResult Get()
        {
            VentaServicio servicio = CrearServicio();
            return Ok(servicio.Listar());
        }

        // GET api/<VentaController>/5
        [HttpGet("{id}")]
        public ActionResult<Venta> Get(Guid id)
        {
            var servicio = CrearServicio();
            return Ok(servicio.SeleccionarPorID(id));
        }

        // POST api/<VentaController>
        [HttpPost]
        public ActionResult Post([FromBody] VentaDTO ventaDTO)
        {
            if (ventaDTO == null)
            {
                return BadRequest("La solicitud de venta no puede ser nula.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var servicio = CrearServicio();
                var venta = servicio.AgregarDesdeDTO(ventaDTO);
                return Ok("Venta agregada con éxito!");
            }
            catch (Exception ex)
            {
                // Aquí deberías registrar la excepción con un logger.
                return StatusCode(500, $"Un error ocurrió al procesar su solicitud, \nDetalles:\n{ex}");
            }
        }

        // DELETE api/<VentaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var servicio = CrearServicio();
            servicio.Anular(id);
            return Ok("Venta anulada");
        }
    }
}
