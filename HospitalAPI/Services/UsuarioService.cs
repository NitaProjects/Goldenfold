using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HospitalApi.DTO;

namespace HospitalApi.Services
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los usuarios con filtros opcionales
        public async Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync(string? nombre, string? nombreUsuario, int? idRol)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(u => u.Nombre.ToLower().Contains(nombre.ToLower()));

            if (!string.IsNullOrEmpty(nombreUsuario))
                query = query.Where(u => u.NombreUsuario.ToLower().Contains(nombreUsuario.ToLower()));

            if (idRol.HasValue)
                query = query.Where(u => u.IdRol == idRol.Value);

            var usuarios = await query.ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        // Obtener un usuario por ID
        public async Task<UsuarioDTO?> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario == null ? null : _mapper.Map<UsuarioDTO>(usuario);
        }

        // Crear un nuevo usuario
        public async Task<UsuarioDTO> CreateUsuarioAsync(UsuarioCreateDTO usuarioDTO)
        {
            // Verificar si el nombre de usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario))
            {
                throw new ArgumentException($"El nombre de usuario '{usuarioDTO.NombreUsuario}' ya está en uso.");
            }

            // Verificar si el rol especificado existe
            if (!await _context.Roles.AnyAsync(r => r.IdRol == usuarioDTO.IdRol))
            {
                throw new ArgumentException("El rol proporcionado no existe.");
            }

            usuarioDTO.Contrasenya = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contrasenya); // Hashear la contraseña

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        // Actualizar un usuario existente
        public async Task<bool> UpdateUsuarioAsync(int id, UsuarioUpdateDTO usuarioDTO)
        {
            var usuarioExiste = await _context.Usuarios.FindAsync(id);
            if (usuarioExiste == null)
                return false;

            // Verificar si el nombre de usuario ya existe (excluyendo el actual)
            if (await _context.Usuarios.AnyAsync(u => u.IdUsuario != id && u.NombreUsuario == usuarioDTO.NombreUsuario))
            {
                throw new ArgumentException($"El nombre de usuario '{usuarioDTO.NombreUsuario}' ya está en uso.");
            }

            // Verificar si el rol especificado existe
            if (!await _context.Roles.AnyAsync(r => r.IdRol == usuarioDTO.IdRol))
            {
                throw new ArgumentException("El rol proporcionado no existe.");
            }

            _mapper.Map(usuarioDTO, usuarioExiste);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un usuario
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
