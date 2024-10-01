namespace SpaceShip.WebAPI.Models;

/// <summary>
/// Возможные состояния ресурсов
/// </summary>
public enum ResourceStateResponse
{
    /// <summary>
    /// Спящий режим, не потребляет другие ресурсы
    /// </summary>
    Sleep = 1,

    /// <summary>
    /// Активен, норма.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Активен, есть проблема с ресурсом
    /// </summary>
    Dead = 2
}
