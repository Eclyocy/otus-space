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
        #region constructor

        /// <summary>
        /// Конструктор
        /// </summary>
        public Ship()
        {
            Id = Guid.NewGuid();
            Step = 0;
            State = SpaceshipState.Start;
            Resources = new List<Resource>()
            {
                new Resource()
                {
                    Name = "Armor",
                    Amount = 10,
                    State = ResourceState.Start
                },
                new Resource()
                {
                    Name = "Fuel",
                    Amount = 4,
                    State = ResourceState.Start
                },
                new Resource()
                {
                    Name = "Water",
                    Amount = 6,
                    State = ResourceState.Start
                }
            };
        }

        #endregion

        #region public properties

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
        public virtual List<Resource>? Resources { get; set; }

        #endregion
    }
}
