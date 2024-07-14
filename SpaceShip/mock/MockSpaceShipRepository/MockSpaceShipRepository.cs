// using SpaceShip.Domain.DTO;
using SpaceShip.Domain.DTO;
using SpaceShip.Domain.Interfaces;

namespace MockSpaceShip.Repository;

/// <summary>
/// Заглушка репозитори корабля для работы без БД
/// </summary>
public class MockSpaceShipRepository : IShipRepository
{
    /// <summary>
    /// Словарь для хранения кораблей
    /// Ключ - Id корабля
    /// </summary>
    private readonly Dictionary<Guid, SpaceShipModelDto> _repository;

    /// <summary>
    /// Конструктор <see cref="MockSpaceShipRepository"/> class.
    /// </summary>
    public MockSpaceShipRepository() => _repository = new Dictionary<Guid, SpaceShipModelDto> { };

    /// <summary>
    /// Создание корабля (в минимальной конфигурации)
    /// </summary>
    /// <returns>Метрики корабля</returns>
    public SpaceShipModelDto Create()
    {
        var ship = CreateShip();
        _repository.Add(ship.Id, ship);
        return ship;
    }

    /// <summary>
    /// Возвращает корабль
    /// </summary>
    /// <param name="id">Id корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipModelDto FindById(Guid id)
    {
        if (_repository.TryGetValue(id, out SpaceShipModelDto? ship))
        {
            return ship;
        }

        throw new KeyNotFoundException("Cannot find spaceship by given Id");
    }

    /// <summary>
    /// Увеличивает шаг игры (новый день полета)
    /// </summary>
    /// <param name="id">Id корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipModelDto NextDay(Guid id)
    {
        if (_repository.TryGetValue(id, out SpaceShipModelDto? ship))
        {
            ship.Step++;
            _repository[id] = ship;
            return ship;
        }

        throw new KeyNotFoundException("Cannot find spaceship by given Id");
    }

    private static SpaceShipModelDto CreateShip()
    {
        List<ResourceModelDto> value = new List<ResourceModelDto>
        {
            new () { Id = Guid.NewGuid(), Name = "Engine", State = ResourceStateModelDto.Normal },
            new () { Id = Guid.NewGuid(), Name = "Body", State = ResourceStateModelDto.Normal }
        };

        return new SpaceShipModelDto
        {
            Id = Guid.NewGuid(),
            Step = 0,
            Resources = value
        };
    }
}
