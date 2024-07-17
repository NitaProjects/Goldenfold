using Microsoft.EntityFrameworkCore;
using HospitalApi;

namespace HospitalApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

public DbSet<HospitalApi.Paciente> Pacientes { get; set; } = default!;

public DbSet<HospitalApi.Asignacion> Asignacion { get; set; } = default!;

public DbSet<HospitalApi.Cama> Cama { get; set; } = default!;

public DbSet<HospitalApi.Habitaciones> Habitaciones { get; set; } = default!;

public DbSet<HospitalApi.HistorialAlta> HistorialAlta { get; set; } = default!;

public DbSet<HospitalApi.Rol> Rol { get; set; } = default!;

public DbSet<HospitalApi.Usuarios> Usuarios { get; set; } = default!;
}

