using EventGenerator.Database.Models;

namespace EventGenerator.Database.Interfaces
{
    /// <summary>
    /// Interface for <see cref="Generator"/> repository.
    /// </summary>
    public interface IGeneratorRepository : IRepository<Generator>
    {
        /// <summary>
        /// Get entity by id.
        /// <br/>
        /// In case <paramref name="includeEvents"/> is supplied,
        /// also fetch the related events.
        /// </summary>
        Generator? Get(Guid id, bool includeEvents = false);
    }
}
