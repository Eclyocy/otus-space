using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;

namespace SpaceShip.Domain.Implementation;

/// <summary>
/// Resource repository.
/// </summary>
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
