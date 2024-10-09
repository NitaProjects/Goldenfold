using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class HistorialAltaDTO
    {
        public int IdHistorial { get; set; }

        public int IdPaciente { get; set; }

        public DateTime FechaAlta { get; set; }

        public string Diagnostico { get; set; }

        public string Tratamiento { get; set; }

        public int IdMedico { get; set; }
    }

    public class HistorialAltaCreateDTO
    {
        [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "La fecha de alta es obligatoria.")]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El diagnóstico es obligatorio.")]
        [StringLength(255, ErrorMessage = "El diagnóstico no puede tener más de 255 caracteres.")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "El tratamiento es obligatorio.")]
        [StringLength(255, ErrorMessage = "El tratamiento no puede tener más de 255 caracteres.")]
        public string Tratamiento { get; set; }

        [Required(ErrorMessage = "El ID del médico es obligatorio.")]
        public int IdMedico { get; set; }
    }

    public class HistorialAltaUpdateDTO
    {
        [Required(ErrorMessage = "El diagnóstico es obligatorio.")]
        [StringLength(255, ErrorMessage = "El diagnóstico no puede tener más de 255 caracteres.")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "El tratamiento es obligatorio.")]
        [StringLength(255, ErrorMessage = "El tratamiento no puede tener más de 255 caracteres.")]
        public string Tratamiento { get; set; }

        [Required(ErrorMessage = "El ID del médico es obligatorio.")]
        public int IdMedico { get; set; }
    }
}
