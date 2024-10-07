using Microsoft.EntityFrameworkCore;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;

namespace SpaceShip.Domain.Implementation;

/// <inheritdoc cref="IShipRepository"/>
public class ShipRepository : BaseRepository<Ship>, IShipRepository
{
    #region constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public ShipRepository(DatabaseContext context)
        : base(context)
    {
    }

    #endregion

    #region public methods

    /// <summary>
    /// Get space ship with related resources included
    /// by its <paramref name="id"/>.
    /// </summary>
    public override Ship? Get(Guid id)
    {
        return Context.Ships
            .Include(x => x.Resources)
            .FirstOrDefault(x => x.Id == id);
    }

    #endregion
}
