using Microsoft.AspNetCore.Mvc;
using HospitalApi.DTO;
using HospitalApi.Services;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialAltasController : ControllerBase
    {
        private readonly HistorialAltaService _historialAltaService;

        public HistorialAltasController(HistorialAltaService historialAltaService)
        {
            _historialAltaService = historialAltaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialAltaDTO>>> GetHistorialAltas(
            [FromQuery] int? idPaciente, 
            [FromQuery] DateTime? fechaAlta, 
            [FromQuery] string? diagnostico, 
            [FromQuery] string? tratamiento)
        {
            var historialAltas = await _historialAltaService.GetHistorialAltasAsync(idPaciente, fechaAlta, diagnostico, tratamiento);

            return Ok(historialAltas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialAltaDTO>> GetHistorialAltaById(int id)
        {
            var historialAlta = await _historialAltaService.GetHistorialAltaByIdAsync(id);
            if (historialAlta == null)
                return NotFound($"No se ha encontrado ningún historial de alta con el ID {id}.");

            return Ok(historialAlta);
        }

        [HttpPost]
        public async Task<ActionResult<HistorialAltaDTO>> CreateHistorialAlta(HistorialAltaCreateDTO historialAltaDTO)
        {
            try
            {
                var nuevoHistorialAlta = await _historialAltaService.CreateHistorialAltaAsync(historialAltaDTO);
                return CreatedAtAction(nameof(GetHistorialAltaById), new { id = nuevoHistorialAlta.IdHistorial }, nuevoHistorialAlta);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistorialAlta(int id, HistorialAltaUpdateDTO historialAltaDTO)
        {
            try
            {
                var result = await _historialAltaService.UpdateHistorialAltaAsync(id, historialAltaDTO);
                if (!result)
                    return NotFound("No se encontró el historial de alta con el ID proporcionado.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialAlta(int id)
        {
            var result = await _historialAltaService.DeleteHistorialAltaAsync(id);
            if (!result)
                return NotFound("No se encontró el historial de alta con el ID proporcionado.");
            return NoContent();
        }
    }
}
