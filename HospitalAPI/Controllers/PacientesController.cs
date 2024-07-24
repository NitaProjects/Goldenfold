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
        public async Task<ActionResult<IEnumerable<PacienteDTO>>> GetPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();

            if (!pacientes.Any())
            {
                return NotFound("No se han encontrado pacientes.");
            }

            var pacientesDTO = _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);

            return Ok(pacientesDTO);
        }

        // GET: api/Pacientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPacienteById(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con este id.");
            }

            var pacienteDTO = _mapper.Map<PacienteDTO>(paciente);
            return Ok(pacienteDTO);
        }

        // GET: api/Pacientes/ByName/{nombre}
        [HttpGet("ByName/{nombre}")]
        public async Task<ActionResult<IEnumerable<PacienteDTO>>> GetPacienteByName(string nombre)
        {
            var pacientes = await _context.Pacientes
                .Where(p => p.Nombre.Contains(nombre))
                .ToListAsync();

            if (!pacientes.Any())
            {
                return NotFound("No se ha encontrado ningún paciente con este nombre.");
            }

            var pacientesDTO = _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);
            return Ok(pacientesDTO);
        }

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<PacienteDTO>> AddPaciente(PacienteDTO pacienteDTO)
        {
            var paciente = _mapper.Map<Paciente>(pacienteDTO);

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            pacienteDTO.IdPaciente = paciente.IdPaciente;
            return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteDTO.IdPaciente }, pacienteDTO);
        }

        // PUT: api/Pacientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPacienteById(int id, PacienteDTO pacienteDTO)
        {
            if (id != pacienteDTO.IdPaciente)
            {
                return BadRequest("El ID del paciente proporcionado no coincide con el ID en la solicitud.");
            }

            var pacienteExistente = await _context.Pacientes.FindAsync(id);

            if (pacienteExistente == null)
            {
                return NotFound("No se encontró el paciente especificado.");
            }

            _mapper.Map(pacienteDTO, pacienteExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
                {
                    return NotFound("No se encontró el paciente especificado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Pacientes/ByName/{name}
        [HttpPut("ByName/{name}")]
        public async Task<IActionResult> EditPacienteByName(string name, PacienteDTO pacienteDTO)
        {
            var pacienteExistente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Nombre == name);

            if (pacienteExistente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con ese nombre.");
            }

            _mapper.Map(pacienteDTO, pacienteExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el paciente.");
            }

            return NoContent();
        }

        // DELETE: api/Pacientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacienteById(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound("No se encontró el paciente especificado.");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pacientes/ByName/{name}
        [HttpDelete("ByName/{name}")]
        public async Task<IActionResult> DeletePacienteByName(string name)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Nombre == name);

            if (paciente == null)
            {
                return NotFound("No se ha encontrado este paciente. Asegúrate de que el nombre es correcto.");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.IdPaciente == id);
        }
    }
}
