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
        CreateMap<ResourceStateDTO, SignalRResourceState>().ReverseMap();
        CreateMap<ResourceDTO, SignalRResource>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.ResourceType.ToString()))
            .ReverseMap();
        CreateMap<SpaceShipDTO, SignalRShip>()
            .ForMember(x => x.Day, opt => opt.MapFrom(x => x.Step))
            .ReverseMap()
            .ForMember(x => x.Step, opt => opt.MapFrom(x => x.Day));
    }
}
