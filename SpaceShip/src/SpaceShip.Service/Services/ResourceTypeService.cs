using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Services.Exceptions;
using ResourceType = SpaceShip.Domain.Model.ResourceType;

namespace SpaceShip.Service.Services
{
    /// <summary>
    /// Сервис для работы с сущностью "Тип ресурса".
    /// </summary>
    public class ResourceTypeService : IResourceTypeService
    {
        #region private fields

        private readonly IResourceTypeRepository _resourceTypeRepository;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResourceTypeService(IResourceTypeRepository resourceTypeRepository, IMapper mapper)
        {
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Создать новый тип ресурса.
        /// </summary>
        /// <returns>ID типа ресурса</returns>
        public ResourceTypeDTO CreateResourceType()
        {
            ResourceType resourceType = _resourceTypeRepository.Create();

            return _mapper.Map<ResourceTypeDTO>(resourceType);
        }

        /// <summary>
        /// Получить тип ресурса.
        /// </summary>
        /// <returns>Метрики ресурса</returns>
        public ResourceTypeDTO GetResourceType(Guid resourceTypeId)
        {
            ResourceType resourceType = GetRepositoryResourceType(resourceTypeId);

            return _mapper.Map<ResourceTypeDTO>(resourceType);
        }

        /// <summary>
        /// Получить типы ресурсов.
        /// </summary>
        /// <returns>Метрики проблемы</returns>
        public ResourceTypeDTO GetResourcesType()
        {
            List<ResourceType> resourceType = _resourceTypeRepository.GetAll();

            return _mapper.Map<ResourceTypeDTO>(resourceType);
        }

        /// <summary>
        /// Изменение метрик существующего типа ресурса.
        /// </summary>
        /// <returns>Метрики типа ресурса</returns>
        public ResourceTypeDTO UpdateResourceType(Guid resourceTypeId, ResourceTypeDTO resourceTypeDTO)
        {
            Domain.Model.ResourceType resourceType = UpdateRepositoryResourceType(resourceTypeId, resourceTypeDTO);

            return _mapper.Map<ResourceTypeDTO>(resourceType);
        }

        /// <summary>
        /// Удалить тип ресурса.
        /// </summary>
        public bool DeleteResourceType(Guid resourceTypeId)
        {
            return _resourceTypeRepository.Delete(resourceTypeId);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get resource type from repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the resource type is not found by the repository.
        /// </exception>
        private ResourceType GetRepositoryResourceType(Guid resourceTypeId)
        {
            ResourceType? resourceType = _resourceTypeRepository.Get(resourceTypeId);

            if (resourceType == null)
            {
                throw new NotFoundException($"User with ID {resourceTypeId} not found.");
            }

            return resourceType;
        }

        /// <summary>
        /// Update resource type in repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the resource type is not found by the repository.
        /// </exception>
        /// <exception cref="NotModifiedException">
        /// In case no changes are requested.
        /// </exception>
        private ResourceType UpdateRepositoryResourceType(
            Guid resourceTypeId,
            ResourceTypeDTO resourceTypeRequest)
        {
            ResourceType currentResourceType = GetRepositoryResourceType(resourceTypeId);

            bool updateRequested = false;

            if (resourceTypeRequest.Name != null && resourceTypeRequest.Name != currentResourceType.Name)
            {
                updateRequested = true;
                currentResourceType.Name = resourceTypeRequest.Name;
            }

            if (!updateRequested)
            {
                throw new NotModifiedException();
            }

            _resourceTypeRepository.Update(currentResourceType); // updates entity in-place

            return currentResourceType;
        }

        #endregion
    }
}
