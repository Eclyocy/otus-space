﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GameController.Database.Models
{
    /// <summary>
    /// Session model.
    /// </summary>
    [Table("sessions")]
    public class Session : BaseEntity
    {
        /// <summary>
        /// User ID.
        /// </summary>
        [Column("userid")]
        public Guid UserId { get; set; }

        /// <summary>
        /// ShipId.
        /// </summary>
        [Column("shipid")]
        public Guid ShipId { get; set; }

        /// <summary>
        /// GeneratorId.
        /// </summary>
        [Column("generatorid")]
        public Guid GeneratorId { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        public User User { get; set; }
    }
}