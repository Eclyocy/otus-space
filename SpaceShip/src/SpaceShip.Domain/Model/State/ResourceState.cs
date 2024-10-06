namespace SpaceShip.Domain.Model.State
{
    /// <summary>
    /// State for Resource models.
    /// </summary>
    public enum ResourceState : byte
    {
        Start = 0,
        Sleep = 1,
        Finish = 2
    }
}
