

using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.SpaceshipServices
{
    public interface ISpaceshipService
    {
        public SpaceshipDTO Create(SpaceshipDTO model);
    }
}
