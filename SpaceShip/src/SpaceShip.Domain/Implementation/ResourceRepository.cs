using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceRepository : IResourceRepository
    {
        private EfCoreContext _context;

        public ResourceRepository(EfCoreContext context)
        {
            _context = context;
        }

        public bool FindById(int id)
        {
            var resources = _context.Resources
              .Where(resources => resources.Id == id);

            if (resources == null)
            {
                return true;
            }

            return false;
        }

        public Resource Create()
        {
            var newResource = new Resource();

            _context.Add(newResource);
            _context.SaveChanges();

            return newResource;
        }
    }
}
