using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    public class ResourceTypeService : IResourceTypeService
    {
        private readonly IResourceTypeRepository _resourceTypeRepository;

        public ResourceTypeService(IResourceTypeRepository resourceTypeRepository)
        {
            _resourceTypeRepository = resourceTypeRepository;
        }

        public ResourceTypeDTO Create(ResourceTypeDTO model)
        {
            var resourceType = _resourceTypeRepository.Create(model.Name);

            return new ResourceTypeDTO { Name = model.Name };
        }
    }
}
