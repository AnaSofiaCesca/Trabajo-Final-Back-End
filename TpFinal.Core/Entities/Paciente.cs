using System.ComponentModel.DataAnnotations;

namespace TpFinal.Core.Entities;

public class Paciente : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [StringLength(15)]
    public string Dni { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

    [StringLength(30)]
    public string? Telefono { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? ObraSocial { get; set; }

    public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
