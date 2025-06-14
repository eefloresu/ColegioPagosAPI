using ColegioPagosAPI.Data;
using ColegioPagosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace ColegioPagosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ColegioDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ColegioDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        /* Registra un nuevo usuario en la base de datos.
         * Encripta la contraseña antes de guardarla.
         * <param name="usuario">Objeto Usuario con los datos a registrar.</param>
         * <returns>Mensaje de éxito.</returns>
        */
        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario usuario)
        {
            usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(new { message = "Usuario registrado exitosamente." });
        }


        /* Inicia sesión de usuario.
         * Verifica las credenciales y, si son válidas, genera y retorna un JWT.
         * <param name="login">Objeto LoginRequest con nombre de usuario y clave.</param>
         * <returns>Token JWT si las credenciales son correctas, error si no lo son.</returns>
        */
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == login.NombreUsuario);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Clave, usuario.Clave))
                return Unauthorized(new { message = "Credenciales inválidas." });

            // Generar JWT
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            Console.WriteLine("llave al generar token: " + jwtKey);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tu_issuer",
                audience: "tu_audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(48),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }
    }
}