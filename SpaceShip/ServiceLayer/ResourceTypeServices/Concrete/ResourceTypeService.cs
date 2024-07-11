

using DataLayer.Abstrations;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.ResourceTypeServices.Concrete
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
            var ResourceType = _resourceTypeRepository.Create(model.Name);

            return new ResourceTypeDTO { Name = model.Name };
        }
    }
}
