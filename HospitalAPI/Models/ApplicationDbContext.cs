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
            .HasKey(p => p.IdPaciente); 

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.HistorialAltas)
            .WithOne(ha => ha.Paciente)
            .HasForeignKey(ha => ha.IdPaciente); 

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Asignaciones)
            .WithOne(a => a.Paciente)
            .HasForeignKey(a => a.IdPaciente); 

        modelBuilder.Entity<Paciente>()
            .HasIndex(p => p.SeguridadSocial)
            .IsUnique(); 


        // Cama
        modelBuilder.Entity<Cama>()
            .HasKey(c => c.Ubicacion); 

        modelBuilder.Entity<Cama>()
            .HasMany(c => c.Asignaciones)
            .WithOne(a => a.Cama)
            .HasForeignKey(a => a.Ubicacion); 

        // Habitacion
        modelBuilder.Entity<Habitacion>()
            .HasKey(h => h.IdHabitacion);

        // Asignacion
        modelBuilder.Entity<Asignacion>()
            .HasKey(a => a.IdAsignacion);

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Asignaciones)
            .HasForeignKey(a => a.IdPaciente);

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Cama)  
            .WithMany(c => c.Asignaciones) 
            .HasForeignKey(a => a.Ubicacion); 

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Usuario) 
            .WithMany(u => u.Asignaciones) 
            .HasForeignKey(a => a.AsignadoPor);

        // HistorialAlta
        modelBuilder.Entity<HistorialAlta>()
            .HasKey(ha => ha.IdHistorial);

        modelBuilder.Entity<HistorialAlta>()
            .HasOne(ha => ha.Paciente)  
            .WithMany(p => p.HistorialAltas) 
            .HasForeignKey(ha => ha.IdPaciente); 

        // Usuario
        modelBuilder.Entity<Usuario>() 
            .HasKey(u => u.IdUsuario);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol) 
            .WithMany(r => r.Usuarios) 
            .HasForeignKey(u => u.IdRol); 

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Asignaciones) 
            .WithOne(a => a.Usuario) 
            .HasForeignKey(a => a.AsignadoPor); 

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.NombreUsuario) 
            .IsUnique(); 

        // Rol
        modelBuilder.Entity<Rol>()
            .HasKey(r => r.IdRol);  

        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Usuarios) 
            .WithOne(u => u.Rol) 
            .HasForeignKey(u => u.IdRol); 

        base.OnModelCreating(modelBuilder);

        // Rol precarga
        modelBuilder.Entity<Rol>().HasData(
            new Rol { IdRol = 1, NombreRol = RoleType.Administrativo },
            new Rol { IdRol = 2, NombreRol = RoleType.Medico },
            new Rol { IdRol = 3, NombreRol = RoleType.ControladorCamas },
            new Rol { IdRol = 4, NombreRol = RoleType.AdministradorSistemas }
        );
    }
}

