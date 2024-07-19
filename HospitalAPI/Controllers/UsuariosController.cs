using Microsoft.AspNetCore.Mvc; // Para definir la api
using Microsoft.EntityFrameworkCore; // Utilitzar el contexto de BD
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper; // Mapear datos de BD-DTO

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Indica la ruta base para las solicitudes "api/Usuarios
    public class UsuariosController : ControllerBase // Hereda de la clase controlador
    {
        private readonly ApplicationDbContext _context; // acceder al contexto de la base de datos.
        private readonly IMapper _mapper; // usar el automapper

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            // Constructor que inyecta el contexto de la base de datos y el mapeador.
            _context = context; 
            _mapper = mapper;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync(); // Obtiene todos los usuarios de la bd
            var usuariosDTO = _mapper.Map<IEnumerable<UsuariosDTO>>(usuarios); // Con automapper convierte esta lista en una de DTO
            return Ok(usuariosDTO); // Devuelve la lista de DTOs con un código de estado HTTP 200 OK.
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id); // Busca con el id indicado en la bd

            if (usuario == null)
            {
                return NotFound();  // Si no encuentra nada pues 404...
            }

            var usuarioDTO = _mapper.Map<UsuariosDTO>(usuario); // convertir al usuario en uno de DTO
            return Ok(usuarioDTO); // Retorna el usuario y un OK 200
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuariosDTO>> AddUsuario(UsuariosDTO usuarioDTO)
        {
            // Verificar si el nombre de usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.Usuario == usuarioDTO.Usuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            var usuario = _mapper.Map<Usuarios>(usuarioDTO); // convertir el user encontrado a uno dto

            _context.Usuarios.Add(usuario); // Agrega el nuevo usuario al contexto de la DB
            await _context.SaveChangesAsync(); // Guarda los cambios en la db

            usuarioDTO.ID_Usuario = usuario.ID_Usuario; // Actualizo el id del dto con el generao
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDTO.ID_Usuario }, usuarioDTO); // Retorna el nuevo usuario creado con un código de estado HTTP 201 Created.
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUsuario(int id, UsuariosDTO usuarioDTO)
        {
            if (id != usuarioDTO.ID_Usuario)
            {
                return BadRequest("El ID del usuario proporcionado no coincide con el ID en la solicitud.");
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound("No se encontró el usuario especificado.");
            }

            // Verificar si el nombre de usuario ya existe (excepto para el usuario actual)
            if (await _context.Usuarios.AnyAsync(u => u.ID_Usuario != id && u.Usuario == usuarioDTO.Usuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            _mapper.Map(usuarioDTO, usuarioExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound("No se encontró el usuario especificado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("No se encontró el usuario especificado.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID_Usuario == id);
        }
    }
}