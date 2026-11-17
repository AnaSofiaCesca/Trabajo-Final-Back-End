using Microsoft.EntityFrameworkCore;
using TpFinal.Core.Entities;
using TpFinal.Core.Enums;

namespace TpFinal.DL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctores { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Turno> Turnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación Turno -> Doctor (1:N)
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Doctor)
            .WithMany(d => d.Turnos)
            .HasForeignKey(t => t.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación Turno -> Paciente (1:N)
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Paciente)
            .WithMany(p => p.Turnos)
            .HasForeignKey(t => t.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.Matricula)
            .IsUnique();

        modelBuilder.Entity<Paciente>()
            .HasIndex(p => p.Dni)
            .IsUnique();

        modelBuilder.Entity<Turno>()
            .HasIndex(t => new { t.DoctorId, t.FechaHora });

        // Datos de prueba
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 1, Nombre = "Laura", Apellido = "López", Matricula = "MP-1001", Especialidad = "Clínica Médica", Telefono = "381-4000001", Email = "laura.lopez@turnero.com" },
            new Doctor { Id = 2, Nombre = "Martín", Apellido = "Gómez", Matricula = "MP-1002", Especialidad = "Cardiología", Telefono = "381-4000002", Email = "martin.gomez@turnero.com" }
        );

        modelBuilder.Entity<Paciente>().HasData(
            new Paciente { Id = 1, Nombre = "Juan", Apellido = "García", Dni = "30111222", FechaNacimiento = new DateTime(1985, 3, 12), Telefono = "381-5000001", Email = "juan.garcia@mail.com", ObraSocial = "OSDE" },
            new Paciente { Id = 2, Nombre = "María", Apellido = "Fernández", Dni = "35222333", FechaNacimiento = new DateTime(1992, 7, 25), Telefono = "381-5000002", Email = "maria.fernandez@mail.com", ObraSocial = "Swiss Medical" }
        );

        modelBuilder.Entity<Turno>().HasData(
            new Turno { Id = 1, DoctorId = 1, PacienteId = 1, FechaHora = new DateTime(2026, 12, 1, 9, 0, 0), Estado = EstadoTurno.Pendiente, Motivo = "Control general" }
        );
    }
}
