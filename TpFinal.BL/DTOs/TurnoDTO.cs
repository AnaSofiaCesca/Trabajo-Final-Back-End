using System.ComponentModel.DataAnnotations;
using TpFinal.Core.Enums;

namespace TpFinal.BL.DTOs;

public class TurnoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La fecha y hora son obligatorias")]
    [DataType(DataType.DateTime)]
    public DateTime FechaHora { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
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
