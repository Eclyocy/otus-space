using Spaceship.DataLayer.EfClasses.State;

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

        public Guid Id { get; set; }
        public SpaceshipState State { get; set; }
        public short Step { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
