using Microsoft.AspNetCore.Mvc;
using HospitalApi.DTO;
using HospitalApi.Services;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDTO>>> GetPacientes(
            [FromQuery] string? nombre,
            [FromQuery] string? numSS,
            [FromQuery] string? dni,
            [FromQuery] DateTime? fechaNacimiento,
            [FromQuery] EstadoPaciente? estado,
            [FromQuery] DateTime? fechaRegistro,
            [FromQuery] string? direccion,
            [FromQuery] string? telefono,
            [FromQuery] string? email)
        {
            var pacientes = await _pacienteService.GetPacientesAsync(nombre, numSS, dni, fechaNacimiento, estado, fechaRegistro, direccion, telefono, email);


            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
                return NotFound($"No se ha encontrado ningún paciente con el ID {id}.");

            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteDTO>> CreatePaciente(PacienteCreateDTO pacienteDTO)
        {
            try
            {
                var nuevoPaciente = await _pacienteService.CreatePacienteAsync(pacienteDTO);
                return CreatedAtAction(nameof(GetPaciente), new { id = nuevoPaciente.IdPaciente }, nuevoPaciente);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, PacienteUpdateDTO pacienteDTO)
        {
            try
            {
                var result = await _pacienteService.UpdatePacienteAsync(id, pacienteDTO);
                if (!result)
                    return NotFound("No se encontró el paciente especificado.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var result = await _pacienteService.DeletePacienteAsync(id);
            if (!result)
                return NotFound("No se encontró el paciente especificado.");
            return NoContent();
        }
    }
}
