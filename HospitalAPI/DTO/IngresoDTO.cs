using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class IngresoDTO
    {
        public int IdIngreso { get; set; }

        public int IdPaciente { get; set; }
        public string NombrePaciente { get; set; }

        public int IdMedico { get; set; }
        public string NombreMedico { get; set; }

        public string Motivo { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaIngreso { get; set; }

        public string Estado { get; set; }
        
        public int? IdAsignacion { get; set; }
        
        public string TipoCama { get; set; }
    }

    public class IngresoCreateDTO
    {
        [Required(ErrorMessage = "El paciente es obligatorio.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "El médico es obligatorio.")]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "El motivo del ingreso es obligatorio.")]
        [StringLength(500, ErrorMessage = "El motivo no puede tener más de 500 caracteres.")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "La fecha de solicitud es obligatoria.")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado del ingreso es obligatorio.")]
        public string Estado { get; set; } = EstadoIngreso.Pendiente.ToString();

        [Required(ErrorMessage = "El tipo de cama es obligatorio.")]
        public string TipoCama { get; set; }
    }

    public class IngresoUpdateDTO
    {
        [Required]
        public int IdIngreso { get; set; }

        [Required(ErrorMessage = "El paciente es obligatorio.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "El médico es obligatorio.")]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "El motivo del ingreso es obligatorio.")]
        [StringLength(200, ErrorMessage = "El motivo no puede tener más de 200 caracteres.")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "La fecha de solicitud es obligatoria.")]
        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaIngreso { get; set; }

        [Required(ErrorMessage = "El estado del ingreso es obligatorio.")]
        public string Estado { get; set; }

        public int? IdAsignacion { get; set; }

        [Required(ErrorMessage = "El tipo de cama es obligatorio.")]
        public string TipoCama { get; set; }
    }
}
