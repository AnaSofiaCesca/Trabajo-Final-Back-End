using System.ComponentModel.DataAnnotations;
using TpFinal.Core.Enums;

namespace TpFinal.Core.Entities;

public class Turno : BaseEntity
{
    [Required]
    public DateTime FechaHora { get; set; }

    [Required]
    public EstadoTurno Estado { get; set; } = EstadoTurno.Pendiente;

    [StringLength(250)]
    public string? Motivo { get; set; }

    [StringLength(500)]
    public string? Observaciones { get; set; }

    [Required]
    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    [Required]
    public int PacienteId { get; set; }

    public Paciente Paciente { get; set; } = null!;
}
