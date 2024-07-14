using AutoMapper;
using SpaceShip.Domain.DTO;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Domain.Mappers;

/// <summary>
/// Профиль автомаппера для сущности "Корабль".
/// </summary>
public class SpaceShipModelMappingProfile : Profile
{
    /// <summary>
    /// Конструктор, при создании описываем настройки маппинга.
    /// </summary>
    public SpaceShipModelMappingProfile()
    {
        CreateMap<ResourceStateDTO, ResourceStateModelDto>().ReverseMap();
        CreateMap<ResourceDTO, ResourceModelDto>().ReverseMap();
        CreateMap<SpaceShipDTO, SpaceShipModelDto>().ReverseMap();
    }
}
