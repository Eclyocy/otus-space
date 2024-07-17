using AutoMapper;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Mappers
{
    public class ResourceModelMappingProfile : Profile
    {
        /// <summary>
        /// Конструктор, при создании описываем настройки маппинга.
        /// </summary>
        public ResourceModelMappingProfile()
        {
            CreateMap<Resource, ResourceDTO>();
        }
    }
}
