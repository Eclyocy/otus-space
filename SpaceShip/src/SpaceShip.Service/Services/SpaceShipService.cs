using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для работы с сущностью "Корабль".
/// </summary>
public class SpaceShipService : IShipService
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public SpaceShipService()
    {
    }

    /// <summary>
    /// Создать новый корабль с ресурсами.
    /// </summary>
    /// <returns>ID корабля</returns>
    public SpaceShipDTO CreateShip()
    {
        return new SpaceShipDTO
        {
            Id = Guid.NewGuid(),
            Step = 0
        };
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO Get(Guid id)
    {
        return new SpaceShipDTO
        {
            Id = Guid.NewGuid(),
            Step = 0
        }; // TODO Mapper
    }
}
