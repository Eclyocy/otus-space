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
    /// Retrieve the SpaceShip by <paramref name="spaceshipId"/>.
    /// </summary>
    ShipDTO GetShip(Guid spaceshipId);

    /// <summary>
    /// Update the SpaceShip with <paramref name="spaceshipId"/>.
    /// </summary>
    ShipDTO UpdateShip(Guid spaceshipId, ShipDTO spaceShipDTO);

    /// <summary>
    /// Delete the SpaceShip with <paramref name="spaceshipId"/>.
    /// </summary>
    bool DeleteShip(Guid spaceshipId);
}
