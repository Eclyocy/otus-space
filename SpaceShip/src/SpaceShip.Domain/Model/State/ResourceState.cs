namespace SpaceShip.Domain.Model.State
{
    /// <summary>
    /// State for Resource models.
    /// </summary>
    public enum ResourceState : byte
    {
        OK = 0,
        Sleep = 1,
        Dead = 2
    }
}
