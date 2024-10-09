using System.ComponentModel.DataAnnotations;
namespace HospitalApi.DTO
{
    public class HabitacionDTO
    {
        public int IdHabitacion { get; set; }
        public string Edificio { get; set; }
        public string Planta { get; set; }
        public string NumeroHabitacion { get; set; }
        public IEnumerable<CamaDTO> Camas { get; set; }
    }

    public class HabitacionCreateDTO
    {
        [Required(ErrorMessage = "El edificio es obligatorio.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El código del edificio debe tener 2 caracteres.")]
        public string Edificio { get; set; }

        [Required(ErrorMessage = "La planta es obligatoria.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "La planta debe tener 2 caracteres.")]
        public string Planta { get; set; }

        [Required(ErrorMessage = "El número de la habitación es obligatorio.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El número de la habitación debe tener 2 caracteres.")]
        public string NumeroHabitacion { get; set; }

        [Required(ErrorMessage = "El tipo de cama es obligatorio.")]
        public string TipoCama { get; set; } 
    }

    public class HabitacionUpdateDTO
    {
        [Required(ErrorMessage = "El edificio es obligatorio.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El código del edificio debe tener 2 caracteres.")]
        public string Edificio { get; set; }

        [Required(ErrorMessage = "La planta es obligatoria.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "La planta debe tener 2 caracteres.")]
        public string Planta { get; set; }

        [Required(ErrorMessage = "El número de la habitación es obligatorio.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El número de la habitación debe tener 2 caracteres.")]
        public string NumeroHabitacion { get; set; }

        [Required(ErrorMessage = "El tipo de cama es obligatorio.")]
        public string TipoCama { get; set; } 
    }
}
