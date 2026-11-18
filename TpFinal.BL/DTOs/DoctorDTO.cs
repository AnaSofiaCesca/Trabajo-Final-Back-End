
using System.ComponentModel.DataAnnotations;

namespace TpFinal.BL.DTOs;

public class DoctorDTO
{
	public int Id { get; set; }

	[Required(ErrorMessage = "El nombre es obligatorio")]
	[StringLength(100)]
	public string Nombre { get; set; } = string.Empty;

	[Required(ErrorMessage = "El apellido es obligatorio")]
	[StringLength(100)]
	public string Apellido { get; set; } = string.Empty;

	[Required(ErrorMessage = "La matrícula es obligatoria")]
	[StringLength(20)]
	public string Matricula { get; set; } = string.Empty;

	[Required(ErrorMessage = "La especialidad es obligatoria")]
	[StringLength(100)]
	public string Especialidad { get; set; } = string.Empty;

	[StringLength(30)]
	public string? Telefono { get; set; }

	[EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
	[StringLength(150)]
	public string? Email { get; set; }
}
