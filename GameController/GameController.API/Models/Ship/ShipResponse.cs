namespace GameController.Controllers.Models.Ship
{
    /// <summary>
    /// Model for space ship response.
    /// </summary>
    public class ShipResponse
    {
        /// <summary>
        /// Space ship ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Space ship current day.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Space ship resources.
        /// </summary>
        public List<ShipResourceResponse> Resources { get; set; }
    }
}
