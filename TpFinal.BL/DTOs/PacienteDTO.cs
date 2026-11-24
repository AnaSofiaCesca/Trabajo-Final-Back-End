using System.ComponentModel.DataAnnotations;

namespace TpFinal.BL.DTOs;

public class PacienteDTO
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Apellido")]
    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Display(Name = "DNI")]
    [Required(ErrorMessage = "El DNI es obligatorio")]
    [StringLength(15)]
    [RegularExpression(
        @"^\d{7,8}$",
        ErrorMessage = "El DNI debe tener 7 u 8 números, sin puntos")]
    public string Dni { get; set; } = string.Empty;

    [Display(Name = "Fecha de nacimiento")]
    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }

    [Display(Name = "Teléfono")]
    [StringLength(30)]
    public string? Telefono { get; set; }

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(150)]
    public string? Email { get; set; }

    [Display(Name = "Obra social")]
    [StringLength(100)]
    public string? ObraSocial { get; set; }

    [Display(Name = "Nombre completo")]
    public string NombreCompleto => $"{Apellido}, {Nombre}";
}