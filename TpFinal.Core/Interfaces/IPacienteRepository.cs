using TpFinal.Core.Entities;

namespace TpFinal.Core.Interfaces;

public interface IPacienteRepository : IRepository<Paciente>
{
    Task<bool> ExistsDniAsync(string dni, int? excludeId = null);
    Task<bool> HasTurnosAsync(int pacienteId);
}
