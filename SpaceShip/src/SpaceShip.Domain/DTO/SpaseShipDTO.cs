namespace SpaceShip.Domain.DTO;

/// <summary>
/// DTO корабля
/// </summary>
public class SpaceShipDto()
{
    /// <summary>
    /// идентрификатор корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ноер шага игры (день полета)
    /// </summary>
    public int Step { get; set; }
}
