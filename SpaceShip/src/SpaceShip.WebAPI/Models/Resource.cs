using System;

namespace SpaceShip.WebAPI.Models;

/// <summary>
/// Модель ресурса корабля
/// </summary>
public class Resource
{
    /// <summary>
    /// Id ресурса
    /// </summary>
    public Guid Id {get; set;}
    
    /// <summary>
    /// Наименованеи ресурса
    /// </summary>
    public string? Name {get; set;}
    
    /// <summary>
    /// Состояние ресурса (спит, норма, проблема)
    /// </summary>
    public required ResourceState State {get; set;} = ResourceState.Sleep;

}
