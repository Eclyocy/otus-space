namespace GameController.Services.Models.Ship
{
    /// <summary>
    /// Enum for <see cref="ShipResourceDto"/> states.
    /// </summary>
    public enum ShipResourceStateDto
    {
        /// <summary>
        /// Resource is dormant.
        /// </summary>
        Sleep,

        /// <summary>
        /// Resource is in normal state.
        /// </summary>
        Normal,

        /// <summary>
        /// There is an issue with the resource.
        /// </summary>
        Fail
    }
}
