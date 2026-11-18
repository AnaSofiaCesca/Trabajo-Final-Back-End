using AutoMapper;
using TpFinal.Core.Entities;
using TpFinal.BL.DTOs;

namespace TpFinal.BL.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo de Doctor
        CreateMap<Doctor, DoctorDTO>();
        CreateMap<DoctorDTO, Doctor>();

        // Mapeo de Paciente
        CreateMap<Paciente, PacienteDTO>();
        CreateMap<PacienteDTO, Paciente>();
    }
}