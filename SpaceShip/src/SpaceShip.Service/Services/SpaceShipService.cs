using SpaceShip.Service.Abstractions;
using SpaceShip.Service.Models;


namespace SpaceShip.Service.Implementation;

public class ShipService : IShipService
{
    public ShipService()
    {

    }

    /// <summary>
    /// Создать новый корабль с ресурсами
    /// </summary>
    /// <returns>ID корабля</returns>
    public Guid CreateShip() // <--- Тупо отдать ID или всю структуру?
    {
        return Guid.NewGuid();
    }

    /// <summary>
    /// Получить метрики корабля
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public Ship GetSpaceShip(Guid id)
    {
        return new Ship();
    }

}