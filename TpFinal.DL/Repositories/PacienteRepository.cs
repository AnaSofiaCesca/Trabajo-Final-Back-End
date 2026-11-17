using Microsoft.EntityFrameworkCore;
using TpFinal.Core.Entities;
using TpFinal.Core.Interfaces;
using TpFinal.DL.Data;

namespace TpFinal.DL.Repositories;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Paciente>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking()
            .OrderBy(p => p.Apellido)
            .ThenBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<bool> ExistsDniAsync(string dni, int? excludeId = null)
    {
        return await _dbSet.AnyAsync(p => p.Dni == dni && (excludeId == null || p.Id != excludeId));
    }

    public async Task<bool> HasTurnosAsync(int pacienteId)
    {
        return await _context.Turnos.AnyAsync(t => t.PacienteId == pacienteId);
    }
}
