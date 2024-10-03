namespace GameController.Services.Models.Ship
{
    /// <summary>
    /// Model for resource of <see cref="ShipDto"/>.
    /// </summary>
    public class ShipResourceDto
    {
        /// <summary>
        /// Resource ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Resource name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Resource amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Resource state.
        /// </summary>
        public ShipResourceStateDto State { get; set; }
    }
}
