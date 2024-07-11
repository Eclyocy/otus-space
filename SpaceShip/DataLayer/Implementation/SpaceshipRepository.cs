using DataLayer.Abstrations;
using DataLayer.EfCore;
using Spaceship.DataLayer.EfClasses;

namespace DataLayer.Implementation
{
    public class SpaceshipRepository : ISpaceshipRepository
    {
        private EfCoreContext _context;

        public SpaceshipRepository(EfCoreContext context)
        {
            _context = context;
        }

        public bool FindById(Guid id)
        {
            var spaceship = _context.Spaceships
              .Where(spaceship => spaceship.Id == id);

            if (spaceship == null)
            {
                return true;
            }

            return false;
        }

        public SpaceshipDataDTO Create(Guid id)
        {
            var check = FindById(id);

            if (!check)
            {
                var newSpaceship = new Spaceship.DataLayer.EfClasses.Spaceship();
                var newDTO = new SpaceshipDataDTO();

                _context.Add(newSpaceship);
                _context.SaveChanges();

                return newDTO;
            }

            throw new Exception("This spaceship is already in the database");
        }
    }
}
