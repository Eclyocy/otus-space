using GameController.Services.Models.Generator;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with generators.
    /// </summary>
    public interface IGeneratorService
    {
        /// <summary>
        /// Create a generator.
        /// </summary>
        Task<Guid> CreateGeneratorAsync(CreateGeneratorDto createGeneratorDto);
    }
}
