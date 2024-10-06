using System;

namespace SpaceShip.WebAPI.Models;

/// <summary>
/// Модель ресурса корабля.
/// </summary>
public class ResourceResponse
{
    /// <summary>
    /// Id ресурса.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование ресурса.
    /// </summary>
    public ResourceTypeResponse Name { get; set; }

    /// <summary>
    /// Состояние ресурса (спит, норма, проблема).
    /// </summary>
    public ResourceStateResponse State { get; set; }

    /// <summary>
    /// Resource amount.
    /// </summary>
    public int Amount { get; set; }
}
