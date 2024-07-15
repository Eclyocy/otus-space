using GameController.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameController.Database.Repositories
{
    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="T">Repository entity.</typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseRepository(DatabaseContext databaseContext)
        {
            Context = databaseContext;
            EntitySet = Context.Set<T>();
        }

        #endregion

        #region protected fields

        /// <summary>
        /// Context.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// EntitySet.
        /// </summary>
        protected DbSet<T> EntitySet { get; }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public virtual T Create(T entity)
        {
            var entityEntry = EntitySet.Add(entity);
            Context.SaveChanges();

            return entityEntry.Entity;
        }

        /// <inheritdoc/>
        public virtual List<T> GetAll()
        {
            return EntitySet.ToList();
        }

        /// <inheritdoc/>
        public virtual T? Get(Guid id)
        {
            return EntitySet.Find(id);
        }

        /// <inheritdoc/>
        public T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        /// <inheritdoc/>
        public bool Delete(Guid id)
        {
            var obj = EntitySet.Find(id);
            if (obj == null)
            {
                return false;
            }

            EntitySet.Remove(obj);
            Context.SaveChanges();
            return true;
        }

        #endregion
    }
}
