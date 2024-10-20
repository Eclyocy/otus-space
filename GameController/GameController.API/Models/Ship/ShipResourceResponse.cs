﻿namespace GameController.Controllers.Models.Ship
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
        /// Resource amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Resource type.
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// Resource state.
        /// </summary>
        public string State { get; set; }
    }
}
