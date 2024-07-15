using Spaceship.DataLayer.EfClasses.State;

namespace SpaceShip.Domain.Model
{
    public class Ship
    {
        public Guid Id { get; set; }
        public SpaceshipState State { get; set; }
        public short Step { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
