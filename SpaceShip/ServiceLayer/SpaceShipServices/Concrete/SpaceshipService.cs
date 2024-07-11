using DataLayer.Abstrations;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.SpaceshipServices.Concrete
{
    public class SpaceshipService : ISpaceshipService
    {
        private readonly ISpaceshipRepository _spaceshipRepository;

        public SpaceshipService(ISpaceshipRepository spaceshipRepository)
        {
            _spaceshipRepository = spaceshipRepository;
        }

        public SpaceshipDTO Create(SpaceshipDTO model)
        {
            var resource = _spaceshipRepository.Create(model.Id);

            return new SpaceshipDTO { Id = model.Id };
        }
    }
}
