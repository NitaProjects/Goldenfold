using HospitalApi.Models;
using HospitalApi.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class HabitacionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HabitacionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener habitaciones con filtros opcionales
        public async Task<IEnumerable<HabitacionDTO>> GetHabitacionesAsync(string? edificio, string? planta, string? numeroHabitacion)
        {
            IQueryable<Habitacion> query = _context.Habitaciones;

            if (!string.IsNullOrEmpty(edificio))
                query = query.Where(h => h.Edificio.Contains(edificio));

            if (!string.IsNullOrEmpty(planta))
                query = query.Where(h => h.Planta.Contains(planta));

            if (!string.IsNullOrEmpty(numeroHabitacion))
                query = query.Where(h => h.NumeroHabitacion.Contains(numeroHabitacion));

            var habitaciones = await query.ToListAsync();
            return _mapper.Map<IEnumerable<HabitacionDTO>>(habitaciones);
        }

        // Obtener una habitación por ID
        public async Task<HabitacionDTO> GetHabitacionByIdAsync(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
                return null;

            return _mapper.Map<HabitacionDTO>(habitacion);
        }

        // Crear una nueva habitación y camas asociadas
        public async Task<HabitacionDTO> CreateHabitacionAsync(HabitacionCreateDTO habitacionDTO)
        {
            var habitacion = _mapper.Map<Habitacion>(habitacionDTO);

            // Verificar duplicados
            if (await _context.Habitaciones.AnyAsync(h =>
                h.Edificio == habitacionDTO.Edificio &&
                h.Planta == habitacionDTO.Planta &&
                h.NumeroHabitacion == habitacionDTO.NumeroHabitacion))
            {
                throw new ArgumentException("Ya existe una habitación con el número proporcionado en este edificio y planta.");
            }

            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();

            // Crear dos camas para la habitación con el tipo de cama seleccionado
            for (int i = 1; i <= 2; i++)
            {
                var cama = new Cama
                {
                    Ubicacion = $"{habitacionDTO.Edificio}{habitacionDTO.Planta}{habitacionDTO.NumeroHabitacion}{i:00}",
                    Estado = EstadoCama.Disponible,
                    Tipo = (TipoCama)Enum.Parse(typeof(TipoCama), habitacionDTO.TipoCama),
                    IdHabitacion = habitacion.IdHabitacion
                };
                _context.Camas.Add(cama);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<HabitacionDTO>(habitacion);
        }

        // Actualizar una habitación existente
        public async Task<bool> UpdateHabitacionAsync(int id, HabitacionUpdateDTO habitacionDTO)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
                return false;

            _mapper.Map(habitacionDTO, habitacion);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar una habitación
        public async Task<bool> DeleteHabitacionAsync(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
                return false;

            var camas = _context.Camas.Where(c => c.IdHabitacion == id);
            _context.Camas.RemoveRange(camas);

            _context.Habitaciones.Remove(habitacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
