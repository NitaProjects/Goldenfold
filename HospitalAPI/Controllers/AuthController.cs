using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalApi; 
using BCrypt.Net; // Para manejar el hashing de contraseñas
using Microsoft.EntityFrameworkCore; 
using HospitalApi.Models;
using HospitalApi.DTO; // Para importar los DTOs necesarios
using System.ComponentModel.DataAnnotations; // Para RequiredAttribute

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Endpoint para iniciar sesión
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginRequest)
        {
            if (ModelState.IsValid)
            {
                // Buscar el usuario en la base de datos e incluir el rol
                var usuario = _context.Usuarios
                    .Include(u => u.Rol) // Incluir el rol asociado al usuario
                    .FirstOrDefault(u => u.NombreUsuario == loginRequest.NombreUsuario);

                // Verificar si el usuario existe y si la contraseña es correcta usando BCrypt
                if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Contrasenya, usuario.Contrasenya))
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                // Emitir el token JWT si las credenciales son válidas
                var token = GenerateJwtToken(usuario);

                return Ok(new { Token = token });
            }

            return BadRequest("Request no válida");
        }

        // Endpoint para registrar un usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioCreateDTO usuarioCreateDTO)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el nombre de usuario ya existe
                if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuarioCreateDTO.NombreUsuario))
                {
                    return BadRequest(new { message = "El nombre de usuario ya está en uso." });
                }

                // Verificar si el rol existe
                if (!await _context.Roles.AnyAsync(r => r.IdRol == usuarioCreateDTO.IdRol))
                {
                    return BadRequest(new { message = "El rol proporcionado no existe." });
                }

                // Crear el nuevo usuario con la contraseña hasheada
                var usuario = new Usuario
                {
                    Nombre = usuarioCreateDTO.Nombre,
                    NombreUsuario = usuarioCreateDTO.NombreUsuario,
                    Contrasenya = BCrypt.Net.BCrypt.HashPassword(usuarioCreateDTO.Contrasenya), // Hash de la contraseña
                    IdRol = usuarioCreateDTO.IdRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuario registrado exitosamente." });
            }

            return BadRequest(ModelState);
        }

        // Método para generar el token JWT
        private string GenerateJwtToken(Usuario usuario)
        {
            // Crear la clave de seguridad simétrica
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definir los claims básicos
            var claims = new List<Claim>
            {
                new Claim("UserId", usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.NombreUsuario), // Suponiendo que el nombre de usuario es único
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.NombreRol) // Añadir el rol como claim
            };

            // Crear el token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // DTO para el login
    public class LoginDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contrasenya { get; set; }
    }
}
