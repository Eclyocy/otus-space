using Microsoft.Extensions.Logging;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Mock;

public class MockSpaceShipService:IShipService
{
    private ILogger _logger;
    public MockSpaceShipService(ILogger logger)
    {
        _logger = logger;
    }

    public Guid CreateShip()
    {
        _logger.LogInformation("New request to create spaceship");

        var shipId = Guid.NewGuid();
        _logger.LogInformation("Spaceship with Id = {0} successfully created", shipId);
        
        return shipId;
    }

}

