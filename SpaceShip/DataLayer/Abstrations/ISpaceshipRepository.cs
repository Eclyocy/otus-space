
using Spaceship.DataLayer.EfClasses.State;

namespace DataLayer.Abstrations
{
    public interface ISpaceshipRepository
    {
        public int Create(int id, SpaceshipState state, string name, int thisDay);
    }
}
