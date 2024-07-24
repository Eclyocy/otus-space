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
        /// Entity Id.
        /// </summary>
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
    }
}
