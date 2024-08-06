using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    /// <summary>
    /// Spaceship repository.
    /// </summary>
    public class SpaceshipRepository : BaseRepository<Ship>, ISpaceshipRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SpaceshipRepository(EfCoreContext context)
            : base(context)
        {
        }

        #endregion
    }
}
