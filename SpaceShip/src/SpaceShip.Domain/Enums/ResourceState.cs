namespace SpaceShip.Domain.Enums;

/// <summary>
/// State for Resource models.
/// </summary>
public enum ResourceState : byte
{
    /// <summary>
    /// Normal state.
    /// </summary>
    /// <remarks>
    /// Resource can work as expected.<br/>
    /// Resource consumes other resources in this state.
    /// </remarks>
    OK = 0,

    /// <summary>
    /// Hibernation.
    /// </summary>
    /// <remarks>
    /// Resource can work as expected.<br/>
    /// Resource does not consume other resources in this state.
    /// </remarks>
    Sleep = 1,

    /// <summary>
    /// Failure.
    /// </summary>
    /// <remarks>
    /// Resource cannot work.<br/>
    /// Resource does not consume other resources in this state.
    /// </remarks>
    Fail = 2
}
