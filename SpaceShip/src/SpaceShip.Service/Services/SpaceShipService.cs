using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Implementation;

public class SpaceShipService : IShipService
{

    public SpaceShipService()
    {}
    
    /// <summary>
    /// Создать новый корабль с ресурсами
    /// </summary>
    /// <returns>ID корабля</returns>
    public SpaceShipDTO CreateShip() // <--- Тупо отдать ID или всю структуру?
    {
        return new SpaceShipDTO
        {
            Id = Guid.NewGuid(),
            Step = 0
        };
    }

    /// <summary>
    /// Получить метрики корабля
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO Get(Guid id)
    {
        return new SpaceShipDTO
        {
            Id = Guid.NewGuid(),
            Step = 0
        }; //TODO Mapper
    }
}