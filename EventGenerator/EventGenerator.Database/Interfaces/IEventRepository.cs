using EventGenerator.Database.Models;

namespace EventGenerator.Database.Interfaces
{
    /// <summary>
    /// Interface for <see cref="Event"/> repository.
    /// </summary>
    public interface IEventRepository : IRepository<Event>
    {
    }
}
