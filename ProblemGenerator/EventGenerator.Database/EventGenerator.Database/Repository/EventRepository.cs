using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;

namespace EventGenerator.Database.Repository
{
    /// <summary>
    /// Event repository.
    /// </summary>
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EventRepository(EventDBContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
