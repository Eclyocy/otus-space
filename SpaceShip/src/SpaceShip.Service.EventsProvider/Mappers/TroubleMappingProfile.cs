using AutoMapper;
using SpaceShip.Service.EventsConsumer.Contracts;
using SpaceShip.Service.Models;

namespace SpaceShip.Service.Mappers;

/// <summary>
/// Профиль автомаппера для сообщения от генератора проблем.
/// </summary>
public class TroubleMappingProfile : Profile
{
    /// <summary>
    /// Конструктор, при создании описываем настройки маппинга.
    /// </summary>
    public TroubleMappingProfile()
    {
        CreateMap<TroubleMessageDTO, Trouble>()
            .ForMember(dest => dest.ShipId, op => op.MapFrom(src => src.ShipId))
            .ForMember(dest => dest.Level, op => op.MapFrom(src => src.EventLevel))
            .ForMember(dest => dest.Resource, op => op.MapFrom(src => src.Resource));
    }
}
