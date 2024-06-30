namespace WebAPI.Models;

/// <summary>
/// Модель ресурса корабля
/// </summary>
public class Resource
{
    /// <summary>
    /// Id ресурса
    /// </summary>
    public int? Id {get; set;}
    
    /// <summary>
    /// Наименованеи ресурса
    /// </summary>
    public string? Name {get; set;}
    
    /// <summary>
    /// Состояние ресурса (спит, норма, проблема)
    /// </summary>
    public string? State {get; set;} 

}