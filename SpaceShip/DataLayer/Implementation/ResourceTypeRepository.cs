
using DataLayer.Abstrations;
using DataLayer.EfCore;

namespace DataLayer.Implementation
{
    public class ResourceTypeRepository : IResourceTypeRepository
    {
        private EfCoreContext _context;

        public ResourceTypeRepository(EfCoreContext context)
        {
            _context = context;
        }

        public int Create(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
