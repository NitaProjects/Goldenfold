using AutoMapper;
using HospitalApi.Models;
using HospitalAPI.DTO;
using HospitalApi;

namespace HospitalAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Paciente mappings
            CreateMap<Paciente, PacienteDTO>().ReverseMap();

            // Cama mappings
            CreateMap<Cama, CamaDTO>().ReverseMap();

            // Habitacion mappings
            CreateMap<Habitaciones, HabitacionesDTO>().ReverseMap();

            // Asignacion mappings
            CreateMap<Asignacion, AsignacionDTO>().ReverseMap();

            // HistorialAlta mappings
            CreateMap<HistorialAlta, HistorialAltaDTO>().ReverseMap();

            // Usuario mappings
            CreateMap<Usuarios, UsuariosDTO>().ReverseMap();

            // Rol mappings
            CreateMap<Rol, RolDTO>().ReverseMap();
        }
    }
}
