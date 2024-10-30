namespace Shared.Enums;

/// <summary>
/// Resource states.
/// </summary>
public enum ResourceState : byte
{
    /// <summary>
    /// Normal state.
    /// </summary>
    /// <remarks>
    /// Resource is:<br/>
    /// * functional;<br/>
    /// * ready to be used;<br/>
    /// * requires life support.
    /// </remarks>
    OK = 0,

    /// <summary>
    /// Hibernation.
    /// </summary>
    /// <remarks>
    /// Resource is:<br/>
    /// * functional;<br/>
    /// * not ready to be used;<br/>
    /// * does not require life support.
    /// </remarks>
    Sleep = 1,

    /// <summary>
    /// Failure.
    /// </summary>
    /// <remarks>
    /// Resource is:<br/>
    /// * nonfunctional;<br/>
    /// * not ready to be used;<br/>
    /// * does not require life support.
    /// </remarks>
    Fail = 2,
}
