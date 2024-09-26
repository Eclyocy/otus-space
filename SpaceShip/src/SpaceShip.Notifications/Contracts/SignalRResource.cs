namespace SpaceShip.Notifications.Models;

/// <summary>
/// Модель ресурса корабля.
/// </summary>
public class SignalRResource
{
    /// <summary>
    /// Id ресурса.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование ресурса.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Состояние ресурса (спит, норма, проблема).
    /// </summary>
    public required SignalRResourceState State { get; set; }
}
