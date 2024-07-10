using System;
using System.Collections.Generic;

namespace SpaceShip.WebAPI.Models;

/// <summary>
/// Модель ответа о состоянии корабля
/// </summary>
public class SpaceShipMetricResponse
{
    /// <summary>
    /// Id корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер хода
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<ResourceResponse>? Resources { get; set; }
}
