using Microsoft.Extensions.Logging;
using MockSpaceShip.Service.Models;

namespace SpaceShip.Service;

public class MockSpaceShipService : IShipService
{
    public MockSpaceShipService()
    {

    }

    public Guid CreateShip()
    {
        //_logger.LogInformation("New request to create spaceship");

        var shipId = Guid.NewGuid();
        //_logger.LogInformation("Spaceship with Id = {shipId} successfully created", shipId);

        return shipId;
    }

    public SpaceShipDto Get(Guid Id)
    {
        SpaceShipDto ship = new SpaceShipDto(Id);
        return ship;
    }

}

