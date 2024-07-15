using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces
{
    public interface IResourceService
    {
        public ResourceDTO Create(ResourceDTO model);
    }
}
