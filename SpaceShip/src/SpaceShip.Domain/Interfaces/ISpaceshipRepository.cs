using SpaceShip.Domain.Entities;

namespace SpaceShip.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория космических кораблей.
/// </summary>
public interface ISpaceshipRepository : IRepository<Ship>
{
}
