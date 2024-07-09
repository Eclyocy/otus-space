using System.ComponentModel.DataAnnotations;

namespace SpaceShip.Services.Queue;

/// <summary>
/// Interface for working with messages queue
/// </summary>
public interface IEventConsumer
{
    Object readEvent();
}