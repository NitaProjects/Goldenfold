using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public enum EstadoCama
{
    Disponible,
    Ocupado,
    EnLimpieza
}

public enum TipoCama
{
    General,
    UCI,
    Postoperatorio
}

public class Cama
{
    [Key]
    [StringLength(10)]
    public string Ubicacion { get; set; }

    [Required]
    [StringLength(50)]
    public EstadoCama Estado { get; set; } = EstadoCama.Disponible;

    [Required]
    [StringLength(50)]
    public TipoCama Tipo { get; set; }

    // Propiedad de navegación
    public ICollection<Asignacion> Asignaciones { get; set; }

}