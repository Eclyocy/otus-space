using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpaceShip.Domain.Model.State;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Ресурсы.
    /// </summary>
    [Table("Resource")]
    public class Resource : BaseEntity
    {
        /// <summary>
        /// SpaceshipId.
        /// </summary>
        [Column("SpaceshipId")]
        [Required]
        public Guid SpaceshipId { get; set; }

        /// <summary>
        /// ResourceType.
        /// </summary>
        [Column("ResourceType")]
        [Required]
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Статус ресурса.
        /// </summary>
        [Column("State")]
        [Required]
        public ResourceState State { get; set; }

        /// <summary>
        /// Название ресурса.
        /// </summary>
        [Column("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        [Column("Amount")]
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// Виртуальное свойство корабля.
        /// </summary>
        [ForeignKey("SpaceshipId")]
        public virtual Ship Spaceship { get; set; }
    }
}
