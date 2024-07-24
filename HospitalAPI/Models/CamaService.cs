using HospitalApi.Models;
using HospitalApi;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class CamaService
    {
        private readonly ApplicationDbContext _context;

        public CamaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarUbicacionAsync(int idHabitacion)
        {
            var habitacion = await _context.Habitaciones.FindAsync(idHabitacion);
            if (habitacion == null)
            {
                throw new ArgumentException("Habitación no encontrada.");
            }

            var ubicacionBase = $"{habitacion.Edificio}{habitacion.Planta}{habitacion.NumeroHabitacion}";
            var camasExistentes = await _context.Camas
                .Where(c => c.Ubicacion.StartsWith(ubicacionBase))
                .ToListAsync();

            var numeroCama = camasExistentes.Count + 1;
            return $"{ubicacionBase}{numeroCama}";
        }

        public async Task CrearCamaAsync(Cama cama, int idHabitacion)
        {
            cama.Ubicacion = await GenerarUbicacionAsync(idHabitacion);
            _context.Camas.Add(cama);
            await _context.SaveChangesAsync();
        }
    }

}
