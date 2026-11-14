using TpFinal.Core.Entities;

namespace TpFinal.Core.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<bool> ExistsMatriculaAsync(string matricula, int? excludeId = null);
    Task<bool> HasTurnosAsync(int doctorId);
}
