using AutoMapper;
using SpaceShip.Domain.Model.State;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers
{
    /// <summary>
    /// Профиль маппера типов ресурсов из слоя БД.
    /// </summary>
    public class ResourceStateModelMappingProfile : Profile
    {
        /// <summary>
        /// Конструктор, при создании описываем настройки маппинга.
        /// </summary>
        public ResourceStateModelMappingProfile()
        {
            // Database models -> Service models
            CreateMap<ResourceState, ResourceStateDTO>().ReverseMap();
        }
    }
}
