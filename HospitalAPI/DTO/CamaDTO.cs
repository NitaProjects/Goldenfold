using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class CamaDTO
    {
        public int IdCama { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public int IdHabitacion { get; set; }
    }

    public class CamaCreateDTO
    {
        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La ubicación debe tener exactamente 10 caracteres.")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El estado de la cama es obligatorio.")]
        public string Estado { get; set; }  // Validado en el controlador o servicio como Enum

        [Required(ErrorMessage = "El tipo de cama es obligatorio.")]
        public string Tipo { get; set; }  // Validado en el controlador o servicio como Enum

        [Required(ErrorMessage = "El ID de la habitación es obligatorio.")]
        public int IdHabitacion { get; set; }
    }

    public class CamaUpdateDTO
    {
        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La ubicación debe tener exactamente 10 caracteres.")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El estado de la cama es obligatorio.")]
        public string Estado { get; set; }  // Validado en el controlador o servicio como Enum

        [Required(ErrorMessage = "El tipo de cama es obligatorio.")]
        public string Tipo { get; set; }  // Validado en el controlador o servicio como Enum

        [Required(ErrorMessage = "El ID de la habitación es obligatorio.")]
        public int IdHabitacion { get; set; }
    }
}
