using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.ResourceTypeServices
{
    public interface IResourceTypeService
    {
        public ResourceTypeDTO Create(ResourceTypeDTO model);
    }
}
