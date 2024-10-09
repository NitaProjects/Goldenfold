using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Habitacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_habitacion")]
        public int IdHabitacion { get; set; }

        [Column("edificio")]
        public string Edificio { get; set; }

        [Column("planta")]
        public string Planta { get; set; }

        [Column("numero_habitacion")]
        public string NumeroHabitacion { get; set; }
        
        public ICollection<Cama> Camas { get; set; }

        public Habitacion()
        {
            Camas = new List<Cama>();
        }
    }
}
