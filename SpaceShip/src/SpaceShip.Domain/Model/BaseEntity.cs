using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Base entity.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
    }
}
