using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    public interface IProblemRepository
    {
        public Problem Create(string name);
        public bool FindByName(string name);
    }
}
