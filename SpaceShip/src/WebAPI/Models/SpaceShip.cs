using System.Collections.Generic;

namespace WebAPI.Models;

/// <summary>
/// Модель ответа о состоянии корабля
/// </summary>
public class SpaceShip
{
    /// <summary>
    /// Id корабля
    /// </summary>
    public int Id {get;set;}
    
    /// <summary>
    /// Номер хода
    /// </summary>
    public int Step {get;set;}

    /// <summary>
    /// Коллекция ресурсов корабля
    /// </summary>
    public List<Resource>? Resources {get;set;}
}