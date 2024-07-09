namespace SpaceShip.Service.Models;

public class Ship
{
    /// <summary>
    /// Идентификатор корабля
    /// </summary>
    public Guid Id {get;set;}

    /// <summary>
    /// Номер шага игры
    /// </summary>
    public short Step {get;set;}

}