using SpaceShip.Domain.Model.State;

namespace SpaceShip.Service.Contracts;

/// <summary>
/// Состояние корабля (метрики)
/// </summary>
public class SpaceShipDTO
{
    /// <summary>
    /// Идентификатор корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер шага игры
    /// </summary>
    public short Step { get; set; }

    /// <summary>
    /// Состояние корабля
    /// </summary>
    public SpaceshipState State { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public ICollection<ResourceDTO> Resources { get; set; }
}
