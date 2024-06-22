using Microsoft.Extensions.Logging;

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

}

