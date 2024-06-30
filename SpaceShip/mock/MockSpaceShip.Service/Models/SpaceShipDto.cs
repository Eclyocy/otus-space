using System.Security.Cryptography;

namespace MockSpaceShip.Service.Models;

public class SpaceShipDto
{
    /// <summary>
    /// Id корабля
    /// </summary>
    public Guid Id {get;set;}
    
    /// <summary>
    /// Номер хода
    /// </summary>
    public int ? Step {get;set;}

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<ResourceDto> Resources {get;set;}

    public SpaceShipDto(Guid id)
    {
        Id = id;
        Step = 0;
    
        ResourceDto res = new ResourceDto{Id = Guid.NewGuid(), Name = "Engine", State = "Normal"};
        Resources.Add(res);
    }
}