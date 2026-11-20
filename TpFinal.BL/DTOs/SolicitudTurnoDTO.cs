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
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Ingrese la fecha con el formato dd/mm/aaaa")]
    public string FechaNacimiento { get; set; } = string.Empty;

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

    [Required(ErrorMessage = "Debe seleccionar un día")]
    public string? Fecha { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un horario")]
    public string? Hora { get; set; }

    [StringLength(250, ErrorMessage = "El motivo no puede superar los 250 caracteres")]
    public string? Motivo { get; set; }
}
