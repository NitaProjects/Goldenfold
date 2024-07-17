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
            var historialAltas = await _context.HistorialAlta.ToListAsync();
            var historialAltasDTO = _mapper.Map<IEnumerable<HistorialAltaDTO>>(historialAltas);
            return Ok(historialAltasDTO);
        }

        // GET: api/HistorialAltas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialAltaDTO>> GetHistorialAlta(int id)
        {
            var historialAlta = await _context.HistorialAlta.FindAsync(id);

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

            _context.HistorialAlta.Add(historialAlta);
            await _context.SaveChangesAsync();

            historialAltaDTO.ID_Historial = historialAlta.ID_Historial;
            return CreatedAtAction("GetHistorialAlta", new { id = historialAltaDTO.ID_Historial }, historialAltaDTO);
        }

        // PUT: api/HistorialAltas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialAlta(int id, HistorialAltaDTO historialAltaDTO)
        {
            if (id != historialAltaDTO.ID_Historial)
            {
                return BadRequest();
            }

            var historialAlta = await _context.HistorialAlta.FindAsync(id);
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
            var historialAlta = await _context.HistorialAlta.FindAsync(id);
            if (historialAlta == null)
            {
                return NotFound();
            }

            _context.HistorialAlta.Remove(historialAlta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialAltaExists(int id)
        {
            return _context.HistorialAlta.Any(e => e.ID_Historial == id);
        }
    }
}