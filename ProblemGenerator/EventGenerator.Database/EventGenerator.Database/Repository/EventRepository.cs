using EventGenerator.Database.Models;
using EventGenerator.Database.Interfaces;

namespace EventGenerator.Database.Repository
{
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
