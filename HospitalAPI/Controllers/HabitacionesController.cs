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
        public async Task<ActionResult<IEnumerable<HabitacionDTO>>> GetHabitaciones()
        {
            var habitaciones = await _context.Habitaciones.ToListAsync();
            var habitacionesDTO = _mapper.Map<IEnumerable<HabitacionDTO>>(habitaciones);
            return Ok(habitacionesDTO);
        }

        // GET: api/Habitaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HabitacionDTO>> GetHabitacionById(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            var habitacionDTO = _mapper.Map<HabitacionDTO>(habitacion);
            return Ok(habitacionDTO);
        }

        // GET: api/Habitaciones/ByNum/{NumHabitacion}
        [HttpGet("ByNum/{NumHabitacion}")]
        public async Task<ActionResult<HabitacionDTO>> GetHabitacionByNum(int NumHabitacion)
        {
            var habitacion = await _context.Habitaciones
                .FirstOrDefaultAsync(h => h.NumeroHabitacion == NumHabitacion);

            if (habitacion == null)
            {
                return NotFound("No se ha encontrado ninguna habitación con el número proporcionado.");
            }

            var habitacionDTO = _mapper.Map<HabitacionDTO>(habitacion);
            return Ok(habitacionDTO);
        }


        // POST: api/Habitaciones
        [HttpPost]
        public async Task<ActionResult<HabitacionDTO>> AddHabitacion(HabitacionDTO habitacionDTO)
        {
            var habitacion = _mapper.Map<Habitacion>(habitacionDTO);

            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();

            habitacionDTO.IdHabitacion = habitacion.IdHabitacion;
            return CreatedAtAction("GetHabitacion", new { id = habitacionDTO.IdHabitacion }, habitacionDTO);
        }

        // PUT: api/Habitaciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditHabitacionById(int id, HabitacionDTO habitacionDTO)
        {
            if (id != habitacionDTO.IdHabitacion)
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

        // PUT: api/Habitaciones/ByNum/{NumHabitacion}
        [HttpPut("ByNum/{NumHabitacion}")]
        public async Task<IActionResult> EditHabitacionByNum(int NumHabitacion, HabitacionDTO habitacionDTO)
        {
            // Buscar la habitación actual por su número
            var habitacionExistente = await _context.Habitaciones
                .FirstOrDefaultAsync(h => h.NumeroHabitacion == NumHabitacion);

            if (habitacionExistente == null)
            {
                return NotFound("No se ha encontrado ninguna habitación con el número proporcionado.");
            }

            // Validar que el nuevo número de habitación no esté en uso
            if (habitacionDTO.NumeroHabitacion != NumHabitacion)
            {
                if (await _context.Habitaciones.AnyAsync(h => h.NumeroHabitacion == habitacionDTO.NumeroHabitacion))
                {
                    return Conflict("Ya existe una habitación con el nuevo número proporcionado.");
                }
            }

            _mapper.Map(habitacionDTO, habitacionExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(habitacionExistente.IdHabitacion))
                {
                    return NotFound("No se ha encontrado la habitación especificada.");
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
        public async Task<IActionResult> DeleteHabitacionById(int id)
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

        // DELETE: api/Habitaciones/ByNum/{NumHabitacion}
        [HttpDelete("ByNum/{NumHabitacion}")]
        public async Task<IActionResult> DeleteHabitacionByNum(int NumHabitacion)
        {
            var habitacion = await _context.Habitaciones
                .FirstOrDefaultAsync(h => h.NumeroHabitacion == NumHabitacion);

            if (habitacion == null)
            {
                return NotFound("No se ha encontrado ninguna habitación con el número proporcionado.");
            }

            _context.Habitaciones.Remove(habitacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool HabitacionExists(int id)
        {
            return _context.Habitaciones.Any(e => e.IdHabitacion == id);
        }
    }
}