using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;

namespace EventGenerator.Database.Repository
{
    /// <summary>
    /// Event repository.
    /// </summary>
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventRepository(EventDBContext databaseContext)
            : base(databaseContext)
        {
        }

        #endregion
    }
}
