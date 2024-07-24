using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialAltasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HistorialAltasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HistorialAltas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialAltaDTO>>> GetHistorialAltas()
        {
            var historialAltas = await _context.HistorialesAltas.ToListAsync();
            var historialAltasDTO = _mapper.Map<IEnumerable<HistorialAltaDTO>>(historialAltas);
            return Ok(historialAltasDTO);
        }

        // GET: api/HistorialAltas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialAltaDTO>> GetHistorialAlta(int id)
        {
            var historialAlta = await _context.HistorialesAltas.FindAsync(id);

            if (historialAlta == null)
            {
                return NotFound();
            }

            var historialAltaDTO = _mapper.Map<HistorialAltaDTO>(historialAlta);
            return Ok(historialAltaDTO);
        }

        // POST: api/HistorialAltas
        [HttpPost]
        public async Task<ActionResult<HistorialAltaDTO>> AddHistorialDeAltas(HistorialAltaDTO historialDTO)
        {
            // Verificar si el paciente con el IdPaciente proporcionado existe
            var pacienteExistente = await _context.Pacientes
                .FindAsync(historialDTO.IdPaciente);

            if (pacienteExistente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con el ID proporcionado.");
            }

            // Verificar si ya existe un historial de altas para el mismo IdPaciente
            bool historialExistente = await _context.HistorialesAltas
                .AnyAsync(h => h.IdPaciente == historialDTO.IdPaciente);

            if (historialExistente)
            {
                return Conflict("Ya existe un historial de altas para el paciente con el ID proporcionado.");
            }

            var historial = _mapper.Map<HistorialAlta>(historialDTO);

            _context.HistorialesAltas.Add(historial);
            await _context.SaveChangesAsync();

            historialDTO.IdHistorial = historial.IdHistorial;
            return CreatedAtAction(nameof(GetHistorialAlta), new { id = historialDTO.IdHistorial }, historialDTO);
        }

        // PUT: api/HistorialAltas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditHistorialAltas(int id, HistorialAltaDTO historialDTO)
        {
            if (id != historialDTO.IdHistorial)
            {
                return BadRequest("El ID del historial proporcionado no coincide con el ID en la solicitud.");
            }

            // Verificar si el paciente con el IdPaciente proporcionado existe
            var pacienteExistente = await _context.Pacientes
                .FindAsync(historialDTO.IdPaciente);

            if (pacienteExistente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con el ID proporcionado.");
            }

            // Verificar si el historial de altas a actualizar existe
            var historialExistente = await _context.HistorialesAltas
                .FindAsync(id);

            if (historialExistente == null)
            {
                return NotFound("No se ha encontrado ningún historial de altas con el ID proporcionado.");
            }

            // Verificar si ya existe un historial de altas para el mismo IdPaciente (excepto el actual)
            bool historialDuplicado = await _context.HistorialesAltas
                .AnyAsync(h => h.IdPaciente == historialDTO.IdPaciente && h.IdHistorial != id);

            if (historialDuplicado)
            {
                return Conflict("Ya existe un historial de altas para el paciente con el ID proporcionado.");
            }

            // Mapear las propiedades del DTO al historial existente
            _mapper.Map(historialDTO, historialExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialDeAltasExists(id))
                {
                    return NotFound("No se ha encontrado ningún historial de altas con el ID proporcionado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool HistorialDeAltasExists(int id)
        {
            return _context.HistorialesAltas.Any(e => e.IdHistorial == id);
        }


        // DELETE: api/HistorialAltas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialAlta(int id)
        {
            var historialAlta = await _context.HistorialesAltas.FindAsync(id);
            if (historialAlta == null)
            {
                return NotFound();
            }

            _context.HistorialesAltas.Remove(historialAlta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialAltaExists(int id)
        {
            return _context.HistorialesAltas.Any(e => e.IdHistorial == id);
        }
    }
}