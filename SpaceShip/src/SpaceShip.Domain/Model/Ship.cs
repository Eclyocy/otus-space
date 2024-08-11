using System.ComponentModel.DataAnnotations.Schema;
using SpaceShip.Domain.Model.State;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Космического корабля.
    /// </summary>
    [Table("ship")]
    public class Ship : BaseEntity
    {
        #region constructor

        /// <summary>
        /// Конструктор
        /// </summary>
        public Ship()
        {
            Id = Guid.NewGuid();
            Step = 0;
            State = SpaceshipState.Start;
        }

        #endregion

        /// <summary>
        /// Имя корабля.
        /// </summary>
        [Column("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Статус корабля.
        /// </summary>
        [Column("state")]
        public SpaceshipState State { get; set; }

        /// <summary>
        /// Ход.
        /// </summary>
        [Column("step")]
        public short? Step { get; set; }

        /// <summary>
        /// Коллекция ресурсов.
        /// </summary>
        public virtual ICollection<Resource>? Resources { get; set; }
    }
}
