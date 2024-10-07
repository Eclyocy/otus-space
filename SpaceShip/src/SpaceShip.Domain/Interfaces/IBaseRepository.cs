namespace SpaceShip.Domain.Interfaces;

/// <summary>
/// Base repository.
/// </summary>
/// <typeparam name="T">
/// Entity type for the repository to work with.
/// </typeparam>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Create entity via default constructor.
    /// </summary>
    /// <returns>Created entity.</returns>
    T Create(bool saveChanges = false);

    /// <summary>
    /// Create entity via model.
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
