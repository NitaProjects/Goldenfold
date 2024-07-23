using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;
using HospitalApi.DTO;
using AutoMapper;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PacientesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacientesDTO>>> GetPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            var pacientesDTO = _mapper.Map<IEnumerable<PacientesDTO>>(pacientes);
            return Ok(pacientesDTO);
        }

        // GET: api/Pacientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PacientesDTO>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            var pacienteDTO = _mapper.Map<PacientesDTO>(paciente);
            return Ok(pacienteDTO);
        }

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<PacientesDTO>> PostPaciente(PacientesDTO pacienteDTO)
        {
            var paciente = _mapper.Map<Paciente>(pacienteDTO);

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            pacienteDTO.ID_Paciente = paciente.ID_Paciente;
            return CreatedAtAction("GetPaciente", new { id = pacienteDTO.ID_Paciente }, pacienteDTO);
        }

        // PUT: api/Pacientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacientesDTO pacienteDTO)
        {
            if (id != pacienteDTO.ID_Paciente)
            {
                return BadRequest();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _mapper.Map(pacienteDTO, paciente);

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // DELETE: api/Pacientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID_Paciente == id);
        }
    }
}