using DataLayer.Abstrations;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.ResourceServices.Concrete
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
            var resource = _resourceRepository.Create(model.Id);

            return new ResourceDTO { Id = model.Id };
        }
    }
}
