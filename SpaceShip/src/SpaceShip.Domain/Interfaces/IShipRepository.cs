using SpaceShip.Domain.DTO;

namespace SpaceShip.Domain.Interfaces;

/// <summary>
/// Интерфейс для работы с сущностью корабля (CrUD)
/// </summary>
public interface IShipRepository : IRepository<SpaceShipModelDto>
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

    /// <summary>
    /// Метод переключающий шаг: тут списываются ресурсы и меняется номер текущего шага
    /// </summary>
    /// <param name="id">Id крабля</param>
    /// <returns>DTO модели с метриками корабля</returns>
    public SpaceShipModelDto NextDay(Guid id);
}
