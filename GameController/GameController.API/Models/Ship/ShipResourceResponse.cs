namespace GameController.Controllers.Models.Ship
{
    /// <summary>
    /// Model for space ship resource response.
    /// </summary>
    public class ShipResourceResponse
    {
        /// <summary>
        /// Resource name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Resource state.
        /// </summary>
        public ShipResourceStateResponse State { get; set; }
    }
}
