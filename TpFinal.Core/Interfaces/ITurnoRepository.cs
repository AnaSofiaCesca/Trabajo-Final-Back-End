using TpFinal.Core.Entities;

namespace TpFinal.Core.Interfaces;

public interface ITurnoRepository : IRepository<Turno>
{
    Task<IEnumerable<Turno>> GetAllWithDetailsAsync();
    Task<Turno?> GetByIdWithDetailsAsync(int id);
    Task<bool> IsDoctorBusyAsync(int doctorId, DateTime fechaHora, int? excludeTurnoId = null);
    Task<bool> IsPacienteBusyAsync(int pacienteId, DateTime fechaHora, int? excludeTurnoId = null);
}
