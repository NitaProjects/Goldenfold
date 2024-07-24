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
        public async Task<ActionResult<HistorialAltaDTO>> PostHistorialAlta(HistorialAltaDTO historialAltaDTO)
        {
            var historialAlta = _mapper.Map<HistorialAlta>(historialAltaDTO);

            _context.HistorialesAltas.Add(historialAlta);
            await _context.SaveChangesAsync();

            historialAltaDTO.IdHistorial = historialAlta.IdHistorial;
            return CreatedAtAction("GetHistorialAlta", new { id = historialAltaDTO.IdHistorial }, historialAltaDTO);
        }

        // PUT: api/HistorialAltas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialAlta(int id, HistorialAltaDTO historialAltaDTO)
        {
            if (id != historialAltaDTO.IdHistorial)
            {
                return BadRequest();
            }

            var historialAlta = await _context.HistorialesAltas.FindAsync(id);
            if (historialAlta == null)
            {
                return NotFound();
            }

            _mapper.Map(historialAltaDTO, historialAlta);

            _context.Entry(historialAlta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialAltaExists(id))
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