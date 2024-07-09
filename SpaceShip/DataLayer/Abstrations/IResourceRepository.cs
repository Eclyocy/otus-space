

using Spaceship.DataLayer.EfClasses.State;

namespace DataLayer.Abstrations
{
    public interface IResourceRepository
    {
        public int Create(int id, ResourceState state, string name,int amount);
    }
}
