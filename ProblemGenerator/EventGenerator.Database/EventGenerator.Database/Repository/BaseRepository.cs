using EventGenerator.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventGenerator.Database.Repository
{
    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="T">Repository entity.</typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseRepository(EventDBContext eventDBContext)
        {
            Context = eventDBContext;
            EntitySet = Context.Set<T>();
        }

        /// <summary>
        /// Context.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// EntitySet.
        /// </summary>
        protected DbSet<T> EntitySet { get; }

        /// <inheritdoc/>
        public virtual T Create(T entity)
        {
            var entityEntry = EntitySet.Add(entity);
            Context.SaveChanges();

            return entityEntry.Entity;
        }

        /// <inheritdoc/>
        public virtual T? Get(Guid id)
        {
            return EntitySet.Find(id);
        }
    }
}
