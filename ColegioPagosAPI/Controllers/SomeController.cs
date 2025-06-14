using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
/*
* SomeController
 * Controlador de ejemplo para demostrar el uso de autorización por roles.
 * Contiene endpoints protegidos que requieren roles específicos para acceder.
 * Utiliza el atributo [Authorize] para restringir el acceso a los métodos según el rol del usuario.
*/
namespace ColegioPagosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SomeController : ControllerBase
    {
        [Authorize(Roles = "Administrador")]
        [HttpGet("admin-endpoint")]
        public IActionResult AdminEndpoint()
        {
            return Ok(new { message = "Acceso permitido para Administrador." });
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("client-endpoint")]
        public IActionResult ClientEndpoint()
        {
            return Ok(new { message = "Acceso permitido para Cliente." });
        }
    }
}