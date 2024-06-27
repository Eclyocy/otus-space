namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with space ships.
    /// </summary>
    public interface IShipService
    {
        /// <summary>
        /// Create a ship.
        /// </summary>
        Task<Guid> CreateShipAsync();
    }
}
