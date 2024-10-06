using Microsoft.EntityFrameworkCore;
using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="T">Repository entity.</typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : BaseEntity, new()
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseRepository(EfCoreContext efCoreContext)
        {
            Context = efCoreContext;
            EntitySet = Context.Set<T>();
        }

        #endregion

        #region protected fields

        /// <summary>
        /// Context.
        /// </summary>
        protected EfCoreContext Context { get; }

        /// <summary>
        /// EntitySet.
        /// </summary>
        protected DbSet<T> EntitySet { get; }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public virtual T Create(bool saveChanges = false)
        {
            var entity = new T();

            var entityEntry = EntitySet.Add(entity);

            if (saveChanges)
            {
                Context.SaveChanges();
            }

            return entityEntry.Entity;
        }

        /// <inheritdoc/>
        public virtual T Create(T entity, bool saveChanges = false)
        {
            var entityEntry = EntitySet.Add(entity);

            if (saveChanges)
            {
                Context.SaveChanges();
            }

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
        public void Update(T entity, bool saveChanges = false)
        {
            Context.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public bool Delete(Guid id, bool saveChanges = false)
        {
            var obj = EntitySet.Find(id);
            if (obj == null)
            {
                return false;
            }

            EntitySet.Remove(obj);

            if (saveChanges)
            {
                Context.SaveChanges();
            }

            return true;
        }

        /// <inheritdoc/>
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        #endregion
    }
}
