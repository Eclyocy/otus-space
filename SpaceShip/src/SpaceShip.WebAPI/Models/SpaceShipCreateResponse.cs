using System;

namespace SpaceShip.WebAPI.Models;

/// <summary>
/// Ответ метода создания нового корабля
/// </summary>
public class SpaceShipCreateResponse
{
    /// <summary>
    /// Id корабля
    /// </summary>
    public Guid Id { get; set; }
}
