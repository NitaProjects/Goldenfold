using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetRoles()
        {
            var roles = await _context.Rol.ToListAsync();
            var rolesDTO = _mapper.Map<IEnumerable<RolDTO>>(roles);
            return Ok(rolesDTO);
        }

        // GET: api/Roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetRol(int id)
        {
            var rol = await _context.Rol.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            var rolDTO = _mapper.Map<RolDTO>(rol);
            return Ok(rolDTO);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RolDTO>> AddRol(RolDTO rolDTO)
        {
            if (!Enum.IsDefined(typeof(RoleType), rolDTO.Nombre_Rol))
            {
                return BadRequest("El nombre del rol proporcionado no es válido.");
            }

            if (await _context.Rol.AnyAsync(r => r.Nombre_Rol == rolDTO.Nombre_Rol))
            {
                return Conflict("Ya existe un rol con el nombre proporcionado.");
            }

            var rol = _mapper.Map<Rol>(rolDTO);

            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();

            rolDTO.ID_Rol = rol.ID_Rol;
            return CreatedAtAction(nameof(GetRol), new { id = rolDTO.ID_Rol }, rolDTO);
        }

        // PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditRol(int id, RolDTO rolDTO)
        {
            if (id != rolDTO.ID_Rol)
            {
                return BadRequest("El ID del rol proporcionado no coincide con el ID en la solicitud.");
            }

            if (!Enum.IsDefined(typeof(RoleType), rolDTO.Nombre_Rol))
            {
                return BadRequest("El nombre del rol proporcionado no es válido.");
            }

            var rolExistente = await _context.Rol.FindAsync(id);

            if (rolExistente == null)
            {
                return NotFound("No se encontró el rol especificado.");
            }

            // Verificar si el nombre de rol ya existe (excepto para el rol actual que se está actualizando)
            if (await _context.Rol.AnyAsync(r => r.ID_Rol != id && r.Nombre_Rol == rolDTO.Nombre_Rol))
            {
                return Conflict("Ya existe un rol con el nombre proporcionado.");
            }

            _mapper.Map(rolDTO, rolExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
                {
                    return NotFound("No se encontró el rol especificado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _context.Rol.FindAsync(id);

            if (rol == null)
            {
                return NotFound("No se encontró el rol especificado.");
            }

            _context.Rol.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _context.Rol.Any(e => e.ID_Rol == id);
        }
    }
}