using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;
public class Usuarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_Usuario { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string Usuario { get; set; }

    [Required]
    [StringLength(255)]
    public string Contraseña { get; set; }

    [ForeignKey("Rol")]
    public int ID_Rol { get; set; }
    public Rol Rol { get; set; }

    // Navigation properties
    public ICollection<Asignacion> Asignaciones { get; set; }
}