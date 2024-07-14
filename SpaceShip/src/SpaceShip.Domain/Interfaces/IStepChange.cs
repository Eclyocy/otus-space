using SpaceShip.Domain.DTO;

namespace SpaceShip.Domain.Interfaces;

/// <summary>
/// Интерфейс для изменения шага игры
/// </summary>
public interface IStepChange
{
    /// <summary>
    /// Метод переключающий шаг: тут списываются ресурсы и меняется номер текущего шага
    /// </summary>
    /// <param name="id">Id крабля</param>
    /// <returns>DTO модели с метриками корабля</returns>
    public SpaceShipModelDto NextDay(Guid id);
}
