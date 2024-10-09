using Microsoft.AspNetCore.Mvc;
using HospitalApi.DTO;
using HospitalApi.Services;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CamasController : ControllerBase
    {
        private readonly CamaService _camaService;

        public CamasController(CamaService camaService)
        {
            _camaService = camaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CamaDTO>>> GetCamas(
            [FromQuery] string? ubicacion, 
            [FromQuery] string? estado, 
            [FromQuery] string? tipo)
        {
            var camas = await _camaService.GetCamasAsync(ubicacion, estado, tipo);
            return Ok(camas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CamaDTO>> GetCamaById(int id)
        {
            var cama = await _camaService.GetCamaByIdAsync(id);
            return Ok(cama);
        }

        [HttpPost]
        public async Task<ActionResult<CamaDTO>> CreateCama(CamaCreateDTO camaDTO)
        {
            try
            {
                var nuevaCama = await _camaService.CreateCamaAsync(camaDTO);
                return CreatedAtAction(nameof(GetCamaById), new { idCama = nuevaCama.IdCama }, nuevaCama);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCama(int id, CamaUpdateDTO camaDTO)
        {
            try
            {
                var result = await _camaService.UpdateCamaAsync(id, camaDTO);
                if (!result)
                    return NotFound("No se ha encontrado la cama con el ID proporcionado.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCama(int id)
        {
            var result = await _camaService.DeleteCamaAsync(id);
            if (!result)
                return NotFound("No se ha encontrado la cama con el ID proporcionado.");
            return NoContent();
        }
    }
}
