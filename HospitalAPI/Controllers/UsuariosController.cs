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
        public async Task<ActionResult<IEnumerable<UsuariosDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var usuariosDTO = _mapper.Map<IEnumerable<UsuariosDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDTO = _mapper.Map<UsuariosDTO>(usuario);
            return Ok(usuarioDTO);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuariosDTO>> AddUsuario(UsuariosDTO usuarioDTO)
        {
            if (await _context.Usuarios.AnyAsync(u => u.usuario == usuarioDTO.usuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            if (!await _context.Roles.AnyAsync(r => r.id_rol == usuarioDTO.id_rol))
            {
                return Conflict("Este rol no existe.");
            }

            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuarioDTO.id_usuario = usuario.id_usuario;
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDTO.ID_Usuario }, usuarioDTO);
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

            if (await _context.Usuarios.AnyAsync(u => u.ID_Usuario != id && u.Usuario == usuarioDTO.Usuario))
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            if (!await _context.Rol.AnyAsync(r => r.ID_Rol == usuarioDTO.ID_Rol))
            {
                return Conflict("Este rol no existe");
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
