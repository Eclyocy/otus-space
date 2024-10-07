using SpaceShip.Domain.Enums;

namespace SpaceShip.Notifications.Models;

/// <summary>
/// Модель ответа о состоянии корабля
/// </summary>
public class SignalRShip
{
    /// <summary>
    /// Id корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Space ship name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер хода
    /// </summary>
    public short Day { get; set; }

    /// <summary>
    /// Space ship state.
    /// </summary>
    public ShipState State { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<SignalRResource> Resources { get; set; }
}
