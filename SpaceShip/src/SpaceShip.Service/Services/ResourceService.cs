using AutoMapper;
using GameController.Services.Exceptions;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    /// <summary>
    /// Сервис для работы с сущностью "Ресурса".
    /// </summary>
    public class ResourceService : IResourceService
    {
        #region private fields

        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResourceService(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Создать новый ресурс.
        /// </summary>
        /// <returns>ID ресурса</returns>
        public ResourceDTO CreateResource(ResourceDTO resourceDTO)
        {
            Resource resourceRequest = _mapper.Map<Resource>(resourceDTO);

            Resource resource = _resourceRepository.Create(resourceRequest);

            return _mapper.Map<ResourceDTO>(resource);
        }

        /// <summary>
        /// Получить ресурс.
        /// </summary>
        /// <returns>Метрики ресурса</returns>
        public ResourceDTO GetResource(Guid resourceId)
        {
            Resource resource = GetRepositoryResource(resourceId);

            return _mapper.Map<ResourceDTO>(resource);
        }

        /// <summary>
        /// Получить ресурсы.
        /// </summary>
        /// <returns>Метрики проблемы</returns>
        public ResourceDTO GetResources()
        {
            List<Resource> resource = _resourceRepository.GetAll();

            return _mapper.Map<ResourceDTO>(resource);
        }

        /// <summary>
        /// Изменение метрик существующего ресурса.
        /// </summary>
        /// <returns>Метрики ресурса</returns>
        public ResourceDTO UpdateResource(Guid resourceId, ResourceDTO resourceDTO)
        {
            Resource resource = UpdateRepositoryResource(resourceId, resourceDTO);

            return _mapper.Map<ResourceDTO>(resource);
        }

        /// <summary>
        /// Удалить ресурс.
        /// </summary>
        public bool DeleteResource(Guid resourceId)
        {
            return _resourceRepository.Delete(resourceId);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get resource from repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the resource is not found by the repository.
        /// </exception>
        private Resource GetRepositoryResource(Guid resourceId)
        {
            Resource? resource = _resourceRepository.Get(resourceId);

            if (resource == null)
            {
                throw new NotFoundException($"User with ID {resourceId} not found.");
            }

            return resource;
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
        private Resource UpdateRepositoryResource(
            Guid resourceId,
            ResourceDTO resourceRequest)
        {
            Resource currentResource = GetRepositoryResource(resourceId);

            bool updateRequested = false;

            if (resourceRequest.Name != null && resourceRequest.Name != currentResource.Name)
            {
                updateRequested = true;
                currentResource.Name = resourceRequest.Name;
            }

            if (!updateRequested)
            {
                throw new NotModifiedException();
            }

            _resourceRepository.Update(currentResource); // updates entity in-place

            return currentResource;
        }

        #endregion
    }
}
