
using DataLayer.Abstrations;
using DataLayer.EfCore;
using Spaceship.DataLayer.EfClasses.State;

namespace DataLayer.Implementation
{
    public class ResourceRepository : IResourceRepository
    {
        private EfCoreContext _context;

        public ResourceRepository(EfCoreContext context)
        {
            _context = context;
        }

        public int Create(int id, ResourceState state, string name, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
