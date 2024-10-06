using Microsoft.EntityFrameworkCore;
using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;

namespace SpaceShip.Domain.Implementation;

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

    #region public methods

    /// <inheritdoc/>
    public override Ship? Get(Guid id)
    {
        return Context.Spaceships
            .Include(x => x.Resources)
            .FirstOrDefault(x => x.Id == id);
    }

    #endregion
}
