using System;
using System.Collections.Generic;
using SpaceShip.Domain.Enums;

namespace SpaceShip.WebAPI.Models;

/// <summary>
/// Модель ответа о состоянии корабля
/// </summary>
public class SpaceShipMetricResponse
{
    /// <summary>
    /// ID корабля
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Space ship name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Номер хода
    /// </summary>
    public short Step { get; set; }

    /// <summary>
    /// Space ship state.
    /// </summary>
    public ShipState State { get; set; }

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<ResourceResponse> Resources { get; set; }
}
