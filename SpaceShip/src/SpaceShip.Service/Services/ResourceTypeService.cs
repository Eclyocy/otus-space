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
    public class ResourceTypeService : IResourceTypeService
    {
        private readonly IResourceTypeRepository _resourceTypeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResourceTypeService(IResourceTypeRepository resourceTypeRepository, IMapper mapper)
        {
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать новый тип ресурса.
        /// </summary>
        /// <returns>ID корабля</returns>
        public ResourceTypeDTO Create(ResourceTypeDTO resourceTypeDTO)
        {
            return _mapper.Map<ResourceTypeDTO>(_resourceTypeRepository.Create(resourceTypeDTO.Name));
        }

        /// <summary>
        /// Получить метрики типа ресурса.
        /// </summary>
        /// <returns>Метрики ресурса</returns>
        public ResourceTypeDTO Get(ResourceTypeDTO resourceTypeDTO)
        {
            return _mapper.Map<ResourceTypeDTO>(_resourceTypeRepository.Get(resourceTypeDTO.Id));
        }

        /// <summary>
        /// Изменение метрик существующего типа ресурсов.
        /// </summary>
        /// <returns>Метрики типа ресурса</returns>
        public ResourceTypeDTO Update(ResourceTypeDTO resourceDTO)
        {
            return _mapper.Map<ResourceTypeDTO>(_resourceTypeRepository.Update(new ResourceType()
            {
                Id = resourceDTO.Id,
                Name = resourceDTO.Name
            }));
        }
    }
}
