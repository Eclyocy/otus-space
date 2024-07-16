using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    public interface ISpaceshipRepository
    {
        public Ship Create(Guid id);
        public bool FindById(Guid id);
    }
}
