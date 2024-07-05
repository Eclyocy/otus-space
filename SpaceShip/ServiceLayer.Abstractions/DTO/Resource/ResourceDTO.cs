

using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public Guid SpaceshipId { get; set; }
        public int ResourceTypeId { get; set; }
        public ResourceState State { get; set; }
        public int Amount { get; set; }


    }
}
