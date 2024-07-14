using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для работы с сущностью "Корабль".
/// </summary>
public class SpaceShipService : IShipService
{
    private readonly IShipRepository _repository;

    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public SpaceShipService(IShipRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Создать новый корабль с ресурсами.
    /// </summary>
    /// <returns>ID корабля</returns>
    public SpaceShipDTO CreateShip()
    {
        return _mapper.Map<SpaceShipDTO>(_repository.Create());
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO? Get(Guid id)
    {
        return _mapper.Map<SpaceShipDTO>(_repository.FindById(id));
    }

    /// <summary>
    /// Применить новый игровой день (новый шаг)
    /// </summary>
    /// <param name="id">ID корабля</param>
    public void ProcessNewDay(Guid id)
    {
        _repository.NextDay(id);
        return;
    }
}
