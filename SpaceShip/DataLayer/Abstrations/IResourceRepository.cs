using Spaceship.DataLayer.EfClasses;

namespace DataLayer.Abstrations
{
    public interface IResourceRepository
    {
        public ResourceDataDTO Create(int id);
        public bool FindById(int id);
    }
}
