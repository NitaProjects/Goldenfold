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

        // GET: api/Pacientes/ByNumSS/{numeroSeguridadSocial}
        [HttpGet("ByNumSS/{numeroSeguridadSocial}")]
        public async Task<ActionResult<PacienteDTO>> GetPacienteByNumSS(string numeroSeguridadSocial)
        {
            // Busca el paciente con el número de seguridad social proporcionado
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.SeguridadSocial == numeroSeguridadSocial);

            if (paciente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con el número de seguridad social proporcionado.");
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
            // Verificar si ya existe un paciente con el mismo número de seguridad social
            if (await _context.Pacientes.AnyAsync(p => p.SeguridadSocial == pacienteDTO.SeguridadSocial))
            {
                return Conflict("Ya existe un paciente con el número de seguridad social proporcionado.");
            }

            var paciente = _mapper.Map<Paciente>(pacienteDTO);

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            pacienteDTO.IdPaciente = paciente.IdPaciente;

            return CreatedAtAction(nameof(GetPacienteByNumSS), new { numeroSeguridadSocial = pacienteDTO.SeguridadSocial }, pacienteDTO);
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

        // PUT: api/Pacientes/ByNumSS/{numeroSeguridadSocial}
        [HttpPut("ByNumSS/{numeroSeguridadSocial}")]
        public async Task<IActionResult> EditPacienteByNumSS(string numeroSeguridadSocial, PacienteDTO pacienteDTO)
        {
            // Verificar si el número de seguridad social en la URL coincide con el del DTO
            if (numeroSeguridadSocial != pacienteDTO.SeguridadSocial)
            {
                return BadRequest("El número de seguridad social proporcionado no coincide con el número en la solicitud.");
            }

            // Buscar el paciente por su número de seguridad social
            var pacienteExistente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.SeguridadSocial == numeroSeguridadSocial);

            if (pacienteExistente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con el número de seguridad social proporcionado.");
            }

            // Validar si el estado proporcionado en el DTO es válido
            if (!Enum.IsDefined(typeof(EstadoPaciente), pacienteDTO.Estado))
            {
                return BadRequest("El estado del paciente proporcionado no es válido.");
            }

            // Verificar si hay otro paciente con el mismo número de seguridad social (excepto el actual)
            if (await _context.Pacientes.AnyAsync(p => p.SeguridadSocial != numeroSeguridadSocial && p.SeguridadSocial == pacienteDTO.SeguridadSocial))
            {
                return Conflict("Ya existe un paciente con el número de seguridad social proporcionado.");
            }

            // Mapear los datos del DTO al paciente existente
            _mapper.Map(pacienteDTO, pacienteExistente);

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(numeroSeguridadSocial))
                {
                    return NotFound("No se ha encontrado ningún paciente con el número de seguridad social proporcionado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Pacientes/ByName/{nombre}
        [HttpPut("ByName/{nombre}")]
        public async Task<IActionResult> EditPacienteByName(string nombre, PacienteDTO pacienteDTO)
        {
            // Buscar el paciente por su nombre
            var pacienteExistente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Nombre == nombre);

            if (pacienteExistente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con el nombre proporcionado.");
            }

            // Validar que el estado proporcionado es un valor válido del enum
            if (!Enum.IsDefined(typeof(EstadoPaciente), pacienteDTO.Estado))
            {
                return BadRequest("El estado proporcionado no es válido.");
            }

            // Verificar si hay otro paciente con el mismo número de seguridad social (si lo has cambiado)
            if (await _context.Pacientes.AnyAsync(p => p.SeguridadSocial == pacienteDTO.SeguridadSocial && p.IdPaciente != pacienteExistente.IdPaciente))
            {
                return Conflict("Ya existe un paciente con el mismo número de seguridad social.");
            }

            // Mapear los cambios del DTO al paciente existente
            _mapper.Map(pacienteDTO, pacienteExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(pacienteExistente.IdPaciente))
                {
                    return NotFound("No se ha encontrado el paciente especificado.");
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

        // DELETE: api/Pacientes/ByNumSS/{numeroSeguridadSocial}
        [HttpDelete("ByNumSS/{numeroSeguridadSocial}")]
        public async Task<IActionResult> DeletePacienteByNumSS(string numeroSeguridadSocial)
        {
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.SeguridadSocial == numeroSeguridadSocial);

            if (paciente == null)
            {
                return NotFound("No se ha encontrado ningún paciente con el número de seguridad social proporcionado.");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Pacientes/ByName/{nombre}
        [HttpDelete("ByName/{nombre}")]
        public async Task<IActionResult> DeletePacienteByName(string nombre)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Nombre == nombre);

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

        private bool PacienteExists(string numeroSeguridadSocial)
        {
            return _context.Pacientes.Any(p => p.SeguridadSocial == numeroSeguridadSocial);
        }
    }
}
