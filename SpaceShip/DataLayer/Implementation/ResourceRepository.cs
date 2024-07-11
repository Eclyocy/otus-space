
using DataLayer.Abstrations;
using DataLayer.DTO;
using DataLayer.EfCore;
using Spaceship.DataLayer.EfClasses;


namespace DataLayer.Implementation
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

        public ResourceDataDTO Create(int id)
        {
            var check = FindById(id);

            if (!check)
            {
                var newResource = new Resource();
                var newDTO = new ResourceDataDTO();

                _context.Add(newResource);
                _context.SaveChanges();

                return newDTO;
            }

            throw new Exception("This resource is already in the database");
        }
    }
    
}
