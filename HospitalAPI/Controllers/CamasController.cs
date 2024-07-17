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
    public class CamasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CamasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Camas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CamaDTO>>> GetCamas()
        {
            var camas = await _context.Cama.ToListAsync();
            var camasDTO = _mapper.Map<IEnumerable<CamaDTO>>(camas);
            return Ok(camasDTO);
        }

        // GET: api/Camas/{ubicacion}
        [HttpGet("{ubicacion}")]
        public async Task<ActionResult<CamaDTO>> GetCama(string ubicacion)
        {
            var cama = await _context.Cama.FindAsync(ubicacion);

            if (cama == null)
            {
                return NotFound();
            }

            var camaDTO = _mapper.Map<CamaDTO>(cama);
            return Ok(camaDTO);
        }

        // POST: api/Camas
        [HttpPost]
        public async Task<ActionResult<CamaDTO>> PostCama(CamaDTO camaDTO)
        {
            var cama = _mapper.Map<Cama>(camaDTO);

            _context.Cama.Add(cama);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCama", new { ubicacion = cama.Ubicacion }, camaDTO);
        }

        // PUT: api/Camas/{ubicacion}
        [HttpPut("{ubicacion}")]
        public async Task<IActionResult> PutCama(string ubicacion, CamaDTO camaDTO)
        {
            if (ubicacion != camaDTO.Ubicacion)
            {
                return BadRequest();
            }

            var cama = await _context.Cama.FindAsync(ubicacion);
            if (cama == null)
            {
                return NotFound();
            }

            _mapper.Map(camaDTO, cama);

            _context.Entry(cama).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamaExists(ubicacion))
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

        // DELETE: api/Camas/{ubicacion}
        [HttpDelete("{ubicacion}")]
        public async Task<IActionResult> DeleteCama(string ubicacion)
        {
            var cama = await _context.Cama.FindAsync(ubicacion);
            if (cama == null)
            {
                return NotFound();
            }

            _context.Cama.Remove(cama);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CamaExists(string ubicacion)
        {
            return _context.Cama.Any(e => e.Ubicacion == ubicacion);
        }
    }
}