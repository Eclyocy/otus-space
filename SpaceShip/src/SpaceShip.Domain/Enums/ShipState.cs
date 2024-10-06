namespace SpaceShip.Domain.Enums;

/// <summary>
/// Space ship states.
/// </summary>
public enum ShipState : byte
{
    OK = 0,

    Adrift = 1,

    Crashed = 2,

    Arrived = 3,
}
