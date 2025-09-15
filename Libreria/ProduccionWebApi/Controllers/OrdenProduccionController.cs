using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Entities;
using ProduccionBack.Services;

namespace ProduccionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenProduccionController : ControllerBase
    {
        private IProduccionService service;

        public OrdenProduccionController()
        {
            service = new ProduccionService();
        }

        // GET: api/<OrdenProduccionController>
        [HttpGet("componentes")]
        public IActionResult Get()
        {
            return Ok(service.ConsultarComponentes());
        }

        //GET: Ordenes
        [HttpGet("ordenes")]
        public IActionResult GetOrdenes([FromQuery] DateTime? fecha, [FromQuery] string? estado)
        {
            try
            {
                var lista=service.ConsultarOrdenes(fecha, estado);
                if (lista.Count == 0) 
                    return NotFound("No se encontraron órdenes con esos criterios!!!");
                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500, "No se pudo consultar las ordenes!!!");
            }
        }

        // POST api/<OrdenProduccionController>
        [HttpPost]
        public IActionResult Post([FromBody] OrdenProduccion orden)
        {
            try { 
                if(orden == null)
                {
                    return BadRequest("Se esperaba una orden de producción completa");
                }
                if (service.RegistrarProduccion(orden))
                    return Ok("Orden registrada con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar la orden!");
            }
            catch (Exception )
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }
    }
}
