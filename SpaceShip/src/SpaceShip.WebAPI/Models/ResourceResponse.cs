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
    required public string Name { get; set; }

    /// <summary>
    /// Состояние ресурса (спит, норма, проблема).
    /// </summary>
    required public ResourceStateResponse State { get; set; } = ResourceStateResponse.Sleep;
}
