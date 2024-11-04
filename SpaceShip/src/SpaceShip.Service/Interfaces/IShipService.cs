using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces;

/// <summary>
/// Interface for working with SpaceShip.
/// </summary>
public interface IShipService
{
    /// <summary>
    /// Create a SpaceShip.
    /// </summary>
    ShipDTO CreateShip();

    /// <summary>
    /// Retrieve the SpaceShip by <paramref name="shipId"/>.
    /// </summary>
    ShipDTO GetShip(Guid shipId);

    /// <summary>
    /// Process new day on board the SpaceShip with <paramref name="shipId"/>.
    /// </summary>
    ShipDTO ProcessNewDay(Guid shipId);

    /// <summary>
    /// Process trouble message for SpaceShip/>.
    /// </summary>
    ShipDTO ApplyFailure(Guid shipId, int problemLevel);

    /// <summary>
    /// Delete the SpaceShip with <paramref name="shipId"/>.
    /// </summary>
    bool DeleteShip(Guid shipId);
}
