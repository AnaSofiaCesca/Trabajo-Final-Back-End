using TpFinal.Core.Interfaces;
using TpFinal.DL.Data;
using TpFinal.DL.Repositories;

namespace TpFinal.DL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDoctorRepository? _doctores;
    private IPacienteRepository? _pacientes;
    private ITurnoRepository? _turnos;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IDoctorRepository Doctores => _doctores ??= new DoctorRepository(_context);
    public IPacienteRepository Pacientes => _pacientes ??= new PacienteRepository(_context);
    public ITurnoRepository Turnos => _turnos ??= new TurnoRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
