using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceRepository(EfCoreContext context)
            : base(context)
        {
        }

        #endregion
    }
}
