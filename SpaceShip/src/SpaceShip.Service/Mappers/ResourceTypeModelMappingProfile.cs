using AutoMapper;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers
{
    /// <summary>
    /// Профиль маппера типов ресурсов из слоя БД.
    /// </summary>
    public class ResourceTypeModelMappingProfile : Profile
    {
        /// <summary>
        /// Конструктор, при создании описываем настройки маппинга.
        /// </summary>
        public ResourceTypeModelMappingProfile()
        {
            CreateMap<ResourceType, ResourceTypeDTO>();
        }
    }
}
