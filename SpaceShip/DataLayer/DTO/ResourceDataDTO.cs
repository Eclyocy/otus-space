
using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class ResourceDataDTO
    {
        public int Id { get; set; }
        public Guid SpaceshipId { get; set; }
        public int ResourceTypeId { get; set; }
        public ResourceState State { get; set; }
        public int Amount { get; set; }

        public ResourceDataDTO()
        {
                
        }
    }
}
