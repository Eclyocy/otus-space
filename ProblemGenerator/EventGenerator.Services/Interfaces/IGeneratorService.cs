using EventGenerator.Services.Models.Generator;

namespace EventGenerator.Services.Interfaces
{
    /// <summary>
    /// Interface for working with event generator.
    /// </summary>
    public interface IGeneratorService
    {
        /// <summary>
        /// Create a generator.
        /// </summary>
        GeneratorDto CreateGenerator(CreateGeneratorDto createGeneratorDto);

        /// <summary>
        /// Get a generator.
        /// </summary>
        GeneratorDto GetGenerator(Guid generatorId);
    }
}
