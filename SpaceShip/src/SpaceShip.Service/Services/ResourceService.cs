using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    /// <summary>
    /// Resources service.
    /// </summary>
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public ResourceService(
            IResourceRepository resourceRepository,
            IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public ResourceDTO DecreaseResourceAmount(Guid resourceId)
        {
            Resource resource = _resourceRepository.Get(resourceId);
            resource.Amount--;
            _resourceRepository.Update(resource);
            return _mapper.Map<ResourceDTO>(resource);
        }

        /// <inheritdoc/>
        public ResourceDTO SetResourceStateToDead(Guid resourceId)
        {
            Resource resource = _resourceRepository.Get(resourceId);
            resource.State = Domain.Model.State.ResourceState.Dead;
            _resourceRepository.Update(resource);
            return _mapper.Map<ResourceDTO>(resource);
        }
    }
}
