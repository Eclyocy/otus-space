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

        public Ship Create()
        {
            var newSpaceship = new Ship();

            _context.Add(newSpaceship);
            _context.SaveChanges();

            return newSpaceship;
        }

        /// <summary>
        /// Метод возвращает иформацию по существующему кораблю.
        /// </summary>
        /// <returns>Модель корабля</returns>
        public Ship Get(Guid id)
        {
               return _context.Spaceships.Find(id) ?? throw new Exception("Spaceship not found");
        }

        /// <summary>
        /// Обновить существующий корабль.
        /// </summary>
        /// <param name="ship">новая модель корабля</param>
        /// <returns>обновленная модель корабля</returns>
        /// <exception cref="Exception">Корабль не найден</exception>
        public Ship Update(Ship ship)
        {
            if (!FindById(ship.Id))
            {
                throw new Exception("Spaceship not found");
            }

            _context.Spaceships.Update(ship);

            return _context.Spaceships.Find(ship.Id) ?? throw new Exception("Spaceship not found");
        }
    }
}
