namespace SpaceShip.Domain.DTO;

/// <summary>
/// DTO корабля
/// </summary>
public class SpaceShipModelDto()
{
    /// <summary>
    /// идентификатор корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер шага игры (день полета)
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<ResourceModelDto>? Resources { get; set; }
}
