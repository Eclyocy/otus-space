using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;

namespace SpaceShip.Domain.Implementation;

/// <inheritdoc cref="IResourceRepository"/>
public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
{
    #region constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public ResourceRepository(DatabaseContext context)
        : base(context)
    {
    }

    #endregion
}
