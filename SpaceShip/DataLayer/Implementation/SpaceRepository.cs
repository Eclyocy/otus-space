using DataLayer.Abstrations;
using DataLayer.EfCore;
using Spaceship.DataLayer.EfClasses.State;

namespace DataLayer.Implementation
{
    public class SpaceRepository : ISpaceshipRepository
    {
        private EfCoreContext _context;

        public SpaceRepository(EfCoreContext context)
        {
            _context = context;
        }

        public int Create(int id, SpaceshipState state, string name, int thisDay)
        {
            throw new NotImplementedException();
        }
    }
}
