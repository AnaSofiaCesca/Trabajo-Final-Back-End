
using TpFinal.BL.DTOs;

namespace TpFinal.BL.Interfaces;

public interface IPacienteService
{
	Task<IEnumerable<PacienteDTO>> GetAllAsync();
	Task<PacienteDTO?> GetByIdAsync(int id);
	Task<PacienteDTO> CreateAsync(PacienteDTO dto);
	Task<bool> UpdateAsync(int id, PacienteDTO dto);
	Task<bool> DeleteAsync(int id);
}
