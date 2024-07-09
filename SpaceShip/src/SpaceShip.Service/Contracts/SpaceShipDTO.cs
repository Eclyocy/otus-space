namespace SpaceShip.Service.Contracts;

public class SpaceShipDTO
{
    /// <summary>
    /// Идентификатор корабля
    /// </summary>
    public Guid Id {get;set;}

    /// <summary>
    /// Номер шага игры
    /// </summary>
    public short Step {get;set;}

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<ResourceDTO>? Resources {get;set;}

}