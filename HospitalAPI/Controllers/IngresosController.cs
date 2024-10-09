using Microsoft.AspNetCore.Mvc;
using HospitalApi.DTO;
using HospitalApi.Services;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngresosController : ControllerBase
    {
        private readonly IngresoService _ingresoService;

        public IngresosController(IngresoService ingresoService)
        {
            _ingresoService = ingresoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngresoDTO>>> GetIngresos(
            [FromQuery] int? idPaciente,
            [FromQuery] int? idMedico,
            [FromQuery] string? estado,
            [FromQuery] string? tipoCama,
            [FromQuery] DateTime? fechaSolicitudDesde,
            [FromQuery] DateTime? fechaSolicitudHasta)
        {
            var ingresos = await _ingresoService.GetIngresosAsync(idPaciente, idMedico, estado, tipoCama, fechaSolicitudDesde, fechaSolicitudHasta);
            return Ok(ingresos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngresoDTO>> GetIngreso(int id)
        {
            var ingreso = await _ingresoService.GetIngresoByIdAsync(id);
            if (ingreso == null)
                return NotFound($"No se encontró ningún ingreso con el ID {id}.");
            return Ok(ingreso);
        }

        [HttpPost]
        public async Task<ActionResult<IngresoDTO>> CreateIngreso(IngresoCreateDTO ingresoDTO)
        {
            try
            {
                var nuevoIngreso = await _ingresoService.CreateIngresoAsync(ingresoDTO);
                return CreatedAtAction(nameof(GetIngreso), new { id = nuevoIngreso.IdIngreso }, nuevoIngreso);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngreso(int id, IngresoUpdateDTO ingresoDTO)
        {
            if (id != ingresoDTO.IdIngreso)
                return BadRequest("El ID proporcionado no coincide con el ID del ingreso.");

            try
            {
                var result = await _ingresoService.UpdateIngresoAsync(id, ingresoDTO);
                if (!result)
                    return NotFound($"No se encontró ningún ingreso con el ID {id}.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            var result = await _ingresoService.DeleteIngresoAsync(id);
            if (!result)
                return NotFound($"No se encontró ningún ingreso con el ID {id}.");
            return NoContent();
        }
    }
}
