using SpaceShip.Domain.Model;

namespace SpaceShip.Service.Contracts;

/// <summary>
/// Ресурс корабля
/// </summary>
public class ResourceDTO
{
    /// <summary>
    /// Id ресурса
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// КОличество ресурса
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Id корабля
    /// </summary>
    public Guid SpaceshipId { get; set; }

    /// <summary>
    /// Id типа ресурса
    /// </summary>
    public Guid? ResourceTypeId { get; set; }

    /// <summary>
    /// Состояние ресурса (спит, норма, проблема)
    /// </summary>
    public required ResourceStateDTO State { get; set; }

    /// <summary>
    /// Сущность корабля
    /// </summary>
    public Ship Spaceship { get; set; }

    /// <summary>
    /// Сущность типа ресурсов
    /// </summary>
    public ResourceType ResourceType { get; set; }
}
