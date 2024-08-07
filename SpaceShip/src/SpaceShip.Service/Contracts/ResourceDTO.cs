namespace SpaceShip.Service.Contracts;

/// <summary>
/// Ресурс корабля
/// </summary>
public class ResourceDTO
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
    public required ResourceStateDTO State { get; set; }
}
