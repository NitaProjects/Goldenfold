using HospitalApi.DTO;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class HistorialAltaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HistorialAltaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los historiales de altas con filtros opcionales
        public async Task<IEnumerable<HistorialAltaDTO>> GetHistorialAltasAsync(int? idPaciente, DateTime? fechaAlta, string? diagnostico, string? tratamiento)
        {
            IQueryable<HistorialAlta> query = _context.HistorialesAltas;

            if (idPaciente.HasValue)
                query = query.Where(h => h.IdPaciente == idPaciente);

            if (fechaAlta.HasValue)
                query = query.Where(h => h.FechaAlta.Date == fechaAlta.Value.Date);

            if (!string.IsNullOrEmpty(diagnostico))
                query = query.Where(h => h.Diagnostico.ToLower().Contains(diagnostico.ToLower()));

            if (!string.IsNullOrEmpty(tratamiento))
                query = query.Where(h => h.Tratamiento.ToLower().Contains(tratamiento.ToLower()));

            var historialAltas = await query.ToListAsync();
            return _mapper.Map<IEnumerable<HistorialAltaDTO>>(historialAltas);
        }

        // Obtener un historial de alta por ID
        public async Task<HistorialAltaDTO> GetHistorialAltaByIdAsync(int id)
        {
            var historialAlta = await _context.HistorialesAltas.FindAsync(id);
            if (historialAlta == null)
                return null;

            return _mapper.Map<HistorialAltaDTO>(historialAlta);
        }

        // Crear un nuevo historial de alta
        public async Task<HistorialAltaDTO> CreateHistorialAltaAsync(HistorialAltaCreateDTO historialAltaDTO)
        {
            // Validar la existencia del paciente
            var paciente = await _context.Pacientes.FindAsync(historialAltaDTO.IdPaciente);
            if (paciente == null)
                throw new ArgumentException("El paciente especificado no existe.");

            // Validar la existencia del médico
            var medico = await _context.Usuarios.FindAsync(historialAltaDTO.IdMedico);
            if (medico == null)
                throw new ArgumentException("El médico especificado no existe.");

            // Cambiar el estado del paciente a "Alta"
            paciente.Estado = EstadoPaciente.Alta;

            // Eliminar el ingreso relacionado si existe
            var ingresoRelacionado = await _context.Ingresos
                .FirstOrDefaultAsync(i => i.IdPaciente == historialAltaDTO.IdPaciente);

            if (ingresoRelacionado != null)
            {
                _context.Ingresos.Remove(ingresoRelacionado);
            }

            // Crear el historial de alta
            var historialAlta = _mapper.Map<HistorialAlta>(historialAltaDTO);
            _context.HistorialesAltas.Add(historialAlta);

            // Guardar los cambios
            await _context.SaveChangesAsync();

            // Retornar el historial de alta creado
            return _mapper.Map<HistorialAltaDTO>(historialAlta);
        }


        // Actualizar un historial de alta existente
        public async Task<bool> UpdateHistorialAltaAsync(int id, HistorialAltaUpdateDTO historialAltaDTO)
        {
            var historialAlta = await _context.HistorialesAltas.FindAsync(id);
            if (historialAlta == null)
                return false;

            _mapper.Map(historialAltaDTO, historialAlta);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un historial de alta
        public async Task<bool> DeleteHistorialAltaAsync(int id)
        {
            var historialAlta = await _context.HistorialesAltas.FindAsync(id);
            if (historialAlta == null)
                return false;

            _context.HistorialesAltas.Remove(historialAlta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
