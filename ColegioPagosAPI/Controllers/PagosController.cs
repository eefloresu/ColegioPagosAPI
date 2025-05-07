using Microsoft.AspNetCore.Mvc;
using ColegioPagosAPI.Data;
using ColegioPagosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ColegioPagosAPI.Controllers
{
    /*
     * ApiController
     * Indica que este controlador responderá a solicitudes HTTP tipo API REST.
     * Habilita validación automática de modelos, respuestas 400 por defecto, y
     * binding automático. */
    [ApiController]
    /* [Route("api/[controller]")]: 
     * Define la ruta base para este controlador. [controller] se reemplaza automáticamente por 
     * el nombre del controlador sin el sufijo "Controller". En este caso:
     * ➜ Ruta base: api/pagos
    */
    [Route("api/[controller]")]

    //Constructor de la clase PagosController donde se inyecta el contexto de la base de datos.
    public class PagosController : ControllerBase
    {
        private readonly ColegioDbContext _context;

        public PagosController(ColegioDbContext context)
        {
            _context = context;
        }

        /* Crea un nuevo registro de pago con estado "Pendiente".
         * Guarda en la base de datos y devuelve el objeto como respuesta.*/
        [HttpPost("cargar")]
        public async Task<IActionResult> CargarPago(PagoColegiatura pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return Ok(pago);
        }
        /* Busca el pago por ID.
         * Si no existe, devuelve 404 NotFound.
         * Si existe, cambia su estado a "Pagado" y guarda.*/
        [HttpPost("pagar/{id}")]
        public async Task<IActionResult> Pagar(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();
            pago.Estado = "pagado";
            await _context.SaveChangesAsync();
            return Ok(pago);
        }
        /* Busca un pago por ID.
         * Devuelve 404 si no existe, o el objeto PagoColegiatura si existe.*/
        [HttpGet("consultar/{id}")]
        public async Task<IActionResult> Consultar(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }
        /*Actualiza los campos del pago especificado por ID.
         * Reemplaza los datos por los del nuevo objeto recibido.
         * Devuelve el objeto actualizado.*/
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Actualizar(int id, PagoColegiatura pagoActualizado)
        {
            if (id != pagoActualizado.Id) return BadRequest();
            _context.Entry(pagoActualizado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(pagoActualizado);
        }
        /*Elimina el pago con el ID dado.
         * Devuelve 404 si no lo encuentra, o 200 OK si lo elimina.*/
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}