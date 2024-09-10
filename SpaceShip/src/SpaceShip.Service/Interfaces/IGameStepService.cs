namespace SpaceShip.Service.Interfaces;

public interface IGameStepService
{
    /// <summary>
    /// Process Day the SpaceShip with <paramref name="spaceshipId"/>.
    /// </summary>
    public Task ProcessNewDayAsync(Guid spaceshipId);
}
