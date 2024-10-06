﻿using SpaceShip.Domain.Enums;

namespace SpaceShip.Notifications.Models;

/// <summary>
/// Модель ресурса корабля.
/// </summary>
public class SignalRResource
{
    /// <summary>
    /// Id ресурса.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование ресурса.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Resource amount.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Resource type.
    /// </summary>
    public ResourceType ResourceType { get; set; }

    /// <summary>
    /// Состояние ресурса.
    /// </summary>
    public ResourceState State { get; set; }
}
