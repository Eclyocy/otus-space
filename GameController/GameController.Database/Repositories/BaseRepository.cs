using GameController.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="T">Repository entity.</typeparam>
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        #region private fields

        private readonly DbContext _context;
        private readonly DbSet<T> _entitySet;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseRepository(DatabaseContext databaseContext)
        {
            _context = databaseContext;
            _entitySet = _context.Set<T>();
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public virtual T Create(T entity)
        {
            var entityEntry = _entitySet.Add(entity);
            _context.SaveChanges();

            return entityEntry.Entity;
        }

        /// <inheritdoc/>
        public virtual T? Get(Guid id)
        {
            return _entitySet.Find(id);
        }

        /// <inheritdoc/>
        public T Update(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
