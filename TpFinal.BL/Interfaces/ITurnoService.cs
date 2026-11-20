using TpFinal.BL.DTOs;

namespace TpFinal.BL.Interfaces;

public interface ITurnoService
{
    Task<IEnumerable<TurnoDTO>> GetAllAsync();
    Task<TurnoDTO?> GetByIdAsync(int id);
    Task<TurnoDTO> SolicitarAsync(SolicitudTurnoDTO dto);
    Task<bool> UpdateAsync(int id, TurnoDTO dto);
    Task<bool> DeleteAsync(int id);
}
