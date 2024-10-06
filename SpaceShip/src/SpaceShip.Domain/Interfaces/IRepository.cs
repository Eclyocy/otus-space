namespace SpaceShip.Domain.Interfaces;

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
    T Create(bool saveChanges = false);

    /// <summary>
    /// Create entity.
    /// </summary>
    /// <returns>Created entity.</returns>
    T Create(T entity, bool saveChanges = false);

    /// <summary>
    /// Get all entities.
    /// </summary>
    /// <returns>List of entities.</returns>
    List<T> GetAll();

    /// <summary>
    /// Get entity by <paramref name="id"/>.
    /// </summary>
    /// <returns>Entity if found, otherwise null.</returns>
    T? Get(Guid id);

    /// <summary>
    /// Update entity.
    /// </summary>
    /// <remarks>Updates <paramref name="entity"/> in-place.</remarks>
    void Update(T entity, bool saveChanges = false);

    /// <summary>
    /// Delete entity by <paramref name="id"/>.
    /// </summary>
    /// <returns>The indication of operation success.</returns>
    bool Delete(Guid id, bool saveChanges = false);

    /// <summary>
    /// Save all changes.
    /// </summary>
    void SaveChanges();
}
