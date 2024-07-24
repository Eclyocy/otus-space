using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceTypeRepository : BaseRepository<ResourceType>, IResourceTypeRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceTypeRepository(EfCoreContext context)
            : base(context)
        {
        }

        #endregion
    }
}
