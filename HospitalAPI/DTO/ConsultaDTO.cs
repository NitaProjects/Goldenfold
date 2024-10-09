using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class ConsultaDTO
    {
        public int IdConsulta { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaConsulta { get; set; }
        public string Estado { get; set; }
    }

    public class ConsultaCreateDTO
    {
        [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
        public int? IdPaciente { get; set; }

        [Required(ErrorMessage = "El ID del médico es obligatorio.")]
        public int? IdMedico { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio.")]
        [StringLength(200, ErrorMessage = "El motivo no puede tener más de 200 caracteres.")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "La fecha de solicitud es obligatoria.")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado de la consulta es obligatorio.")]
        public string Estado { get; set; }
    }

    public class ConsultaUpdateDTO
    {
        [Required(ErrorMessage = "El ID de la consulta es obligatorio.")]
        public int IdConsulta { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio.")]
        [StringLength(200, ErrorMessage = "El motivo no puede tener más de 200 caracteres.")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "El estado de la consulta es obligatorio.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La fecha de consulta es obligatoria.")]
        public DateTime? FechaConsulta { get; set; }
    }
}
