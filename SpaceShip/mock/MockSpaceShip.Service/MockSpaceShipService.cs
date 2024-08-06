using Microsoft.Extensions.Logging;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace MockSpaceShip.Service;

/// <summary>
/// Заглушка сервиса создания корабля, для отладки без БД
/// </summary>
public class MockSpaceShipService : IShipService
{
    private readonly ILogger _logger;

    private readonly List<SpaceShipDTO> _ships = new List<SpaceShipDTO>()
    {
        new SpaceShipDTO()
        {
            Id = Guid.NewGuid(),
            Step = 3,
            Resources = new List<ResourceDTO>()
            {
                new ResourceDTO() { Id = Guid.NewGuid(), Name = "Engine", State = ResourceStateDTO.Normal },
                new ResourceDTO() { Id = Guid.NewGuid(), Name = "Body", State = ResourceStateDTO.Normal }
            }
        }
    };

    /// <summary>
    /// Пустой конструктор
    /// </summary>
    public MockSpaceShipService(ILogger<MockSpaceShipService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Метод создания корабля
    /// </summary>
    /// <returns>DTO метрик корабля</returns>
    public SpaceShipDTO CreateShip()
    {
        _logger.LogInformation("Create ship");

        SpaceShipDTO ship = new SpaceShipDTO()
        {
            Id = Guid.NewGuid(),
            Step = 0
        };

        _ships.Add(ship);

        return ship;
    }

    public SpaceShipDTO CreateShip(SpaceShipDTO spaceShipDTO)
    {
        throw new NotImplementedException();
    }

    public bool DeleteShip(Guid spaceshipId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Метод получения состояния корабля
    /// </summary>
    /// <param name="id">Идентификатор корабля</param>
    /// <returns>DTO метрик корабля</returns>
    public SpaceShipDTO? Get(Guid id)
    {
        _logger.LogInformation("Get ship {shipId}", id);

        return _ships.FirstOrDefault(x => x.Id == id);
    }

    public SpaceShipDTO? GetShip(Guid spaceshipId)
    {
        throw new NotImplementedException();
    }

    public void ProcessNewDay(Guid id)
    {
        _logger.LogInformation("Process new day event for ship {shipId}", id);

        SpaceShipDTO? ship = Get(id);

        if (ship == null)
        {
            throw new Exception("No such ship");
        }

        ship.Step++;
    }

    public SpaceShipDTO UpdateShip(Guid spaceshipId, SpaceShipDTO spaceShipDTO)
    {
        throw new NotImplementedException();
    }
}
