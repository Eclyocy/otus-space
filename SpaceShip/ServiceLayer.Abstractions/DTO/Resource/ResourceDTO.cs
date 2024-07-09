

using ServiceLayer.Abstractions.ReturnResult;
using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class ResourceDTO : IDTO
    {
        public int Id { get; set; }
        public Guid SpaceshipId { get; set; }
        public int ResourceTypeId { get; set; }
        public ResourceStateDTO State { get; set; }
        public int Amount { get; set; }

        public ResourceDTO()
        {
                
        }
    }
}
