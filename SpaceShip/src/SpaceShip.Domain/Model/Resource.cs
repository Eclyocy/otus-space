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
        [Column("SpaceshipId_id")]
        public Guid? SpaceshipId { get; set; }

        /// <summary>
        /// ResourceTypeId.
        /// </summary>
        [Column("ResourceType_id")]
        public Guid? ResourceTypeId { get; set; }

        /// <summary>
        /// Статус ресурса.
        /// </summary>
        [Column("State")]
        public ResourceState? State { get; set; }

        /// <summary>
        /// Название ресурса.
        /// </summary>
        [Column("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int? Amount { get; set; }

        /// <summary>
        /// Виртуцальное ствойство корабля.
        /// </summary>
        [ForeignKey("SpaceshipId")]
        public virtual Ship? Spaceship { get; set; }

        /// <summary>
        /// Виртуцальное ствойство типа ресурса.
        /// </summary>
        [ForeignKey("ResourceTypeId")]
        public virtual ResourceType? ResourceType { get; set; }
    }
}
