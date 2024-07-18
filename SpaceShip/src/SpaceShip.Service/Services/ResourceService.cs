using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    /// <summary>
    /// Сервис для работы с сущностью "Проблема".
    /// </summary>
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResourceService(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать новый ресурс.
        /// </summary>
        /// <returns>ID корабля</returns>
        public ResourceDTO Create(ResourceDTO resourceDTO)
        {
            return _mapper.Map<ResourceDTO>(_resourceRepository.Create());
        }

        /// <summary>
        /// Получить метрики ресурса.
        /// </summary>
        /// <returns>Метрики ресурса</returns>
        public ResourceDTO Get(ResourceDTO resourceDTO)
        {
            return _mapper.Map<ResourceDTO>(_resourceRepository.Get(resourceDTO.Id));
        }

        /// <summary>
        /// Изменение метрик существующего ресурса.
        /// </summary>
        /// <returns>Метрики ресурса</returns>
        public ResourceDTO Update(ResourceDTO resourceDTO)
        {
            return _mapper.Map<ResourceDTO>(_resourceRepository.Update(new Resource()
            {
                Id = resourceDTO.Id,
                SpaceshipId = resourceDTO.SpaceshipId,
                ResourceTypeId = resourceDTO.ResourceTypeId,
                Amount = resourceDTO.Amount,
                Name = resourceDTO.Name,
                State = (SpaceShip.Domain.Model.State.ResourceState)resourceDTO.State,
                ResourceType = resourceDTO.ResourceType,
                Spaceship = resourceDTO.Spaceship,
            }));
        }
    }
}
