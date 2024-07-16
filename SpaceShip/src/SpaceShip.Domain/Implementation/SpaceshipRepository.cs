using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
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

        public Ship Create(Guid id)
        {
            var newSpaceship = new Ship();

            _context.Add(newSpaceship);
            _context.SaveChanges();

            return newSpaceship;
        }
    }
}
