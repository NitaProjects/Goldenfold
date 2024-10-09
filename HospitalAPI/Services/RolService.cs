using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HospitalApi.DTO;

namespace HospitalApi.Services
{
    public class RolService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los roles con filtro opcional por nombre
        public async Task<IEnumerable<RolDTO>> GetRolesAsync(string? nombreRol = null)
        {
            IQueryable<Rol> query = _context.Roles;

            if (!string.IsNullOrEmpty(nombreRol))
            {
                query = query.Where(r => r.NombreRol.ToLower().Contains(nombreRol.ToLower()));
            }

            var roles = await query.ToListAsync();
            return _mapper.Map<IEnumerable<RolDTO>>(roles);
        }

        // Obtener un rol por ID
        public async Task<RolDTO?> GetRolByIdAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            return rol == null ? null : _mapper.Map<RolDTO>(rol);
        }

        // Crear un nuevo rol
        public async Task<RolDTO> CreateRolAsync(RolCreateDTO rolCreateDTO)
        {
            // Verificar si ya existe un rol con el mismo nombre
            if (await _context.Roles.AnyAsync(r => r.NombreRol == rolCreateDTO.NombreRol))
            {
                throw new ArgumentException($"El rol '{rolCreateDTO.NombreRol}' ya existe. Por favor, elige un nombre diferente.");
            }

            var rol = _mapper.Map<Rol>(rolCreateDTO);
            _context.Roles.Add(rol);

            await _context.SaveChangesAsync();

            return _mapper.Map<RolDTO>(rol);
        }

        // Actualizar un rol existente
        public async Task<bool> UpdateRolAsync(int id, RolUpdateDTO rolUpdateDTO)
        {
            var rolExiste = await _context.Roles.FindAsync(id);
            if (rolExiste == null)
                return false;

            // Verificar si ya existe otro rol con el mismo nombre
            if (await _context.Roles.AnyAsync(r => r.NombreRol == rolUpdateDTO.NombreRol && r.IdRol != id))
            {
                throw new ArgumentException($"El rol '{rolUpdateDTO.NombreRol}' ya existe. Por favor, elige un nombre diferente.");
            }

            _mapper.Map(rolUpdateDTO, rolExiste);
            await _context.SaveChangesAsync();

            return true;
        }

        // Eliminar un rol
        public async Task<bool> DeleteRolAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
