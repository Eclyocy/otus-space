namespace SpaceShip.Notifications;

/// <summary>
/// Collection of SignalR notification methods.
/// </summary>
public class NotificationMethod
{
    /// <summary>
    /// Command for subscribing to a group.
    /// </summary>
    public const string Subscribe = nameof(Subscribe);

    /// <summary>
    /// Command for unsubscribing to a group.
    /// </summary>
    public const string Unsubscribe = nameof(Unsubscribe);

    /// <summary>
    /// Command for refreshing.
    /// </summary>
    public const string Refresh = nameof(Refresh);
}
