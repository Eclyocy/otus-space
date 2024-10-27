namespace SpaceShip.Service.Helpers.Abstractions;

/// <summary>
/// Interface provide name generation functionality.
/// </summary>
public interface INameGenerator
{
    /// <summary>
    /// Create name string.
    /// </summary>
    /// <returns>Name string.</returns>
    string Get();
}
