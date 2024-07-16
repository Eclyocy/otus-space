using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceTypeRepository : IResourceTypeRepository
    {
        private EfCoreContext _context;

        public ResourceTypeRepository(EfCoreContext context)
        {
            _context = context;
        }

        public bool FindByName(string name)
        {
            var resourceType = _context.ResourcesType
              .Where(resourceType => resourceType.Name.ToUpper() == name.ToUpper());

            if (resourceType == null)
            {
                return true;
            }

            return false;
        }

        public ResourceType Create(string name)
        {
            var newResourceType = new ResourceType() { Name = name };

            _context.Add(newResourceType);
            _context.SaveChanges();

            return newResourceType;
        }
    }
}
