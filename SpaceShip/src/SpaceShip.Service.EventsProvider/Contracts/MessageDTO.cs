namespace SpaceShip.Service.Contracts;

/// <summary>
/// Структура сообщения в очереди RabbitMQ
/// </summary>
public class MessageDTO
{
    /// <summary>
    /// Идентификатор корабля
    /// </summary>
    public Guid ShipId { get; set; }

    /// <summary>
    /// Тело сообщения
    /// </summary>
    required public string Content { get; set; }
}