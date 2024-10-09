using HospitalApi.DTO;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class AsignacionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AsignacionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todas las asignaciones con filtros opcionales
        public async Task<IEnumerable<AsignacionDTO>> GetAsignacionesAsync(
            int? idPaciente, int? idCama, DateTime? fechaAsignacion, DateTime? fechaLiberacion, int? asignadoPor)
        {
            IQueryable<Asignacion> query = _context.Asignaciones;

            if (idPaciente.HasValue)
                query = query.Where(a => a.IdPaciente == idPaciente.Value);

            if (idCama.HasValue)
                query = query.Where(a => a.IdCama == idCama.Value);

            if (fechaAsignacion.HasValue)
                query = query.Where(a => a.FechaAsignacion.Date == fechaAsignacion.Value.Date);

            if (fechaLiberacion.HasValue)
                query = query.Where(a => a.FechaLiberacion.Value.Date == fechaLiberacion.Value.Date);

            if (asignadoPor.HasValue)
                query = query.Where(a => a.AsignadoPor == asignadoPor.Value);

            var asignaciones = await query.ToListAsync();
            return _mapper.Map<IEnumerable<AsignacionDTO>>(asignaciones);
        }

        // Obtener una asignación por ID
        public async Task<AsignacionDTO?> GetAsignacionByIdAsync(int id)
        {
            var asignacion = await _context.Asignaciones.FindAsync(id);
            return asignacion == null ? null : _mapper.Map<AsignacionDTO>(asignacion);
        }

        public async Task<AsignacionDTO> CreateAsignacionAsync(AsignacionCreateDTO asignacionDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(); // Iniciar transacción

            try
            {
                // Verificar si la cama ya está asignada a otro paciente y no ha sido liberada
                var asignacionExistente = await _context.Asignaciones
                    .Where(a => a.IdCama == asignacionDTO.IdCama && a.FechaLiberacion == null)
                    .FirstOrDefaultAsync();

                if (asignacionExistente != null)
                {
                    throw new InvalidOperationException("La cama ya está asignada a otro paciente y no ha sido liberada.");
                }

                // Crear nueva asignación
                var asignacion = _mapper.Map<Asignacion>(asignacionDTO);
                _context.Asignaciones.Add(asignacion);
                await _context.SaveChangesAsync(); // Guardar la nueva asignación

                // Verificar si el paciente tiene un ingreso pendiente y actualizar su estado
                var ingreso = await _context.Ingresos
                    .FirstOrDefaultAsync(i => i.IdPaciente == asignacionDTO.IdPaciente && i.Estado == EstadoIngreso.Pendiente);

                if (ingreso != null)
                {
                    ingreso.Estado = EstadoIngreso.Ingresado;
                    ingreso.IdAsignacion = asignacion.IdAsignacion;
                    ingreso.FechaIngreso = DateTime.Now;
                    _context.Ingresos.Update(ingreso);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync(); // Confirmar transacción
                return _mapper.Map<AsignacionDTO>(asignacion);
            }
            catch (InvalidOperationException ex)
            {
                // Este bloque captura errores específicos y los reenvía sin cambiarlos
                await transaction.RollbackAsync(); // Revertir transacción en caso de error
                throw; // Lanzar la excepción original
            }
            catch (Exception ex)
            {
                // Para otros errores, lanzar el mensaje genérico
                await transaction.RollbackAsync(); // Revertir transacción en caso de error
                throw new InvalidOperationException("Ocurrió un error inesperado durante la asignación.", ex);
            }
        }
        // Actualizar una asignación existente
        public async Task<bool> UpdateAsignacionAsync(int id, AsignacionUpdateDTO asignacionDTO)
        {
            var asignacionExiste = await _context.Asignaciones.FindAsync(id);
            if (asignacionExiste == null)
                return false;

            _mapper.Map(asignacionDTO, asignacionExiste);

            if (asignacionDTO.FechaLiberacion.HasValue)
            {
                var cama = await _context.Camas.FindAsync(asignacionDTO.IdCama);
                if (cama != null)
                {
                    cama.Estado = EstadoCama.Disponible;
                    _context.Camas.Update(cama);
                }

                var paciente = await _context.Pacientes.FindAsync(asignacionDTO.IdPaciente);
                if (paciente != null)
                {
                    paciente.Estado = EstadoPaciente.Alta;
                    _context.Pacientes.Update(paciente);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar una asignación
        public async Task<bool> DeleteAsignacionAsync(int id)
        {
            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion == null)
                return false;

            _context.Asignaciones.Remove(asignacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
