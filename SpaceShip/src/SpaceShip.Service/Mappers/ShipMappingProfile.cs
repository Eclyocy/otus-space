using AutoMapper;
using SpaceShip.Domain.Entities;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers;

/// <summary>
/// Профиль автомаппера для сущности "Корабль".
/// </summary>
public class ShipMappingProfile : Profile
{
    /// <summary>
    /// Конструктор, при создании описываем настройки маппинга.
    /// </summary>
    public ShipMappingProfile()
    {
        // Database models -> Service models
        CreateMap<Ship, ShipDTO>();
    }
}
