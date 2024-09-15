using AutoMapper;
using SpaceShip.Notifications.Models;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Notifications.Mappers;

/// <summary>
/// Mapper profile for spaceship metrics
/// </summary>
public class ShipMetricsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShipMetricsProfile"/> class.
    /// </summary>
    public ShipMetricsProfile()
    {
        CreateMap<ResourceStateDTO, ResourceState>().ReverseMap();
        CreateMap<ResourceDTO, Resource>().ReverseMap();
        CreateMap<SpaceShipDTO, SpaceShipMetricsNotification>().ReverseMap();
    }
}
