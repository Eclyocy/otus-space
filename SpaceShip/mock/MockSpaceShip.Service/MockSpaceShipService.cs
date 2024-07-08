using Microsoft.Extensions.Logging;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Contracts;


namespace MockSpaceShip.Service;

public class MockSpaceShipService : IShipService
{
    public MockSpaceShipService()
    {

    }

    public SpaceShipDTO CreateShip()
    {
        return new SpaceShipDTO
        {
            Id = Guid.NewGuid(),
            Step = 0
        };
    }

    public SpaceShipDTO Get(Guid Id)
    {
        SpaceShipDTO ship = new SpaceShipDTO
        {
            Id = Id,
            Step = 0,
            Resources = [
                new() {Id = Guid.NewGuid(),Name = "Engine",State = "Normal"},
                new() {Id = Guid.NewGuid(),Name = "Body",State = "Normal"},
            ]
        };

        return ship;
    }

}

