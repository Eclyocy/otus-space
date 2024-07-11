
using Spaceship.DataLayer.EfClasses;


namespace DataLayer.Abstrations
{
    public interface ISpaceshipRepository
    {
        public SpaceshipDataDTO Create(Guid id);
        public bool FindById(Guid id);
    }
}
