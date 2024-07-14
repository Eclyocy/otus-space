using GameController.Services.Models.Ship;

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

        /// <summary>
        /// Retrieve the ship by ID.
        /// </summary>
        Task<ShipDto> GetShipAsync(Guid shipId);
    }
}
