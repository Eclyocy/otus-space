using AutoMapper;
using SpaceShip.Service.Contracts;
using SpaceShip.WebAPI.Models;

namespace SpaceShip.WebAPI.Mappers;

/// <summary>
/// Профиль автомаппера для сущности "Корабль".
/// </summary>
public class SpaceShipMappingProfile : Profile
{
    /// <summary>
    /// Конструктор, при создании описываем настройки маппинга.
    /// </summary>
    public SpaceShipMappingProfile()
    {
        CreateMap<ResourceDTO, ResourceResponse>().ReverseMap();
        CreateMap<ShipDTO, SpaceShipMetricResponse>().ReverseMap();

        CreateMap<ShipDTO, SpaceShipCreateResponse>()
            .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id));
    }
}
