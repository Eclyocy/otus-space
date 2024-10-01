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
    /// Количество ресурса
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Id корабля
    /// </summary>
    public Guid SpaceshipId { get; set; }

    /// <summary>
    /// Состояние ресурса
    /// </summary>
    public required ResourceStateDTO State { get; set; }

    /// <summary>
    /// Сущность корабля
    /// </summary>
    public Ship Spaceship { get; set; }

    /// <summary>
    /// Сущность типа ресурсов
    /// </summary>
    public ResourceTypeDTO ResourceType { get; set; }
}
