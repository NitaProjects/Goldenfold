using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets para las entidades
    public DbSet<Paciente> Pacientes { get; set; } = default!;
    public DbSet<Asignacion> Asignaciones { get; set; } = default!;
    public DbSet<Cama> Camas { get; set; } = default!;
    public DbSet<Habitacion> Habitaciones { get; set; } = default!;
    public DbSet<HistorialAlta> HistorialesAltas { get; set; } = default!;
    public DbSet<Rol> Roles { get; set; } = default!;
    public DbSet<Usuario> Usuarios { get; set; } = default!;
    public DbSet<Consulta> Consultas { get; set; } = default!;
    public DbSet<Ingreso> Ingresos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Paciente
        modelBuilder.Entity<Paciente>()
            .ToTable("pacientes")
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
            .HasMany(p => p.Consultas)
            .WithOne(c => c.Paciente)
            .HasForeignKey(c => c.IdPaciente);

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Ingresos)
            .WithOne(i => i.Paciente)
            .HasForeignKey(i => i.IdPaciente);

        modelBuilder.Entity<Paciente>()
            .Property(p => p.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);

        modelBuilder.Entity<Paciente>()
            .HasIndex(p => p.SeguridadSocial)
            .IsUnique();

        // Cama
        modelBuilder.Entity<Cama>()
            .ToTable("camas")
            .HasKey(c => c.IdCama);

        modelBuilder.Entity<Cama>()
            .HasMany(c => c.Asignaciones)
            .WithOne(a => a.Cama)
            .HasForeignKey(a => a.IdCama);

        modelBuilder.Entity<Cama>()
    .Property(c => c.Estado)
    .HasConversion<string>()
    .HasMaxLength(20);

        modelBuilder.Entity<Cama>()
            .Property(c => c.Tipo)
            .HasConversion<string>()
            .HasMaxLength(20);


        // Habitacion
        modelBuilder.Entity<Habitacion>()
            .ToTable("habitaciones")
            .HasKey(h => h.IdHabitacion);

        modelBuilder.Entity<Habitacion>()
            .HasMany(h => h.Camas)
            .WithOne(c => c.Habitacion)
            .HasForeignKey(c => c.IdHabitacion);

        // Asignacion
        modelBuilder.Entity<Asignacion>()
            .ToTable("asignaciones")
            .HasKey(a => a.IdAsignacion);

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Asignaciones)
            .HasForeignKey(a => a.IdPaciente);

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Cama)
            .WithMany(c => c.Asignaciones)
            .HasForeignKey(a => a.IdCama)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Usuario)
            .WithMany(u => u.Asignaciones)
            .HasForeignKey(a => a.AsignadoPor);

        // HistorialAlta
        modelBuilder.Entity<HistorialAlta>()
            .ToTable("historialaltas")
            .HasKey(ha => ha.IdHistorial);

        modelBuilder.Entity<HistorialAlta>()
            .HasOne(ha => ha.Paciente)
            .WithMany(p => p.HistorialAltas)
            .HasForeignKey(ha => ha.IdPaciente);

        modelBuilder.Entity<HistorialAlta>()
            .HasOne(ha => ha.Medico)
            .WithMany(m => m.HistorialAltas)
            .HasForeignKey(ha => ha.IdMedico);

        // Usuario
        modelBuilder.Entity<Usuario>()
            .ToTable("usuarios")
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
            .HasMany(u => u.Consultas)
            .WithOne(c => c.Medico)
            .HasForeignKey(c => c.IdMedico);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Ingresos)
            .WithOne(i => i.Medico)
            .HasForeignKey(i => i.IdMedico);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.NombreUsuario)
            .IsUnique();

        // Rol
        modelBuilder.Entity<Rol>()
            .ToTable("roles")
            .HasKey(r => r.IdRol);

        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Usuarios)
            .WithOne(u => u.Rol)
            .HasForeignKey(u => u.IdRol);

        // Consultas
        modelBuilder.Entity<Consulta>()
            .ToTable("consultas")
            .HasKey(c => c.IdConsulta);

        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Paciente)
            .WithMany(p => p.Consultas)
            .HasForeignKey(c => c.IdPaciente);

        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Medico)
            .WithMany(m => m.Consultas)
            .HasForeignKey(c => c.IdMedico);

        modelBuilder.Entity<Consulta>()
            .Property(c => c.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Ingresos
        modelBuilder.Entity<Ingreso>()
            .ToTable("ingresos")
            .HasKey(i => i.IdIngreso);

        modelBuilder.Entity<Ingreso>()
            .Property(i => i.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);

        modelBuilder.Entity<Ingreso>()
            .Property(i => i.TipoCama)
            .HasConversion<string>()
            .HasMaxLength(20);

        modelBuilder.Entity<Ingreso>()
            .HasOne(i => i.Paciente)
            .WithMany(p => p.Ingresos)
            .HasForeignKey(i => i.IdPaciente);

        modelBuilder.Entity<Ingreso>()
            .HasOne(i => i.Medico)
            .WithMany(m => m.Ingresos)
            .HasForeignKey(i => i.IdMedico);

        modelBuilder.Entity<Ingreso>()
            .HasOne(i => i.Asignacion)
            .WithMany(a => a.Ingresos)
            .HasForeignKey(i => i.IdAsignacion)
            .OnDelete(DeleteBehavior.Restrict);


        // Precarga de roles
        modelBuilder.Entity<Rol>().HasData(
            new Rol { IdRol = 1, NombreRol = "Administrativo" },
            new Rol { IdRol = 2, NombreRol = "Medico" },
            new Rol { IdRol = 3, NombreRol = "ControladorCamas" },
            new Rol { IdRol = 4, NombreRol = "AdministradorSistemas" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
