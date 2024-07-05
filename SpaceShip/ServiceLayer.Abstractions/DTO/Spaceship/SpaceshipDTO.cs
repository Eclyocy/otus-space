using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class SpaceshipDTO
    {
        public Guid Id { get; set; }
        public SpaceshipState State { get; set; }
        public int ThisDay { get; set; }

    }
}
