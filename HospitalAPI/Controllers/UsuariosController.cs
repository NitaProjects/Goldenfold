using Microsoft.AspNetCore.Mvc;
using HospitalApi.DTO;
using HospitalApi.Services;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios(
            [FromQuery] string? nombre, 
            [FromQuery] string? nombreUsuario, 
            [FromQuery] int? idRol)
        {
            var usuarios = await _usuarioService.GetUsuariosAsync(nombre, nombreUsuario, idRol);

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound($"No se ha encontrado ningún usuario con el ID {id}.");
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CreateUsuario(UsuarioCreateDTO usuarioDTO)
        {
            try
            {
                var nuevoUsuario = await _usuarioService.CreateUsuarioAsync(usuarioDTO);
                return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.IdUsuario }, nuevoUsuario);
            }
            catch (ArgumentException ex)
            {
                // Devolvemos un conflicto si ya existe el usuario
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioUpdateDTO usuarioDTO)
        {
            try
            {
                var result = await _usuarioService.UpdateUsuarioAsync(id, usuarioDTO);
                if (!result)
                    return NotFound($"No se encontró ningún usuario con el ID {id}.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Devolvemos conflicto si el nombre de usuario está en uso o hay algún problema con los datos
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(id);
            if (!result)
                return NotFound($"No se encontró el usuario con el ID {id}.");
            return NoContent();
        }
    }
}
