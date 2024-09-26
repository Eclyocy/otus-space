using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces;

/// <summary>
/// Interface to process SpaceShip events.
/// </summary>
public interface IGameStepService
{
    /// <summary>
    /// Process Day the SpaceShip with <paramref name="spaceshipId"/>.
    /// </summary>
    public Task<SpaceShipDTO> ProcessNewDayAsync(Guid spaceshipId);
}
