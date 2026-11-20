using System.ComponentModel.DataAnnotations;
using TpFinal.Core.Enums;

namespace TpFinal.BL.DTOs;

public class TurnoDTO
{
    public int Id { get; set; }

    // Fecha y hora completas (para mostrar)
    public DateTime FechaHora { get; set; }

    // Día y horario por separado (para el formulario de edición)
    [Required(ErrorMessage = "Debe seleccionar un día")]
    public string? Fecha { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un horario")]
    public string? Hora { get; set; }

    public EstadoTurno Estado { get; set; } = EstadoTurno.Pendiente;

    [StringLength(250, ErrorMessage = "El motivo no puede superar los 250 caracteres")]
    public string? Motivo { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres")]
    public string? Observaciones { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un doctor")]
    public int DoctorId { get; set; }

    public string? DoctorNombreCompleto { get; set; }
    public string? DoctorEspecialidad { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un paciente")]
    public int PacienteId { get; set; }

    public string? PacienteNombreCompleto { get; set; }
    public string? PacienteDni { get; set; }
}
