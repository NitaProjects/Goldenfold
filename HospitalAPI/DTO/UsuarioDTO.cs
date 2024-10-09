using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public int IdRol { get; set; }
    }

    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 30 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "La contraseña debe tener entre 3 y 30 caracteres.")]
        public string Contrasenya { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int IdRol { get; set; }
    }

    public class UsuarioUpdateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 30 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "La contraseña debe tener entre 3 y 30 caracteres.")]
        public string Contrasenya { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int IdRol { get; set; }
    }
}
