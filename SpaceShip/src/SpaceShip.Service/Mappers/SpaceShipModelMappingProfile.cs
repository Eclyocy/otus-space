using AutoMapper;
using SpaceShip.Domain.Model;
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
        // Database models -> Service models
        CreateMap<Ship, SpaceShipDTO>()
            .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
            .ForMember(dest => dest.Step, op => op.MapFrom(src => src.Step))
            .ForMember(dest => dest.Resources, op => op.UseDestinationValue());
    }
}
