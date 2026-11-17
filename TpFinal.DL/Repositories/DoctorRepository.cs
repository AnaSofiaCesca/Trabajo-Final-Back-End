using Microsoft.EntityFrameworkCore;
using TpFinal.Core.Entities;
using TpFinal.Core.Interfaces;
using TpFinal.DL.Data;

namespace TpFinal.DL.Repositories;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking()
            .OrderBy(d => d.Apellido)
            .ThenBy(d => d.Nombre)
            .ToListAsync();
    }

    public async Task<bool> ExistsMatriculaAsync(string matricula, int? excludeId = null)
    {
        return await _dbSet.AnyAsync(d => d.Matricula == matricula && (excludeId == null || d.Id != excludeId));
    }

    public async Task<bool> HasTurnosAsync(int doctorId)
    {
        return await _context.Turnos.AnyAsync(t => t.DoctorId == doctorId);
    }
}
