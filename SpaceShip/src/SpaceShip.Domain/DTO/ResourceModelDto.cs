namespace SpaceShip.Domain.DTO;

/// <summary>
/// Ресурс корабля
/// </summary>
public class ResourceModelDto
{
    /// <summary>
    /// Id ресурса
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Состояние ресурса (спит, норма, проблема)
    /// </summary>
    required public ResourceStateModelDto State { get; set; }
}
