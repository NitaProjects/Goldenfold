using HospitalApi.DTO;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HospitalApi.Services
{
    public class IngresoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IngresoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los ingresos con filtros opcionales
        public async Task<IEnumerable<IngresoDTO>> GetIngresosAsync(int? idPaciente, int? idMedico, string? estado, string? tipoCama, DateTime? fechaSolicitudDesde, DateTime? fechaSolicitudHasta)
        {
            IQueryable<Ingreso> query = _context.Ingresos;

            if (idPaciente.HasValue)
                query = query.Where(i => i.IdPaciente == idPaciente.Value);

            if (idMedico.HasValue)
                query = query.Where(i => i.IdMedico == idMedico.Value);

            if (!string.IsNullOrEmpty(estado) && Enum.TryParse(typeof(EstadoIngreso), estado, true, out var estadoEnum))
                query = query.Where(i => i.Estado == (EstadoIngreso)estadoEnum);

            if (!string.IsNullOrEmpty(tipoCama) && Enum.TryParse(typeof(TipoCama), tipoCama, true, out var tipoCamaEnum))
                query = query.Where(i => i.TipoCama == (TipoCama)tipoCamaEnum);

            if (fechaSolicitudDesde.HasValue)
                query = query.Where(i => i.FechaSolicitud >= fechaSolicitudDesde.Value);

            if (fechaSolicitudHasta.HasValue)
                query = query.Where(i => i.FechaSolicitud <= fechaSolicitudHasta.Value);

            var ingresos = await query.ToListAsync();
            return _mapper.Map<IEnumerable<IngresoDTO>>(ingresos);
        }

        // Obtener un ingreso por ID
        public async Task<IngresoDTO?> GetIngresoByIdAsync(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            return ingreso == null ? null : _mapper.Map<IngresoDTO>(ingreso);
        }

        // Crear un nuevo ingreso
        public async Task<IngresoDTO> CreateIngresoAsync(IngresoCreateDTO ingresoDTO)
        {
            if (!await _context.Pacientes.AnyAsync(p => p.IdPaciente == ingresoDTO.IdPaciente))
                throw new ArgumentException("El paciente especificado no existe.");

            if (!await _context.Usuarios.AnyAsync(u => u.IdUsuario == ingresoDTO.IdMedico))
                throw new ArgumentException("El médico especificado no existe.");

            if (!Enum.IsDefined(typeof(TipoCama), ingresoDTO.TipoCama))
                throw new ArgumentException("El tipo de cama proporcionado no es válido.");

            var ingreso = _mapper.Map<Ingreso>(ingresoDTO);
            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();

            var consulta = await _context.Consultas
                .Where(c => c.IdPaciente == ingresoDTO.IdPaciente && c.Estado == EstadoConsulta.pendiente)
                .FirstOrDefaultAsync();

            if (consulta != null)
            {
                consulta.Estado = EstadoConsulta.completada;
                consulta.FechaConsulta = DateTime.Now;
                _context.Consultas.Update(consulta);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IngresoDTO>(ingreso);
        }

        // Actualizar un ingreso existente
        public async Task<bool> UpdateIngresoAsync(int id, IngresoUpdateDTO ingresoDTO)
        {
            var ingresoExiste = await _context.Ingresos.FindAsync(id);
            if (ingresoExiste == null)
                return false;

            if (!Enum.IsDefined(typeof(TipoCama), ingresoDTO.TipoCama))
                throw new ArgumentException("El tipo de cama proporcionado no es válido.");

            if (!Enum.TryParse(ingresoDTO.Estado, out EstadoIngreso nuevoEstado))
                throw new ArgumentException("El estado proporcionado no es válido.");

            // Validación de asignación de camas
            var camaAsignada = await _context.Asignaciones
                .Where(a => a.IdCama == ingresoDTO.IdAsignacion && a.FechaLiberacion == null && a.IdPaciente != ingresoDTO.IdPaciente)
                .FirstOrDefaultAsync();

            if (camaAsignada != null)
                throw new ArgumentException("La cama está ocupada por otro paciente.");

            // Mapear los cambios desde el DTO
            _mapper.Map(ingresoDTO, ingresoExiste);
            ingresoExiste.Estado = nuevoEstado;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un ingreso
        public async Task<bool> DeleteIngresoAsync(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
                return false;

            // Eliminar la asignación relacionada si existe
            if (ingreso.IdAsignacion.HasValue)
            {
                var asignacion = await _context.Asignaciones.FindAsync(ingreso.IdAsignacion.Value);
                if (asignacion != null)
                    _context.Asignaciones.Remove(asignacion);
            }

            _context.Ingresos.Remove(ingreso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
