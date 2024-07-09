using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class Resource
    {
        public int Id { get; set; }
        public Guid SpaceshipId { get; set; }
        public int ResourceTypeId { get; set; }
        public ResourceState State { get; set; }
        public int Amount { get; set; }

        public virtual Spaceship Spaceship { get; set; }
        public virtual ResourceType ResourceType { get; set; }

    }
}
