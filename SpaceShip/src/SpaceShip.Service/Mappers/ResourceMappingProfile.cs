using AutoMapper;
using SpaceShip.Domain.Entities;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers;

/// <summary>
/// Профиль автомаппера модели ресурсов корабля уровня БД.
/// </summary>
public class ResourceMappingProfile : Profile
{
    /// <summary>
    /// Конструктор, при создании описываем настройки маппинга.
    /// </summary>
    public ResourceMappingProfile()
    {
        // Database models -> Service models
        CreateMap<Resource, ResourceDTO>();
    }
}
