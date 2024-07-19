using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Paciente> Pacientes { get; set; } = default!;
    public DbSet<Asignacion> Asignacion { get; set; } = default!;
    public DbSet<Cama> Cama { get; set; } = default!;
    public DbSet<Habitaciones> Habitaciones { get; set; } = default!;
    public DbSet<HistorialAlta> HistorialAlta { get; set; } = default!;
    public DbSet<Rol> Rol { get; set; } = default!;
    public DbSet<Usuarios> Usuarios { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Paciente
        modelBuilder.Entity<Paciente>()
            .HasKey(p => p.ID_Paciente); 

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.HistorialAltas)
            .WithOne(ha => ha.Paciente)
            .HasForeignKey(ha => ha.ID_Paciente); 

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Asignaciones)
            .WithOne(a => a.Paciente)
            .HasForeignKey(a => a.ID_Paciente); 

        modelBuilder.Entity<Paciente>()
            .HasIndex(p => p.Seguridad_Social)
            .IsUnique(); 


        // Cama
        modelBuilder.Entity<Cama>()
            .HasKey(c => c.Ubicacion); 

        modelBuilder.Entity<Cama>()
            .HasMany(c => c.Asignaciones)
            .WithOne(a => a.Cama)
            .HasForeignKey(a => a.Ubicacion); 

        // Habitacion
        modelBuilder.Entity<Habitaciones>()
            .HasKey(h => h.ID_Habitacion); //ID_Habitacion es la clave primaria de la entidad Habitacion.

        // Asignacion
        modelBuilder.Entity<Asignacion>()
            .HasKey(a => a.ID_Asignacion); //ID_Asignación es la clave primaria en la entidad Asignación

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Paciente)  //Relación 1 a 1 o 1 a N.
            .WithMany(p => p.Asignaciones) //Un Paciente puede estar relacionado con N Asignaciones
            .HasForeignKey(a => a.ID_Paciente); //ID_Paciente es la clave foránea en la Entidad Asignación

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Cama)  //Relación 1 a 1 o 1 a N.
            .WithMany(c => c.Asignaciones) // Una Cama puede estar relacionada con N Asignaciones
            .HasForeignKey(a => a.Ubicacion); //Ubicación es la clave foránea en la Entidad Asignación

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Usuario) //Relación 1 a 1 o 1 a N.
            .WithMany(u => u.Asignaciones) //Un Usuario puede estar relacionado con N Asignaciones
            .HasForeignKey(a => a.Asignado_Por); //Asignado_Por es la clave foránea en la Entidad Asignación

        // HistorialAltas
        modelBuilder.Entity<HistorialAlta>()
            .HasKey(ha => ha.ID_Historial); //ID_Hisotiral como clave primaria de HistorialAlta. 

        modelBuilder.Entity<HistorialAlta>()
            .HasOne(ha => ha.Paciente)  //Relación 1 a 1 o 1 a N.
            .WithMany(p => p.HistorialAltas) //Un Paciente puede estar relacionado con N Historiales de Alta
            .HasForeignKey(ha => ha.ID_Paciente); //ID_Paciente es la clave foránea en la Entidad HistorialAlta.

        // Usuario
        modelBuilder.Entity<Usuarios>() // Indicar qué Entidad estamos configurando
            .HasKey(u => u.ID_Usuario); // ID_Usuario como clave primaria de Usuarios. 

        modelBuilder.Entity<Usuarios>()
            .HasOne(u => u.Rol) //Relación 1 a 1 o N Usuarios pueden tener el mismo rol.
            .WithMany(r => r.Usuarios) //Un rol puede estar relacionado con N Usuarios
            .HasForeignKey(u => u.ID_Rol); //ID_Rol es la clave foránea en a entidad Usuarios

        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Asignaciones) //Relación 1 a N con Asignaciones
            .WithOne(a => a.Usuario) //Cada Asignación tiene una referencia a Usuario 
            .HasForeignKey(a => a.Asignado_Por); //Asignado_Por es la clave foránea en la entidad Asignación 

        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Usuario) //Crear índice en la propiedad Usuario de la Entidad Usuario(bbdd) para mejorar consultas. 
            .IsUnique(); //El índice debe ser único

        // Rol
        modelBuilder.Entity<Rol>()
            .HasKey(r => r.ID_Rol); //ID_Rol como clave primaria de Rol. 

        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Usuarios) //Relación 1 a N con Usuarios
            .WithOne(u => u.Rol) //Cada Rol tiene una referencia a Usuario 
            .HasForeignKey(u => u.ID_Rol); // ID_Rol es la clave foránea en a entidad Usuarios

        base.OnModelCreating(modelBuilder);
    }
}

