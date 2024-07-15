using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public ResourceDTO Create(ResourceDTO model)
        {
            var resource = _resourceRepository.Create();

            return default;
        }
    }
}
