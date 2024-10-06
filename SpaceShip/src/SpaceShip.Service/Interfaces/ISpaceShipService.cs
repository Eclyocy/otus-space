using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces;

/// <summary>
/// Interface for working with SpaceShip.
/// </summary>
public interface ISpaceShipService
{
    /// <summary>
    /// Create a SpaceShip.
    /// </summary>
    public SpaceShipDTO CreateShip();

    /// <summary>
    /// Retrieve the SpaceShip by <paramref name="spaceshipId"/>.
    /// </summary>
    public SpaceShipDTO GetShip(Guid spaceshipId);

    /// <summary>
    /// Update the SpaceShip with <paramref name="spaceshipId"/>.
    /// </summary>
    SpaceShipDTO UpdateShip(Guid spaceshipId, SpaceShipDTO spaceShipDTO);

    /// <summary>
    /// Delete the SpaceShip with <paramref name="spaceshipId"/>.
    /// </summary>
    bool DeleteShip(Guid spaceshipId);
}
