namespace SpaceShip.Domain.Model;

/// <summary>
/// Класс, описывающий сущность корабля
/// </summary>
public class Ship
{
    /// <summary>
    /// Публичный конструктор <see cref="Ship"/> class.
    /// </summary>
    public Ship()
    {
        Id = Guid.NewGuid();
        Step = 0;
    }

    /// <summary>
    /// Идентификатор корабля
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Номер шага игры
    /// </summary>
    public short Step { get; set; }
}
