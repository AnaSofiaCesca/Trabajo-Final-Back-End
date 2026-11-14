namespace TpFinal.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDoctorRepository Doctores { get; }
    IPacienteRepository Pacientes { get; }
    ITurnoRepository Turnos { get; }

    Task<int> SaveChangesAsync();
}
