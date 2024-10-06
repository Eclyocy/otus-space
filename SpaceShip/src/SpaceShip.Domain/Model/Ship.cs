using System.ComponentModel.DataAnnotations.Schema;
using SpaceShip.Domain.Model.State;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Космический корабль.
    /// </summary>
    [Table("Ship")]
    public class Ship : BaseEntity
    {
        /// <summary>
        /// Имя корабля.
        /// </summary>
        [Column("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Статус корабля.
        /// </summary>
        [Column("State")]
        public SpaceshipState State { get; set; }

        /// <summary>
        /// Ход.
        /// </summary>
        [Column("Step")]
        public short? Step { get; set; }

        /// <summary>
        /// Коллекция ресурсов.
        /// </summary>
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
