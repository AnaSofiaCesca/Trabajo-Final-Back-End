using System.ComponentModel.DataAnnotations;

namespace TpFinal.Core.Entities;

public class Doctor : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Matricula { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Especialidad { get; set; } = string.Empty;

    [StringLength(30)]
    public string? Telefono { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
