using AutoMapper;
using HospitalApi.Models;
using HospitalApi.DTO;
using HospitalApi;

namespace HospitalApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Paciente mappings
            CreateMap<Paciente, PacientesDTO>().ReverseMap();

            // Cama mappings
            CreateMap<Cama, CamasDTO>().ReverseMap();

            // Habitacion mappings
            CreateMap<Habitaciones, HabitacionesDTO>().ReverseMap();

            // Asignacion mappings
            CreateMap<Asignacion, AsignacionesDTO>().ReverseMap();

            // HistorialAlta mappings
            CreateMap<HistorialAlta, HistorialAltasDTO>().ReverseMap();

            // Usuario mappings
            CreateMap<Usuarios, UsuariosDTO>().ReverseMap();

            // Rol mappings
            CreateMap<Rol, RolDTO>().ReverseMap();
        }
    }
}
