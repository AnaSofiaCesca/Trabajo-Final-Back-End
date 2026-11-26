
using AutoMapper;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;
using TpFinal.BL.Rules;
using TpFinal.Core.Entities;
using TpFinal.Core.Interfaces;

namespace TpFinal.BL.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDTO>> GetAllAsync()
    {
        var doctores = await _unitOfWork.Doctores.GetAllAsync();
        return _mapper.Map<IEnumerable<DoctorDTO>>(doctores);
    }

    public async Task<DoctorDTO?> GetByIdAsync(int id)
    {
        var doctor = await _unitOfWork.Doctores.GetByIdAsync(id);

        return doctor == null
            ? null
            : _mapper.Map<DoctorDTO>(doctor);
    }

    public async Task<DoctorDTO> CreateAsync(DoctorDTO dto)
    {
        ValidarEspecialidad(dto.Especialidad);

        if (await _unitOfWork.Doctores.ExistsMatriculaAsync(dto.Matricula))
            throw new BusinessException("Ya existe un doctor con esa matrícula.");

        var doctor = _mapper.Map<Doctor>(dto);
        doctor.Id = 0;

        await _unitOfWork.Doctores.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DoctorDTO>(doctor);
    }

    public async Task<bool> UpdateAsync(int id, DoctorDTO dto)
    {
        ValidarEspecialidad(dto.Especialidad);

        var doctor = await _unitOfWork.Doctores.GetByIdAsync(id);

        if (doctor == null)
            return false;

        if (await _unitOfWork.Doctores.ExistsMatriculaAsync(dto.Matricula, id))
            throw new BusinessException("Ya existe otro doctor con esa matrícula.");

        _mapper.Map(dto, doctor);
        doctor.Id = id;

        _unitOfWork.Doctores.Update(doctor);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var doctor = await _unitOfWork.Doctores.GetByIdAsync(id);

        if (doctor == null)
            return false;

        if (await _unitOfWork.Doctores.HasTurnosAsync(id))
            throw new BusinessException("No se puede eliminar el doctor porque tiene turnos asociados.");

        _unitOfWork.Doctores.Delete(doctor);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static void ValidarEspecialidad(string especialidad)
    {
        if (!Especialidades.EsValida(especialidad))
        {
            throw new BusinessException(
                "La especialidad seleccionada no es válida.");
        }
    }
}
