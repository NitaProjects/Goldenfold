using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            if (!usuarios.Any())
            {
                return NotFound("No se han encontrado usuarios.");
            }

            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            return Ok(usuariosDTO);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUserById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("No se ha encontrado ningun usuario con este id.");
            }

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        // GET: api/Usuarios/ByName/{nombre}
        [HttpGet("ByName/{nombre}")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUserByName(string nombre)
        {
            var usuarios = await _context.Usuarios
                .Where(u => u.NombreUsuario.Contains(nombre))
                .ToListAsync();

            if (!usuarios.Any())
            {
                return NotFound("No se ha encontrado ningún usuario con este nombre.");
            }

            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> AddUser(UsuarioDTO usuarioDTO)
        {
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            if (!await _context.Roles.AnyAsync(r => r.IdRol == usuarioDTO.IdRol))
            {
                return Conflict("El rol no existe.");
            }

            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuarioDTO.IdUsuario = usuario.IdUsuario;
            return CreatedAtAction(nameof(GetUserById), new { id = usuarioDTO.IdUsuario }, usuarioDTO);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUserById(int id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.IdUsuario)
            {
                return BadRequest("El ID del usuario proporcionado no coincide con el ID en la solicitud.");
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound("No se encontró el usuario especificado.");
            }

            if (await _context.Usuarios.AnyAsync(u => u.IdUsuario != id && u.NombreUsuario == usuarioDTO.NombreUsuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            if (!await _context.Roles.AnyAsync(r => r.IdRol == usuarioDTO.IdRol))
            {
                return Conflict("El rol no existe");
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

        // PUT: api/Usuarios/ByName/{nombre}
        [HttpPut("ByName/{nombre}")]
        public async Task<IActionResult> EditUserByName(string nombre, UsuarioDTO usuarioDTO)
        {
            var usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombre);

            if (usuarioExiste == null)
            {
                return NotFound("No se ha encontrado ningún usuario con ese nombre de usuario.");
            }

            if (await _context.Usuarios.AnyAsync(u => u.IdUsuario != usuarioExiste.IdUsuario && u.NombreUsuario == usuarioDTO.NombreUsuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            _mapper.Map(usuarioDTO, usuarioExiste);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el usuario.");
            }

            return NoContent();
        }



        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
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

        // DELETE: api/Usuarios/ByName/{nombre}
        [HttpDelete("ByName/{nombre}")]
        public async Task<IActionResult> DeleteUserByName(string nombre)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombre);

            if (usuario == null)
            {
                return NotFound("No se ha encontrado este usuario. Asegúrate de que el nombre de usuario es correcto.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
