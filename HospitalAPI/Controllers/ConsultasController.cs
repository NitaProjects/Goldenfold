using Microsoft.AspNetCore.Mvc;
using HospitalApi.DTO;
using HospitalApi.Services;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly ConsultaService _consultaService;

        public ConsultasController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultaDTO>>> GetConsultas(
            [FromQuery] int? idPaciente,
            [FromQuery] int? idMedico,
            [FromQuery] string? motivo,
            [FromQuery] DateTime? fechaSolicitud,
            [FromQuery] DateTime? fechaConsulta,
            [FromQuery] string? estado)
        {
            var consultas = await _consultaService.GetConsultasAsync(idPaciente, idMedico, motivo, fechaSolicitud, fechaConsulta, estado);

            return Ok(consultas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaDTO>> GetConsultaById(int id)
        {
            var consulta = await _consultaService.GetConsultaByIdAsync(id);
            if (consulta == null)
                return NotFound($"No se ha encontrado ninguna consulta con el ID {id}.");

            return Ok(consulta);
        }

        [HttpPost]
        public async Task<ActionResult<ConsultaDTO>> CreateConsulta(ConsultaCreateDTO consultaDTO)
        {
            try
            {
                var nuevaConsulta = await _consultaService.CreateConsultaAsync(consultaDTO);
                return CreatedAtAction(nameof(GetConsultaById), new { id = nuevaConsulta.IdConsulta }, nuevaConsulta);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsulta(int id, ConsultaUpdateDTO consultaDTO)
        {
            try
            {
                var result = await _consultaService.UpdateConsultaAsync(id, consultaDTO);
                if (!result)
                    return NotFound("No se encontró ninguna consulta con el ID proporcionado.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var result = await _consultaService.DeleteConsultaAsync(id);
            if (!result)
                return NotFound("No se encontró la consulta con el ID proporcionado.");
            return NoContent();
        }
    }
}
