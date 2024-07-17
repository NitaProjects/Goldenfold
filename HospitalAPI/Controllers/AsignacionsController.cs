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
        public async Task<ActionResult<IEnumerable<AsignacionDTO>>> GetAsignaciones()
        {
            var asignaciones = await _context.Asignacion.ToListAsync();
            var asignacionesDTO = _mapper.Map<IEnumerable<AsignacionDTO>>(asignaciones);
            return Ok(asignacionesDTO);
        }

        // GET: api/Asignaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AsignacionDTO>> GetAsignacion(int id)
        {
            var asignacion = await _context.Asignacion.FindAsync(id);

            if (asignacion == null)
            {
                return NotFound();
            }

            var asignacionDTO = _mapper.Map<AsignacionDTO>(asignacion);
            return Ok(asignacionDTO);
        }

        // POST: api/Asignaciones
        [HttpPost]
        public async Task<ActionResult<AsignacionDTO>> PostAsignacion(AsignacionDTO asignacionDTO)
        {
            var asignacion = _mapper.Map<Asignacion>(asignacionDTO);

            _context.Asignacion.Add(asignacion);
            await _context.SaveChangesAsync();

            asignacionDTO.ID_Asignacion = asignacion.ID_Asignacion;
            return CreatedAtAction("GetAsignacion", new { id = asignacionDTO.ID_Asignacion }, asignacionDTO);
        }

        // PUT: api/Asignaciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion(int id, AsignacionDTO asignacionDTO)
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