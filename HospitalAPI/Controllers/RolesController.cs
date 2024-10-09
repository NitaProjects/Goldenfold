using HospitalApi.Services;
using HospitalApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolService _rolService;
        public RolesController(RolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetRoles([FromQuery] string? nombreRol)
        {
            var roles = await _rolService.GetRolesAsync(nombreRol);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetRol(int id)
        {
            try
            {
                var rol = await _rolService.GetRolByIdAsync(id);
                return Ok(rol);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RolDTO>> CreateRol(RolCreateDTO rolCreateDTO)
        {
            try
            {
                var rol = await _rolService.CreateRolAsync(rolCreateDTO);
                return CreatedAtAction(nameof(GetRol), new { id = rol.IdRol }, rol);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, RolUpdateDTO rolUpdateDTO)
        {
            try
            {
                await _rolService.UpdateRolAsync(id, rolUpdateDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            try
            {
                await _rolService.DeleteRolAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
