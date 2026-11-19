using AutoMapper;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;
using TpFinal.Core.Entities;
using TpFinal.Core.Enums;
using TpFinal.Core.Interfaces;

namespace TpFinal.BL.Services;

public class TurnoService : ITurnoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TurnoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TurnoDTO>> GetAllAsync()
    {
        var turnos = await _unitOfWork.Turnos.GetAllWithDetailsAsync();
        return _mapper.Map<IEnumerable<TurnoDTO>>(turnos);
    }

    public async Task<TurnoDTO?> GetByIdAsync(int id)
    {
        var turno = await _unitOfWork.Turnos.GetByIdWithDetailsAsync(id);

        return turno == null
            ? null
            : _mapper.Map<TurnoDTO>(turno);
    }

    public async Task<TurnoDTO> SolicitarAsync(SolicitudTurnoDTO dto)
    {
        if (await _unitOfWork.Doctores.GetByIdAsync(dto.DoctorId) == null)
            throw new BusinessException("El doctor seleccionado no existe.");

        if (dto.FechaHora < DateTime.Now)
            throw new BusinessException("No se puede solicitar un turno en una fecha y hora pasada.");

        if (dto.FechaNacimiento > DateTime.Today)
            throw new BusinessException("La fecha de nacimiento no puede ser futura.");

        if (await _unitOfWork.Turnos.IsDoctorBusyAsync(dto.DoctorId, dto.FechaHora))
            throw new BusinessException("El doctor ya tiene un turno asignado en ese horario. Elija otro horario.");

        // Busca al paciente por DNI: si no existe lo crea, si existe lo reutiliza
        var paciente = await _unitOfWork.Pacientes.GetByDniAsync(dto.Dni.Trim());

        if (paciente == null)
        {
            paciente = new Paciente
            {
                Nombre = dto.Nombre.Trim(),
                Apellido = dto.Apellido.Trim(),
                Dni = dto.Dni.Trim(),
                FechaNacimiento = dto.FechaNacimiento!.Value,
                Telefono = dto.Telefono,
                Email = dto.Email,
                ObraSocial = dto.ObraSocial
            };

            await _unitOfWork.Pacientes.AddAsync(paciente);
        }
        else
        {
            // Paciente existente: sus datos no se modifican (se corrigen desde Administración > Pacientes)
            if (await _unitOfWork.Turnos.IsPacienteBusyAsync(paciente.Id, dto.FechaHora))
                throw new BusinessException("Ya tiene un turno solicitado en ese horario.");
        }

        var turno = new Turno
        {
            FechaHora = dto.FechaHora,
            DoctorId = dto.DoctorId,
            Paciente = paciente,
            Motivo = dto.Motivo,
            Estado = EstadoTurno.Pendiente
        };

        await _unitOfWork.Turnos.AddAsync(turno);

        // Una sola transacción: guarda el paciente (si es nuevo) y el turno juntos
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TurnoDTO>(turno);
    }

    public async Task<bool> UpdateAsync(int id, TurnoDTO dto)
    {
        var turno = await _unitOfWork.Turnos.GetByIdAsync(id);

        if (turno == null)
            return false;

        if (await _unitOfWork.Doctores.GetByIdAsync(dto.DoctorId) == null)
            throw new BusinessException("El doctor seleccionado no existe.");

        if (dto.FechaHora != turno.FechaHora && dto.FechaHora < DateTime.Now)
            throw new BusinessException("No se puede mover un turno a una fecha y hora pasada.");

        if (await _unitOfWork.Turnos.IsDoctorBusyAsync(dto.DoctorId, dto.FechaHora, id))
            throw new BusinessException("El doctor ya tiene un turno asignado en ese horario.");

        if (await _unitOfWork.Turnos.IsPacienteBusyAsync(turno.PacienteId, dto.FechaHora, id))
            throw new BusinessException("El paciente ya tiene un turno asignado en ese horario.");

        // Solo se modifican los datos del turno: el paciente y el estado no cambian
        turno.FechaHora = dto.FechaHora;
        turno.DoctorId = dto.DoctorId;
        turno.Motivo = dto.Motivo;
        turno.Observaciones = dto.Observaciones;

        _unitOfWork.Turnos.Update(turno);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var turno = await _unitOfWork.Turnos.GetByIdAsync(id);

        if (turno == null)
            return false;

        _unitOfWork.Turnos.Delete(turno);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
