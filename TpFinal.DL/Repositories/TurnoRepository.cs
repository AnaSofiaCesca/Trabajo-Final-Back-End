using Microsoft.EntityFrameworkCore;
using TpFinal.Core.Entities;
using TpFinal.Core.Enums;
using TpFinal.Core.Interfaces;
using TpFinal.DL.Data;

namespace TpFinal.DL.Repositories;

public class TurnoRepository : Repository<Turno>, ITurnoRepository
{
    public TurnoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Turno>> GetAllWithDetailsAsync()
    {
        return await _dbSet.AsNoTracking()
            .Include(t => t.Doctor)
            .Include(t => t.Paciente)
            .OrderBy(t => t.FechaHora)
            .ToListAsync();
    }

    public async Task<Turno?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(t => t.Doctor)
            .Include(t => t.Paciente)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> IsDoctorBusyAsync(int doctorId, DateTime fechaHora, int? excludeTurnoId = null)
    {
        return await _dbSet.AnyAsync(t =>
            t.DoctorId == doctorId &&
            t.FechaHora == fechaHora &&
            t.Estado != EstadoTurno.Cancelado &&
            (excludeTurnoId == null || t.Id != excludeTurnoId));
    }

    public async Task<bool> IsPacienteBusyAsync(int pacienteId, DateTime fechaHora, int? excludeTurnoId = null)
    {
        return await _dbSet.AnyAsync(t =>
            t.PacienteId == pacienteId &&
            t.FechaHora == fechaHora &&
            t.Estado != EstadoTurno.Cancelado &&
            (excludeTurnoId == null || t.Id != excludeTurnoId));
    }
}
