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

        // Mapeo de Turno (entidad -> DTO): aplana los datos de Doctor y Paciente
        CreateMap<Turno, TurnoDTO>()
            .ForMember(dest => dest.DoctorNombreCompleto,
                opt => opt.MapFrom(src => src.Doctor.Apellido + ", " + src.Doctor.Nombre))
            .ForMember(dest => dest.PacienteNombreCompleto,
                opt => opt.MapFrom(src => src.Paciente.Apellido + ", " + src.Paciente.Nombre));
    }
}
