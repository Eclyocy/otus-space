namespace GameController.Controllers.Models.Ship
{
    /// <summary>
    /// Model for space ship resource state response.
    /// </summary>
    public enum ShipResourceStateResponse
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
