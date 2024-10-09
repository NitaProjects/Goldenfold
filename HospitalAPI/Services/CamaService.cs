using HospitalApi.Models;
using HospitalApi.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class CamaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CamaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todas las camas con filtros opcionales
        public async Task<IEnumerable<CamaDTO>> GetCamasAsync(string? ubicacion, string? estado, string? tipo)
        {
            IQueryable<Cama> query = _context.Camas;

            if (!string.IsNullOrEmpty(ubicacion))
                query = query.Where(c => c.Ubicacion.ToLower().Contains(ubicacion.ToLower()));

            if (!string.IsNullOrEmpty(estado) && Enum.TryParse(typeof(EstadoCama), estado, true, out var estadoEnum))
                query = query.Where(c => c.Estado == (EstadoCama)estadoEnum);

            if (!string.IsNullOrEmpty(tipo) && Enum.TryParse(typeof(TipoCama), tipo, true, out var tipoEnum))
                query = query.Where(c => c.Tipo == (TipoCama)tipoEnum);

            var camas = await query.ToListAsync();
            return _mapper.Map<IEnumerable<CamaDTO>>(camas);
        }

        // Obtener una cama por ID
        public async Task<CamaDTO> GetCamaByIdAsync(int id)
        {
            var cama = await _context.Camas.FindAsync(id);
            if (cama == null)
                return null;

            return _mapper.Map<CamaDTO>(cama);
        }

        // Crear una nueva cama
        public async Task<CamaDTO> CreateCamaAsync(CamaCreateDTO camaDTO)
        {
            // Verificar si la ubicación ya existe
            if (await _context.Camas.AnyAsync(c => c.Ubicacion == camaDTO.Ubicacion))
                throw new ArgumentException("La ubicación de la cama ya está en uso.");

            var cama = _mapper.Map<Cama>(camaDTO);
            _context.Camas.Add(cama);
            await _context.SaveChangesAsync();

            return _mapper.Map<CamaDTO>(cama);
        }

        // Actualizar una cama existente
        public async Task<bool> UpdateCamaAsync(int id, CamaUpdateDTO camaDTO)
        {
            var camaExiste = await _context.Camas.FindAsync(id);
            if (camaExiste == null)
                return false;

            _mapper.Map(camaDTO, camaExiste);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar una cama
        public async Task<bool> DeleteCamaAsync(int id)
        {
            var cama = await _context.Camas.FindAsync(id);
            if (cama == null)
                return false;

            _context.Camas.Remove(cama);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
