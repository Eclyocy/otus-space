using SpaceShip.Domain.Entities;
using SpaceShip.Service.Contracts;

namespace SpaceShip.Service.Builder.Abstractions;

/// <summary>
/// Build spaceship with resources.
/// </summary>
public interface IShipBuilder
{
    /// <summary>
    /// Create spaceship. Ship should be stored in repository.
    /// </summary>
    /// <returns>Ship object.</returns>
    public Ship Build();

    /// <summary>
    /// Add custom resources to create request.
    /// </summary>
    /// <returns>Builder (self).</returns>
    public IShipBuilder AddResource(ResourceDTO resourceDTO);
}
