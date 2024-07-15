using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Interfaces;

public interface IShipService
{
    public SpaceShipDTO CreateShip();

    public SpaceShipDTO? Get(Guid id);

    public void ProcessNewDay(Guid id);
}
