using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Paciente> Pacientes { get; set; } = default!;
    public DbSet<Asignacion> Asignaciones { get; set; } = default!;
    public DbSet<Cama> Camas { get; set; } = default!;
    public DbSet<Habitacion> Habitaciones { get; set; } = default!;
    public DbSet<HistorialAlta> HistorialesAltas { get; set; } = default!;
    public DbSet<Rol> Roles { get; set; } = default!;
    public DbSet<Usuario> Usuarios { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Paciente
        modelBuilder.Entity<Paciente>()
            .HasKey(p => p.id_paciente); 

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.historial_altas)
            .WithOne(ha => ha.paciente)
            .HasForeignKey(ha => ha.id_paciente); 

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.asignaciones)
            .WithOne(a => a.paciente)
            .HasForeignKey(a => a.id_paciente); 

        modelBuilder.Entity<Paciente>()
            .HasIndex(p => p.seguridad_social)
            .IsUnique(); 


        // Cama
        modelBuilder.Entity<Cama>()
            .HasKey(c => c.ubicacion); 

        modelBuilder.Entity<Cama>()
            .HasMany(c => c.asignaciones)
            .WithOne(a => a.cama)
            .HasForeignKey(a => a.ubicacion); 

        // Habitacion
        modelBuilder.Entity<Habitacion>()
            .HasKey(h => h.id_habitacion); //ID_Habitacion es la clave primaria de la entidad Habitacion.

        // Asignacion
        modelBuilder.Entity<Asignacion>()
            .HasKey(a => a.id_asignacion); //ID_Asignación es la clave primaria en la entidad Asignación

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.paciente)  //Relación 1 a 1 o 1 a N.
            .WithMany(p => p.asignaciones) //Un Paciente puede estar relacionado con N Asignaciones
            .HasForeignKey(a => a.id_paciente); //ID_Paciente es la clave foránea en la Entidad Asignación

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.cama)  //Relación 1 a 1 o 1 a N.
            .WithMany(c => c.asignaciones) // Una Cama puede estar relacionada con N Asignaciones
            .HasForeignKey(a => a.ubicacion); //Ubicación es la clave foránea en la Entidad Asignación

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.usuario) //Relación 1 a 1 o 1 a N.
            .WithMany(u => u.asignaciones) //Un Usuario puede estar relacionado con N Asignaciones
            .HasForeignKey(a => a.asignado_por); //Asignado_Por es la clave foránea en la Entidad Asignación

        // HistorialAlta
        modelBuilder.Entity<HistorialAlta>()
            .HasKey(ha => ha.id_historial); //ID_Hisotiral como clave primaria de HistorialAlta. 

        modelBuilder.Entity<HistorialAlta>()
            .HasOne(ha => ha.paciente)  //Relación 1 a 1 o 1 a N.
            .WithMany(p => p.historial_altas) //Un Paciente puede estar relacionado con N Historiales de Alta
            .HasForeignKey(ha => ha.id_paciente); //ID_Paciente es la clave foránea en la Entidad HistorialAlta.

        // Usuario
        modelBuilder.Entity<Usuario>() // Indicar qué Entidad estamos configurando
            .HasKey(u => u.id_usuario); // ID_Usuario como clave primaria de Usuarios. 

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.rol) //Relación 1 a 1 o N Usuarios pueden tener el mismo rol.
            .WithMany(r => r.usuarios) //Un rol puede estar relacionado con N Usuarios
            .HasForeignKey(u => u.id_rol); //ID_Rol es la clave foránea en a entidad Usuarios

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.asignaciones) //Relación 1 a N con Asignaciones
            .WithOne(a => a.usuario) //Cada Asignación tiene una referencia a Usuario 
            .HasForeignKey(a => a.asignado_por); //Asignado_Por es la clave foránea en la entidad Asignación 

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.usuario) //Crear índice en la propiedad Usuario de la Entidad Usuario(bbdd) para mejorar consultas. 
            .IsUnique(); //El índice debe ser único

        // Rol
        modelBuilder.Entity<Rol>()
            .HasKey(r => r.id_rol); //ID_Rol como clave primaria de Rol. 

        modelBuilder.Entity<Rol>()
            .HasMany(r => r.usuarios) //Relación 1 a N con Usuarios
            .WithOne(u => u.rol) //Cada Rol tiene una referencia a Usuario 
            .HasForeignKey(u => u.id_rol); // ID_Rol es la clave foránea en a entidad Usuarios

        base.OnModelCreating(modelBuilder);
    }
}

