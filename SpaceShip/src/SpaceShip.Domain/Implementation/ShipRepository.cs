using SpaceShip.Domain.DTO;
using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ShipRepository : IShipRepository
    {
        private EfCoreContext _context;

        public ShipRepository(EfCoreContext context)
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
            var check = FindById(id);

            if (!check)
            {
                var newSpaceship = new Ship();

                _context.Add(newSpaceship);
                _context.SaveChanges();

                return newSpaceship;
            }

            throw new Exception("This spaceship is already in the database");
        }

        public SpaceShipModelDto Create()
        {
            throw new NotImplementedException();
        }

        SpaceShipModelDto IShipRepository.FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public SpaceShipModelDto NextDay(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
