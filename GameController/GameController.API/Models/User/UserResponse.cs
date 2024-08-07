﻿namespace GameController.API.Models.User
{
    /// <summary>
    /// User model.
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }
    }
}
