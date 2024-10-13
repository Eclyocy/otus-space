using EventGenerator.Database.Models;

namespace EventGenerator.Database.Interfaces
{
    /// <summary>
    /// Interface for <see cref="Event"/> repository.
    /// </summary>
    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Get all event of a generator.
        /// </summary>
        List<Event> GetAllByGeneratorId(Guid generatorId);
    }
}
