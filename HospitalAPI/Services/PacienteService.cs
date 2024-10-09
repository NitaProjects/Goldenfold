using HospitalApi.DTO;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class PacienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PacienteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los pacientes con filtros opcionales
        public async Task<IEnumerable<PacienteDTO>> GetPacientesAsync(
            string? nombre,
            string? numSS,
            string? dni,
            DateTime? fechaNacimiento,
            EstadoPaciente? estado,
            DateTime? fechaRegistro,
            string? direccion,
            string? telefono,
            string? email)
        {
            IQueryable<Paciente> query = _context.Pacientes;

            // Aplicar filtros opcionales
            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower()));

            if (!string.IsNullOrEmpty(numSS))
                query = query.Where(p => p.SeguridadSocial == numSS);

            if (!string.IsNullOrEmpty(dni))
                query = query.Where(p => p.Dni == dni);

            if (fechaNacimiento.HasValue)
                query = query.Where(p => p.FechaNacimiento.Date == fechaNacimiento.Value.Date);

            if (estado.HasValue)
                query = query.Where(p => p.Estado == estado.Value);

            if (fechaRegistro.HasValue)
                query = query.Where(p => p.FechaRegistro.Date == fechaRegistro.Value.Date);

            if (!string.IsNullOrEmpty(direccion))
                query = query.Where(p => p.Direccion.ToLower().Contains(direccion.ToLower()));

            if (!string.IsNullOrEmpty(telefono))
                query = query.Where(p => p.Telefono == telefono);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(p => p.Email.ToLower() == email.ToLower());

            var pacientes = await query.ToListAsync();
            return _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);
        }

        // Obtener un paciente por ID
        public async Task<PacienteDTO?> GetPacienteByIdAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            return paciente == null ? null : _mapper.Map<PacienteDTO>(paciente);
        }

        // Crear un nuevo paciente
        public async Task<PacienteDTO> CreatePacienteAsync(PacienteCreateDTO pacienteDTO)
        {
            if (await _context.Pacientes.AnyAsync(p => p.SeguridadSocial == pacienteDTO.SeguridadSocial))
                throw new ArgumentException("Ya existe un paciente con el número de seguridad social proporcionado.");

            if (await _context.Pacientes.AnyAsync(p => p.Dni == pacienteDTO.Dni))
                throw new ArgumentException("Ya existe un paciente con el DNI proporcionado.");

            var paciente = _mapper.Map<Paciente>(pacienteDTO);
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return _mapper.Map<PacienteDTO>(paciente);
        }

        // Actualizar un paciente existente
        public async Task<bool> UpdatePacienteAsync(int id, PacienteUpdateDTO pacienteDTO)
        {
            var pacienteExiste = await _context.Pacientes.FindAsync(id);
            if (pacienteExiste == null)
                return false;

            if (await _context.Pacientes.AnyAsync(p => p.SeguridadSocial == pacienteDTO.SeguridadSocial && p.IdPaciente != id))
                throw new ArgumentException("Ya existe un paciente con el número de seguridad social proporcionado.");

            if (await _context.Pacientes.AnyAsync(p => p.Dni == pacienteDTO.Dni && p.IdPaciente != id))
                throw new ArgumentException("Ya existe un paciente con el DNI proporcionado.");

            _mapper.Map(pacienteDTO, pacienteExiste);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un paciente
        public async Task<bool> DeletePacienteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return false;

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
