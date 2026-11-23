using System.ComponentModel.DataAnnotations;

namespace TpFinal.BL.DTOs;

public class DoctorDTO
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

    [Display(Name = "Matrícula")]
    [Required(ErrorMessage = "La matrícula es obligatoria")]
    [StringLength(20)]
    public string Matricula { get; set; } = string.Empty;

    [Display(Name = "Especialidad")]
    [Required(ErrorMessage = "La especialidad es obligatoria")]
    [StringLength(100)]
    public string Especialidad { get; set; } = string.Empty;

    [Display(Name = "Teléfono")]
    [StringLength(30)]
    public string? Telefono { get; set; }

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(150)]
    public string? Email { get; set; }

    [Display(Name = "Nombre completo")]
    public string NombreCompleto => $"{Nombre} {Apellido}";
}