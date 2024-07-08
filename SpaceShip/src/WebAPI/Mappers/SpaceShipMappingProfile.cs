using System.Security.Cryptography.X509Certificates;
using AutoMapper;

using SpaceShip.Service.Contracts;
using SpaceShip.WebAPI.Models;


namespace SpaceShip.WebAPI.Mappers;

/// <summary>
/// Профиль автомаппера для сущности "Корабль"
/// </summary>
public class SpaceShipMappingProfile : Profile
{
    public SpaceShipMappingProfile()
    {
        CreateMap<ResourceDTO, Resource>().ReverseMap();
        CreateMap<SpaceShipDTO,SpaceShipMetricResponse>().ReverseMap();

        CreateMap<SpaceShipDTO, SpaceShipCreateResponse>()
            .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id));
    }
}
