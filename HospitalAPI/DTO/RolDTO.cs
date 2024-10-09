using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi.DTO

{
    public class RolDTO
    {
        public int IdRol { get; set; }

        public string NombreRol { get; set; }
    }

    public class RolCreateDTO
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El nombre del rol debe tener entre 4 y 20 caracteres.")]
        public string NombreRol { get; set; }
    }

    public class RolUpdateDTO
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El nombre del rol debe tener entre 4 y 20 caracteres.")]
        public string NombreRol { get; set; }
    }
}
