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
            .HasKey(p => p.ID_Paciente); // Primary key

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.HistorialAltas)
            .WithOne(ha => ha.Paciente)
            .HasForeignKey(ha => ha.ID_Paciente); // One-to-Many relationship with HistorialAltas

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Asignaciones)
            .WithOne(a => a.Paciente)
            .HasForeignKey(a => a.ID_Paciente); // One-to-Many relationship with Asignaciones

        modelBuilder.Entity<Paciente>()
            .HasIndex(p => p.Seguridad_Social)
            .IsUnique(); // Unique index on Seguridad_Social

        // Cama
        modelBuilder.Entity<Cama>()
            .HasKey(c => c.Ubicacion); // Primary key

        modelBuilder.Entity<Cama>()
            .HasMany(c => c.Asignaciones)
            .WithOne(a => a.Cama)
            .HasForeignKey(a => a.Ubicacion); // One-to-Many relationship with Asignaciones

        // Habitacion
        modelBuilder.Entity<Habitaciones>()
            .HasKey(h => h.ID_Habitacion); // Primary key

        // Asignacion
        modelBuilder.Entity<Asignacion>()
            .HasKey(a => a.ID_Asignacion); // Primary key

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Asignaciones)
            .HasForeignKey(a => a.ID_Paciente); // Foreign key relationship with Paciente

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Cama)
            .WithMany(c => c.Asignaciones)
            .HasForeignKey(a => a.Ubicacion); // Foreign key relationship with Cama

        modelBuilder.Entity<Asignacion>()
            .HasOne(a => a.Usuario)
            .WithMany(u => u.Asignaciones)
            .HasForeignKey(a => a.Asignado_Por); // Foreign key relationship with Usuario

        // HistorialAltas
        modelBuilder.Entity<HistorialAlta>()
            .HasKey(ha => ha.ID_Historial); // Primary key

        modelBuilder.Entity<HistorialAlta>()
            .HasOne(ha => ha.Paciente)
            .WithMany(p => p.HistorialAltas)
            .HasForeignKey(ha => ha.ID_Paciente); // Foreign key relationship with Paciente

        // Usuario
        modelBuilder.Entity<Usuarios>()
            .HasKey(u => u.ID_Usuario); // Primary key

        modelBuilder.Entity<Usuarios>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.ID_Rol); // Foreign key relationship with Rol

        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Asignaciones)
            .WithOne(a => a.Usuario)
            .HasForeignKey(a => a.Asignado_Por); // One-to-Many relationship with Asignaciones

        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Usuario)
            .IsUnique(); // Unique index on Usuario

        // Rol
        modelBuilder.Entity<Rol>()
            .HasKey(r => r.ID_Rol); // Primary key

        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Usuarios)
            .WithOne(u => u.Rol)
            .HasForeignKey(u => u.ID_Rol); // One-to-Many relationship with Usuarios

        base.OnModelCreating(modelBuilder);
    }
}

