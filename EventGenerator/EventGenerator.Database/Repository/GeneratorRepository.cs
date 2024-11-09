using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventGenerator.Database.Repository
{
    /// <summary>
    /// Generator repository.
    /// </summary>
    public class GeneratorRepository : BaseRepository<Generator>, IGeneratorRepository
    {
        private readonly EventDBContext _dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorRepository(EventDBContext databaseContext)
            : base(databaseContext)
        {
            _dbContext = databaseContext;
        }

        /// <inheritdoc/>
        public Generator? Get(Guid id, bool includeEvents = false)
        {
            if (!includeEvents)
            {
                return base.Get(id);
            }

            return _dbContext.Generators
                .Include(g => g.Events)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
