using Shared.Enums;

namespace SpaceShip.Service.Contracts;

/// <summary>
/// Ресурс на корабле.
/// </summary>
public class ResourceDTO
{
    /// <summary>
    /// ID ресурса.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование ресурса.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Количество ресурса.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Тип ресурса.
    /// </summary>
    public ResourceType ResourceType { get; set; }

    /// <summary>
    /// Состояние ресурса.
    /// </summary>
    public ResourceState State { get; set; }
}
