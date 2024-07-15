using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    public interface IResourceTypeRepository
    {
        public ResourceType Create(string name);
        public bool FindByName(string name);
    }
}
