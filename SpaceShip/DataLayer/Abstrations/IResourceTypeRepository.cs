

using Spaceship.DataLayer.EfClasses;

namespace DataLayer.Abstrations
{
    public interface IResourceTypeRepository
    {
        public ResourceTypeDataDTO Create(string name);
        public bool FindByName(string name);
    }
}
