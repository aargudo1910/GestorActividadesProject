using AutoMapper;
using GestorActividades.Entidades;
using GestorActividades.DTOs.Actividade;

namespace GestorActividades.Mapeos
{
    public class MapeoActividadProfile : Profile
    {
        public MapeoActividadProfile()
        {
            CreateMap<Actividade, ActividadDto>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToDateTime(TimeOnly.MinValue)));

            CreateMap<CrearActividadDto, Actividade>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Fecha)));

            CreateMap<ActualizarActividadDto, Actividade>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Fecha)));
        }
    }
}
