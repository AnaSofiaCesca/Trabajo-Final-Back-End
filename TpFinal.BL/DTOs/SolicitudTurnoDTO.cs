using System.ComponentModel.DataAnnotations;

namespace TpFinal.BL.DTOs;

public class SolicitudTurnoDTO
{
    // Datos del paciente
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El DNI es obligatorio")]
    [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe tener 7 u 8 números, sin puntos")]
    public string Dni { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }

    [StringLength(30)]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? ObraSocial { get; set; }

    // Datos del turno
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un doctor")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "La fecha y hora son obligatorias")]
    [DataType(DataType.DateTime)]
    public DateTime FechaHora { get; set; }

    [StringLength(250, ErrorMessage = "El motivo no puede superar los 250 caracteres")]
    public string? Motivo { get; set; }
}
