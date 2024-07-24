using SpaceShip.Domain.Model.State;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Космического корабля.
    /// </summary>
    public class Ship
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
        /// Ship ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя корабля.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Статус корабля.
        /// </summary>
        public SpaceshipState State { get; set; }

        /// <summary>
        /// Ход.
        /// </summary>
        public short Step { get; set; }

        /// <summary>
        /// Коллекция ресурсов.
        /// </summary>
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
