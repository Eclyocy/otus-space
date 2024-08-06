using System.ComponentModel.DataAnnotations.Schema;
using SpaceShip.Domain.Model.State;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Ресурсы.
    /// </summary>
    [Table("resource")]
    public class Resource : BaseEntity
    {
        /// <summary>
        /// SpaceshipId.
        /// </summary>
        [Column("spaceshipId_id")]
        public Guid SpaceshipId { get; set; }

        /// <summary>
        /// ResourceTypeId.
        /// </summary>
        [Column("resourceType_id")]
        public Guid ResourceTypeId { get; set; }

        /// <summary>
        /// Статус ресурса.
        /// </summary>
        [Column("state")]
        public ResourceState State { get; set; }

        /// <summary>
        /// Название ресурса.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Виртуцальное ствойство корабля.
        /// </summary>
        [ForeignKey("spaceshipId")]
        public virtual Ship Spaceship { get; set; }

        /// <summary>
        /// Виртуцальное ствойство типа ресурса.
        /// </summary>
        [ForeignKey("resourceTypeId")]
        public virtual ResourceType ResourceType { get; set; }
    }
}
