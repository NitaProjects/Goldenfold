using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApi;
using HospitalApi.Models;
using AutoMapper;
using HospitalAPI.DTO;

namespace HospitalAPI.Controllers
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
        public async Task<ActionResult<RolDTO>> PostRol(RolDTO rolDTO)
        {
            var rol = _mapper.Map<Rol>(rolDTO);

            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();

            rolDTO.ID_Rol = rol.ID_Rol; // Actualizar el DTO con el ID generado
            return CreatedAtAction("GetRol", new { id = rolDTO.ID_Rol }, rolDTO);
        }

        // PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, RolDTO rolDTO)
        {
            if (id != rolDTO.ID_Rol)
            {
                return BadRequest();
            }

            var rol = await _context.Rol.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _mapper.Map(rolDTO, rol);

            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
                {
                    return NotFound();
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
                return NotFound();
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