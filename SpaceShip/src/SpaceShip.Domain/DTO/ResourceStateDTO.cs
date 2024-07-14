namespace SpaceShip.Domain.DTO;

/// <summary>
/// Возможные состояния ресурса:
/// - Sleep - доступен, не потребляет ресурсы
/// - Normal - доступен, активен, потребляет ресурсы
/// - Fail - неисправен, не потребляет ресурсы
/// </summary>
public enum ResourceStateDTO
{
    Sleep,
    Normal,
    Fail
}
