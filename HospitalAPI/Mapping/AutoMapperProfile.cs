using AutoMapper;
using HospitalApi.Models;
using HospitalApi.DTO;

namespace HospitalApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Paciente mappings
            CreateMap<Paciente, PacienteDTO>().ReverseMap();
            CreateMap<Paciente, PacienteCreateDTO>().ReverseMap();
            CreateMap<Paciente, PacienteUpdateDTO>().ReverseMap();

            // Cama mappings
            CreateMap<Cama, CamaDTO>().ReverseMap();
            CreateMap<Cama, CamaCreateDTO>().ReverseMap();
            CreateMap<Cama, CamaUpdateDTO>().ReverseMap();

            // Habitacion mappings
            CreateMap<Habitacion, HabitacionDTO>().ReverseMap();
            CreateMap<Habitacion, HabitacionCreateDTO>().ReverseMap();
            CreateMap<Habitacion, HabitacionUpdateDTO>().ReverseMap();

            // Asignacion mappings
            CreateMap<Asignacion, AsignacionDTO>().ReverseMap();
            CreateMap<Asignacion, AsignacionCreateDTO>().ReverseMap();
            CreateMap<Asignacion, AsignacionUpdateDTO>().ReverseMap();

            // HistorialAlta mappings
            CreateMap<HistorialAlta, HistorialAltaDTO>().ReverseMap();
            CreateMap<HistorialAlta, HistorialAltaCreateDTO>().ReverseMap();
            CreateMap<HistorialAlta, HistorialAltaUpdateDTO>().ReverseMap();

            // Usuario mappings
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioCreateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDTO>().ReverseMap();

            // Rol mappings
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Rol, RolCreateDTO>().ReverseMap();
            CreateMap<Rol, RolUpdateDTO>().ReverseMap();

            // Consulta mappings
            CreateMap<Consulta, ConsultaDTO>().ReverseMap();
            CreateMap<Consulta, ConsultaCreateDTO>().ReverseMap();
            CreateMap<Consulta, ConsultaUpdateDTO>().ReverseMap();

            // Ingreso mappings
            CreateMap<Ingreso, IngresoDTO>().ReverseMap();
            CreateMap<Ingreso, IngresoCreateDTO>().ReverseMap();
            CreateMap<Ingreso, IngresoUpdateDTO>().ReverseMap();
        }
    }
}
