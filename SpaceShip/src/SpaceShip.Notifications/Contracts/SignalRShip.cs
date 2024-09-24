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
    /// Номер хода
    /// </summary>
    public int Day { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<SignalRResource>? Resources { get; set; }
}
