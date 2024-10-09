using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;
public class Login
{
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre de usuario debe tener 20 caracteres como mínimo.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres y máximo 100.")]
    public string Password { get; set; }
}
