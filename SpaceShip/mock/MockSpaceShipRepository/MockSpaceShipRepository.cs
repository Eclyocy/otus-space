// using SpaceShip.Domain.DTO;
using SpaceShip.Domain.DTO;
using SpaceShip.Domain.Interfaces;

namespace MockSpaceShip.Repository;

/// <summary>
/// Заглушка репозитори корабля для работы без БД
/// </summary>
public class MockSpaceShipRepository : IStepChange
{
    /// <summary>
    /// Словарь для хранения кораблей
    /// Ключ - Id корабля
    /// </summary>
    private readonly Dictionary<Guid, SpaceShipDto> _repository;

    /// <summary>
    /// Конструктор <see cref="MockSpaceShipRepository"/> class.
    /// </summary>
    public MockSpaceShipRepository() => _repository = new Dictionary<Guid, SpaceShipDto> { };

    /// <summary>
    /// Создание корабля (в минимальной конфигурации)
    /// </summary>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDto Create()
    {
        var ship = new SpaceShipDto() { };
        _repository.Add(ship.Id, ship);
        return ship;
    }

    /// <summary>
    /// Возвращает корабль
    /// </summary>
    /// <param name="id">Id корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDto FindById(Guid id)
    {
        if (_repository.TryGetValue(id, out SpaceShipDto? ship))
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
    public SpaceShipDto NextDay(Guid id)
    {
        if (_repository.TryGetValue(id, out SpaceShipDto? ship))
        {
            ship.Step++;
            _repository[id] = ship;
            return ship;
        }

        throw new KeyNotFoundException("Cannot find spaceship by given Id");
    }
}
