using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApi;
using HospitalApi.Models;
using AutoMapper;
using HospitalAPI.DTO;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var usuariosDTO = _mapper.Map<IEnumerable<UsuariosDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDTO = _mapper.Map<UsuariosDTO>(usuario);
            return Ok(usuarioDTO);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuariosDTO>> PostUsuario(UsuariosDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuarios>(usuarioDTO);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuarioDTO.ID_Usuario = usuario.ID_Usuario;
            return CreatedAtAction("GetUsuario", new { id = usuarioDTO.ID_Usuario }, usuarioDTO);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuariosDTO usuarioDTO)
        {
            if (id != usuarioDTO.ID_Usuario)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _mapper.Map(usuarioDTO, usuario);

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID_Usuario == id);
        }
    }
}