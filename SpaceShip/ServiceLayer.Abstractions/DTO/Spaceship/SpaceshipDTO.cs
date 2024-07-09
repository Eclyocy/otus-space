using ServiceLayer.Abstractions.ReturnResult;
using Spaceship.DataLayer.EfClasses.State;

namespace Spaceship.DataLayer.EfClasses
{
    public class SpaceshipDTO : IDTO
    {
        public Guid Id { get; set; }
        public SpaceshipStateDTO State { get; set; }
        public int ThisDay { get; set; }

        public SpaceshipDTO()
        {
            
        }
    }
}
