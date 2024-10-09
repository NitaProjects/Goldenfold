using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Cama
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cama")]
        public int IdCama { get; set; } 

        [Column("ubicacion")]
        public string Ubicacion { get; set; }

        [Column("estado")]
        public EstadoCama Estado { get; set; } = EstadoCama.Disponible;

        [Column("tipo")]
        public TipoCama Tipo { get; set; } = TipoCama.General;

        [ForeignKey("Habitacion")]
        [Column("id_habitacion")]
        public int IdHabitacion { get; set; }
        public Habitacion Habitacion { get; set; }

        // Propiedad de navegación para Asignaciones
        public ICollection<Asignacion> Asignaciones { get; set; }

        // Constructor
        public Cama()
        {
            Asignaciones = new List<Asignacion>();
        }
    }

    // Enum para el estado de la cama
    public enum EstadoCama
    {
        Disponible,
        NoDisponible
    }

    // Enum para el tipo de cama
    public enum TipoCama
    {
        General,
        UCI,
        Postoperatorio
    }
}
