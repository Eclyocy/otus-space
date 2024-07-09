using AutoMapper;
using SpaceShip.Service.Contracts;
using SpaceShip.WebAPI.Models;

namespace SpaceShip.WebAPI.Mappers;

/// <summary>
/// Профиль автомаппера для сущности "Корабль"
/// </summary>
public class SpaceShipMappingProfile : Profile
{
    /// <summary>
    /// Конструктор, при создании описываем настройки маппинга 
    /// </summary>
    public SpaceShipMappingProfile()
    {
        CreateMap<ResourceStateDTO, ResourceState>().ReverseMap();
        CreateMap<ResourceDTO, Resource>().ReverseMap();
        CreateMap<SpaceShipDTO,SpaceShipMetricResponse>().ReverseMap();

        CreateMap<SpaceShipDTO, SpaceShipCreateResponse>()
            .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id));
    }
}
