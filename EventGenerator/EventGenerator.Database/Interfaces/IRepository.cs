﻿namespace EventGenerator.Database.Interfaces
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
        /// <remarks>Updates <paramref name="entity"/> in-place.</remarks>
        void Update(T entity);
    }
}
