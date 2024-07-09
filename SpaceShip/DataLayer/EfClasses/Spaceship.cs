using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class Spaceship
    {
        public Guid Id { get; set; }
        public SpaceshipState State { get; set; }
        public int ThisDay { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
