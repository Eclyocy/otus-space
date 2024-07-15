namespace SpaceShip.Domain;

/// <summary>
/// Класс описывающий сущность корабля
/// </summary>
public class SpaceShip
{
    public SpaceShip()
    {
        Id = Guid.NewGuid();
        Step = 0;
    }

    /// <summary>
    /// Идентификатор корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер шага игры
    /// </summary>
    public short Step { get; set; }
}
