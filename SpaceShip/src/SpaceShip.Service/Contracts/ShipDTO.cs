using Shared.Enums;

namespace SpaceShip.Service.Contracts;

/// <summary>
/// Состояние корабля (метрики).
/// </summary>
public class ShipDTO
{
    /// <summary>
    /// Идентификатор корабля.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя корабля.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер шага игры.
    /// </summary>
    public short Step { get; set; }

    /// <summary>
    /// Состояние корабля.
    /// </summary>
    public ShipState State { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля.
    /// </summary>
    public List<ResourceDTO> Resources { get; set; }
}
