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
            var roles = await _context.Roles.ToListAsync();
            var rolesDTO = _mapper.Map<IEnumerable<RolDTO>>(roles);
            return Ok(rolesDTO);
        }

        // GET: api/Roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetRolById(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            var rolDTO = _mapper.Map<RolDTO>(rol);
            return Ok(rolDTO);
        }

        // GET: api/Roles/ByName/{nombre}
        [HttpGet("ByName/{nombre}")]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetRolByName(string nombre)
        {
            var roles = await _context.Roles
                .Where(u => u.NombreRol.ToString().Contains(nombre))
                .ToListAsync();

            if (!roles.Any())
            { 
                return NotFound("No se ha encontrado ningún rol con este nombre.");
            }

            var rolesDTO = _mapper.Map<IEnumerable<RolDTO>>(roles);
            return Ok(rolesDTO);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RolDTO>> AddRol(RolDTO rolDTO)
        {
            // Verifica si el nombre del rol proporcionado es válido según el enum RoleType
            if (!Enum.IsDefined(typeof(RoleType), rolDTO.NombreRol))
            {
                return BadRequest("El nombre del rol proporcionado no es válido.");
            }

            // Verifica si ya existe un rol con el nombre proporcionado
            if (await _context.Roles.AnyAsync(r => r.NombreRol == rolDTO.NombreRol))
            {
                return Conflict("Ya existe un rol con el nombre proporcionado.");
            }

            var rol = _mapper.Map<Rol>(rolDTO);

            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            rolDTO.IdRol = rol.IdRol;

            return CreatedAtAction(nameof(GetRolById), new { id = rolDTO.IdRol }, rolDTO);
        }


        // PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditRolById(int id, RolDTO rolDTO)
        {
            if (id != rolDTO.IdRol)
            {
                return BadRequest("El ID del rol proporcionado no coincide con el ID en la solicitud.");
            }

            if (!Enum.IsDefined(typeof(RoleType), rolDTO.NombreRol))
            {
                return BadRequest("El nombre del rol proporcionado no es válido.");
            }

            var rolExistente = await _context.Roles.FindAsync(id);

            if (rolExistente == null)
            {
                return NotFound("No se encontró el rol especificado.");
            }

            // Verificar si el nombre de rol ya existe (excepto para el rol actual que se está actualizando)
            if (await _context.Roles.AnyAsync(r => r.IdRol != id && r.NombreRol == rolDTO.NombreRol))
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

        // PUT: api/Roles/ByName/{nombre}
        [HttpPut("ByName/{nombre}")]
        public async Task<IActionResult> EditRolByName(string nombre, RolDTO rolDTO)
        {
            // Verifica si el nombre del rol proporcionado es válido según el enum RoleType
            if (!Enum.TryParse(nombre, out RoleType roleType))
            {
                return BadRequest("El nombre del rol proporcionado no es válido.");
            }

            // Busca el rol existente por nombre
            var rolExiste = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == roleType);

            if (rolExiste == null)
            {
                return NotFound("No se ha encontrado ningún rol con ese nombre.");
            }

            // Verifica si el nombre de rol ya existe (excepto para el rol actual que se está actualizando)
            if (await _context.Roles.AnyAsync(r => r.IdRol != rolExiste.IdRol && r.NombreRol == rolDTO.NombreRol))
            {
                return Conflict("Ya existe un rol con el nombre proporcionado.");
            }

            _mapper.Map(rolDTO, rolExiste);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el rol.");
            }

            return NoContent();
        }


        // DELETE: api/Roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound("No se encontró el rol especificado.");
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Roles/ByName/{nombre}
        [HttpDelete("ByName/{nombre}")]
        public async Task<IActionResult> DeleteRolByName(string nombre)
        {
            // Verifica si el nombre del rol proporcionado es válido según el enum RoleType
            if (!Enum.TryParse(nombre, out RoleType roleType))
            {
                return BadRequest("El nombre del rol proporcionado no es válido.");
            }

            // Busca el rol existente por nombre
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == roleType);

            if (rol == null)
            {
                return NotFound("No se ha encontrado ningún rol con ese nombre.");
            }

            // Elimina el rol de la base de datos
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.IdRol == id);
        }
    }
}