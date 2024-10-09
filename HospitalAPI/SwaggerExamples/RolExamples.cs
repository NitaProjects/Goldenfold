using Swashbuckle.AspNetCore.Filters;
using HospitalApi.DTO;

namespace HospitalApi.SwaggerExamples
{
    // Ejemplo para el método GET (Lista de roles)

    public class RolDTOListExample : IExamplesProvider<IEnumerable<RolDTO>>
    {
        public IEnumerable<RolDTO> GetExamples()
        {
            return new List<RolDTO>
            {
                new RolDTO
                {
                    IdRol = 1,
                    NombreRol = "Administrador"
                },
                new RolDTO
                {
                    IdRol = 2,
                    NombreRol = "Usuario"
                }
            };
        }
    }

    // Ejemplo para el método POST (Create Rol)
    public class RolCreateDTOExample : IExamplesProvider<RolCreateDTO>
    {
        public RolCreateDTO GetExamples()
        {
            return new RolCreateDTO
            {
                NombreRol = "Caballero"
            };
        }
    }

    // Ejemplo para el método GET (Get Rol by ID)
    public class RolDTOExample : IExamplesProvider<RolDTO>
    {
        public RolDTO GetExamples()
        {
            return new RolDTO
            {
                IdRol = 5,
                NombreRol = "Caballero"
            };
        }
    }

    // Ejemplo para el método PUT (Update Rol)
    public class RolUpdateDTOExample : IExamplesProvider<RolUpdateDTO>
    {
        public RolUpdateDTO GetExamples()
        {
            return new RolUpdateDTO
            {
                NombreRol = "Super Caballero"
            };
        }
    }

    // Ejemplo para el método DELETE
    public class RolDeleteDTOExample : IExamplesProvider<RolDTO>
    {
        public RolDTO GetExamples()
        {
            return new RolDTO
            {
                IdRol = 5,
                NombreRol = "Super Caballero"
            };
        }
    }

}