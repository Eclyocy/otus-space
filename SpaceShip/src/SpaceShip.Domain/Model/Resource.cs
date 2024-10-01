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
        public Guid SpaceshipId { get; set; }

        /// <summary>
        /// Resource type.
        /// </summary>
        [Column("Type")]
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Статус ресурса.
        /// </summary>
        [Column("State")]
        public ResourceState State { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        [Column("Amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Виртуальное свойство корабля.
        /// </summary>
        [ForeignKey("SpaceshipId")]
        public virtual Ship Spaceship { get; set; }

        /// <summary>
        /// Тип ресурса, требуемый для поддержания жизнедеятельности ресурса.
        /// </summary>
        public ResourceType? RequiredResourceType
        {
            get
            {
                return ResourceType switch
                {
                    ResourceType.Engine => ResourceType.Fuel,
                    ResourceType.Fuel => null,
                    _ => null
                };
            }
        }
    }
}
