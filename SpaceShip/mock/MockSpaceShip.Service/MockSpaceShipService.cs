using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace MockSpaceShip.Service;

/// <summary>
/// Заглушка сервиса создания корабля, для отладки без БД
/// </summary>
public class MockSpaceShipService : IShipService
{
    /// <summary>
    /// Пустой конструктор
    /// </summary>
    public MockSpaceShipService()
    {
    }

    /// <summary>
    /// Метод создания корабля
    /// </summary>
    /// <returns>DTO метрик корабля</returns>
    public SpaceShipDTO CreateShip()
    {
        return new SpaceShipDTO
        {
            Id = Guid.NewGuid(),
            Step = 0
        };
    }

    /// <summary>
    /// Метод получения состояния корабля
    /// </summary>
    /// <param name="id">Идентификатор корабля</param>
    /// <returns>DTO метрик корабля</returns>
    public SpaceShipDTO Get(Guid id)
    {
        List<ResourceDTO> value = new List<ResourceDTO>
        {
            new () { Id = Guid.NewGuid(), Name = "Engine", State = ResourceStateDTO.Normal },
            new () { Id = Guid.NewGuid(), Name = "Body", State = ResourceStateDTO.Normal }
        };

        return new SpaceShipDTO
        {
            Id = id,
            Step = 0,
            Resources = value
        };
    }
}
