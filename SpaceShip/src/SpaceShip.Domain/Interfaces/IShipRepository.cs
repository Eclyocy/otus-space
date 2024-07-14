using SpaceShip.Domain.DTO;

namespace SpaceShip.Domain.Interfaces;

/// <summary>
/// Интерфейс для работы с сущностью корабля (CrUD)
/// </summary>
public interface IShipRepository
{
    /// <summary>
    /// Метод создания корабля
    /// </summary>
    /// <returns>DTO модели с метриками корабля</returns>
    public SpaceShipModelDto Create();

    /// <summary>
    /// Метод запроса метрик
    /// </summary>
    /// <param name="id">Id корабля</param>
    /// <returns>DTO метрик корабля</returns>
    public SpaceShipModelDto FindById(Guid id);
}
