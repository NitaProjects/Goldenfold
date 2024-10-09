using HospitalApi.DTO;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class ConsultaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConsultaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todas las consultas con filtros opcionales
        public async Task<IEnumerable<ConsultaDTO>> GetConsultasAsync(
            int? idPaciente, 
            int? idMedico, 
            string? motivo, 
            DateTime? fechaSolicitud, 
            DateTime? fechaConsulta, 
            string? estado)
        {
            IQueryable<Consulta> query = _context.Consultas;

            if (idPaciente.HasValue)
                query = query.Where(c => c.IdPaciente == idPaciente.Value);

            if (idMedico.HasValue)
                query = query.Where(c => c.IdMedico == idMedico.Value);

            if (!string.IsNullOrEmpty(motivo))
                query = query.Where(c => c.Motivo.ToLower().Contains(motivo.ToLower()));

            if (fechaSolicitud.HasValue)
                query = query.Where(c => c.FechaSolicitud.Date == fechaSolicitud.Value.Date);

            if (fechaConsulta.HasValue)
                query = query.Where(c => c.FechaConsulta.HasValue && c.FechaConsulta.Value.Date == fechaConsulta.Value.Date);

            if (!string.IsNullOrEmpty(estado))
            {
                if (Enum.TryParse(typeof(EstadoConsulta), estado, true, out var estadoEnum))
                {
                    query = query.Where(c => c.Estado == (EstadoConsulta)estadoEnum);
                }
                else
                {
                    throw new ArgumentException("El valor de estado no es válido.");
                }
            }

            var consultas = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ConsultaDTO>>(consultas);
        }

        // Obtener una consulta por ID
        public async Task<ConsultaDTO?> GetConsultaByIdAsync(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            return consulta == null ? null : _mapper.Map<ConsultaDTO>(consulta);
        }

        // Crear una nueva consulta
        public async Task<ConsultaDTO> CreateConsultaAsync(ConsultaCreateDTO consultaDTO)
        {
            var paciente = await _context.Pacientes.FindAsync(consultaDTO.IdPaciente);
            if (paciente == null)
                throw new ArgumentException("El paciente especificado no existe.");

            paciente.Estado = EstadoPaciente.EnConsulta;

            var consulta = _mapper.Map<Consulta>(consultaDTO);
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            return _mapper.Map<ConsultaDTO>(consulta);
        }

        // Actualizar una consulta existente
        public async Task<bool> UpdateConsultaAsync(int id, ConsultaUpdateDTO consultaDTO)
        {
            var consultaExiste = await _context.Consultas.FindAsync(id);
            if (consultaExiste == null)
                return false;

            _mapper.Map(consultaDTO, consultaExiste);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar una consulta
        public async Task<bool> DeleteConsultaAsync(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
                return false;

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
