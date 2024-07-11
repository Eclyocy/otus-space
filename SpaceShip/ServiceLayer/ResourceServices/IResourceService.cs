
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.ResourceServices
{
    public interface IResourceService
    {
        public ResourceDTO Create(ResourceDTO model);
    }
}
