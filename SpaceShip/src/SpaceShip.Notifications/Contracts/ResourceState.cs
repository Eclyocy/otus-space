namespace SpaceShip.Notifications.Models;

/// <summary>
/// Возможные состояния ресурсов
/// </summary>
public enum ResourceState
{
    /// <summary>
    /// Спящий режим, не потребляет другие ресурсы
    /// </summary>
    Sleep,

    /// <summary>
    /// Активен, норма.
    /// </summary>
    Normal,

    /// <summary>
    /// Активен, есть проблема с ресурсом
    /// </summary>
    Fail
}
