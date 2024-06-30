using MockSpaceShip.Service.Models;

namespace SpaceShip.Service;

public interface IShipService
{

    public Guid CreateShip();
    public SpaceShipDto Get(Guid Id);
}