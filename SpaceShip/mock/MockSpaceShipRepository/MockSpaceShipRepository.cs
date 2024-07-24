using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace MockSpaceShip.Repository;

/// <summary>
/// Заглушка репозитория корабля для работы без БД
/// </summary>
public class MockSpaceShipRepository : ISpaceshipRepository
{
    /// <summary>
    /// Словарь для хранения кораблей
    /// Ключ - Id корабля
    /// </summary>
    private readonly Dictionary<Guid, Ship> _repository;

    /// <summary>
    /// Конструктор <see cref="MockSpaceShipRepository"/> class.
    /// </summary>
    public MockSpaceShipRepository() => _repository = new Dictionary<Guid, Ship>
    {
    };

    /// <summary>
    /// Создание корабля (в минимальной конфигурации)
    /// </summary>
    /// <returns>Метрики корабля</returns>
    public Ship Create(Ship entity)
    {
        var ship = new Ship();
        ship.Id = Guid.NewGuid(); // какого??? это должно быть в репозитории!
        _repository.Add(ship.Id, ship);
        return ship;
    }

    public bool Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Возвращает корабль
    /// </summary>
    /// <param name="id">Id корабля</param>
    /// <returns>True если корабль найден, False в противном случае</returns>
    public bool FindById(Guid id)
    {
        return _repository.TryGetValue(id, out Ship? ship);
    }

    /// <summary>
    /// Получить существующий корабль
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>Модель корабля</returns>
    /// <exception cref="KeyNotFoundException">Если корабля нет в БД</exception>
    public Ship Get(Guid id)
    {
        if (!_repository.TryGetValue(id, out Ship? ship))
        {
            throw new KeyNotFoundException();
        }

        return ship ?? throw new KeyNotFoundException();
    }

    public List<Ship> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Обновить существующий корабль
    /// </summary>
    /// <param name="ship">Новая сущность</param>
    /// <returns>Модель корабля</returns>
    public Ship Update(Ship ship)
    {
        _repository.Add(ship.Id, ship);
        return ship;
    }

    void IRepository<Ship>.Update(Ship entity)
    {
        throw new NotImplementedException();
    }
}
