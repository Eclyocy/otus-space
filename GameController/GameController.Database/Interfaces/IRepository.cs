namespace GameController.Database.Interfaces
{
    /// <summary>
    /// Interface for base repository.
    /// </summary>
    /// <typeparam name="T">Repository entity.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Create entity.
        /// </summary>
        /// <returns>Created entity.</returns>
        T Create(T entity);

        /// <summary>
        /// Get entity by <paramref name="id"/>.
        /// </summary>
        /// <returns>Entity if found, otherwise null.</returns>
        T? Get(Guid id);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <returns>Updated entity.</returns>
        T Update(T entity);

        /// <summary>
        /// Delete entity by <paramref name="id"/>.
        /// </summary>
        /// <returns>The indication of operation success.</returns>
        bool Delete(Guid id);
    }
}
