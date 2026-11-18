
using TpFinal.BL.DTOs;

namespace TpFinal.BL.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDTO>> GetAllAsync();
    Task<DoctorDTO?> GetByIdAsync(int id);
    Task<DoctorDTO> CreateAsync(DoctorDTO dto);
    Task<bool> UpdateAsync(int id, DoctorDTO dto);
    Task<bool> DeleteAsync(int id);
}
