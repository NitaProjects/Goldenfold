using Microsoft.AspNetCore.Mvc;
using HospitalApi.Services;
using HospitalApi.DTO;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitacionesController : ControllerBase
    {
        private readonly HabitacionService _habitacionService;

        public HabitacionesController(HabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitacionDTO>>> GetHabitaciones(
            [FromQuery] string? edificio, 
            [FromQuery] string? planta, 
            [FromQuery] string? numeroHabitacion)
        {
            var habitaciones = await _habitacionService.GetHabitacionesAsync(edificio, planta, numeroHabitacion);
            return Ok(habitaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HabitacionDTO>> GetHabitacion(int id)
        {
            var habitacion = await _habitacionService.GetHabitacionByIdAsync(id);
            if (habitacion == null)
                return NotFound("No se ha encontrado la habitación con el ID proporcionado.");
            return Ok(habitacion);
        }

        [HttpPost]
        public async Task<ActionResult<HabitacionDTO>> CreateHabitacion(HabitacionCreateDTO habitacionDTO)
        {
            try
            {
                var nuevaHabitacion = await _habitacionService.CreateHabitacionAsync(habitacionDTO);
                return CreatedAtAction(nameof(GetHabitacion), new { id = nuevaHabitacion.IdHabitacion }, nuevaHabitacion);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabitacion(int id, HabitacionUpdateDTO habitacionDTO)
        {
            var result = await _habitacionService.UpdateHabitacionAsync(id, habitacionDTO);
            if (!result)
                return NotFound("No se encontró la habitación con el ID proporcionado.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            var result = await _habitacionService.DeleteHabitacionAsync(id);
            if (!result)
                return NotFound("No se encontró la habitación con el ID proporcionado.");
            return NoContent();
        }
    }
}
