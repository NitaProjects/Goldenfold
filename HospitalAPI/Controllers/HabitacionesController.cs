using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HabitacionesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Habitaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitacionesDTO>>> GetHabitaciones()
        {
            var habitaciones = await _context.Habitaciones.ToListAsync();
            var habitacionesDTO = _mapper.Map<IEnumerable<HabitacionesDTO>>(habitaciones);
            return Ok(habitacionesDTO);
        }

        // GET: api/Habitaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HabitacionesDTO>> GetHabitacion(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            var habitacionDTO = _mapper.Map<HabitacionesDTO>(habitacion);
            return Ok(habitacionDTO);
        }

        // POST: api/Habitaciones
        [HttpPost]
        public async Task<ActionResult<HabitacionesDTO>> PostHabitacion(HabitacionesDTO habitacionDTO)
        {
            var habitacion = _mapper.Map<Habitaciones>(habitacionDTO);

            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();

            habitacionDTO.ID_Habitacion = habitacion.ID_Habitacion;
            return CreatedAtAction("GetHabitacion", new { id = habitacionDTO.ID_Habitacion }, habitacionDTO);
        }

        // PUT: api/Habitaciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitacion(int id, HabitacionesDTO habitacionDTO)
        {
            if (id != habitacionDTO.ID_Habitacion)
            {
                return BadRequest();
            }

            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }

            _mapper.Map(habitacionDTO, habitacion);

            _context.Entry(habitacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(id))
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

        // DELETE: api/Habitaciones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }

            _context.Habitaciones.Remove(habitacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitaciones.Any(e => e.ID_Habitacion == id);
        }
    }
}