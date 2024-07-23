using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsignacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AsignacionesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Asignaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsignacionesDTO>>> GetAsignaciones()
        {
            var asignaciones = await _context.Asignacion.ToListAsync();
            var asignacionesDTO = _mapper.Map<IEnumerable<AsignacionesDTO>>(asignaciones);
            return Ok(asignacionesDTO);
        }

        // GET: api/Asignaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AsignacionesDTO>> GetAsignacion(int id)
        {
            var asignacion = await _context.Asignacion.FindAsync(id);

            if (asignacion == null)
            {
                return NotFound();
            }

            var asignacionDTO = _mapper.Map<AsignacionesDTO>(asignacion);
            return Ok(asignacionDTO);
        }

        // POST: api/Asignaciones
        [HttpPost]
        public async Task<ActionResult<AsignacionesDTO>> PostAsignacion(AsignacionesDTO asignacionDTO)
        {
            var asignacion = _mapper.Map<Asignacion>(asignacionDTO);

            _context.Asignacion.Add(asignacion);
            await _context.SaveChangesAsync();

            asignacionDTO.ID_Asignacion = asignacion.ID_Asignacion;
            return CreatedAtAction("GetAsignacion", new { id = asignacionDTO.ID_Asignacion }, asignacionDTO);
        }

        // PUT: api/Asignaciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion(int id, AsignacionesDTO asignacionDTO)
        {
            if (id != asignacionDTO.ID_Asignacion)
            {
                return BadRequest();
            }

            var asignacion = await _context.Asignacion.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }

            _mapper.Map(asignacionDTO, asignacion);

            _context.Entry(asignacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
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

        // DELETE: api/Asignaciones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion(int id)
        {
            var asignacion = await _context.Asignacion.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }

            _context.Asignacion.Remove(asignacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignacionExists(int id)
        {
            return _context.Asignacion.Any(e => e.ID_Asignacion == id);
        }
    }
}