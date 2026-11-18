
using AutoMapper;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;
using TpFinal.Core.Entities;
using TpFinal.Core.Interfaces;

namespace TpFinal.BL.Services;

public class PacienteService : IPacienteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PacienteService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PacienteDTO>> GetAllAsync()
    {
        var pacientes = await _unitOfWork.Pacientes.GetAllAsync();
        return _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);
    }

    public async Task<PacienteDTO?> GetByIdAsync(int id)
    {
        var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);

        return paciente == null
            ? null
            : _mapper.Map<PacienteDTO>(paciente);
    }

    public async Task<PacienteDTO> CreateAsync(PacienteDTO dto)
    {
        if (await _unitOfWork.Pacientes.ExistsDniAsync(dto.Dni))
            throw new BusinessException("Ya existe un paciente con ese DNI.");

        var paciente = _mapper.Map<Paciente>(dto);
        paciente.Id = 0;

        await _unitOfWork.Pacientes.AddAsync(paciente);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PacienteDTO>(paciente);
    }

    public async Task<bool> UpdateAsync(int id, PacienteDTO dto)
    {
        var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);

        if (paciente == null)
            return false;

        if (await _unitOfWork.Pacientes.ExistsDniAsync(dto.Dni, id))
            throw new BusinessException("Ya existe otro paciente con ese DNI.");

        _mapper.Map(dto, paciente);
        paciente.Id = id;

        _unitOfWork.Pacientes.Update(paciente);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);

        if (paciente == null)
            return false;

        if (await _unitOfWork.Pacientes.HasTurnosAsync(id))
            throw new BusinessException("No se puede eliminar el paciente porque tiene turnos asociados.");

        _unitOfWork.Pacientes.Delete(paciente);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
