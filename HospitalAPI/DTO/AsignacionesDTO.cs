namespace HospitalApi.DTO
{
    public class AsignacionesDTO
    {
        public int id_asignacion { get; set; }
        public int id_paciente { get; set; }
        public string ubicacion { get; set; }
        public DateTime fecha_asignacion { get; set; }
        public DateTime? fecha_liberacion { get; set; }
        public int asignado_por { get; set; }
    }
}
