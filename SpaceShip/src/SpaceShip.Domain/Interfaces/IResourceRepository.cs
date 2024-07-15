using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    public interface IResourceRepository
    {
        public Resource Create();
        public bool FindById(int id);
    }
}
