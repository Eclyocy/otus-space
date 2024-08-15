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
