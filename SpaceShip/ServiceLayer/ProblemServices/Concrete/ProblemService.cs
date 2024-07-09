using DataLayer.Abstrations;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.ProblemServices.Concrete
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;

        public ProblemService(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
        }

        public ProblemDTO Create(ProblemDTO problemDTO)
        {
            var problem = _problemRepository.Create(problemDTO.Name);

            return new ProblemDTO { Name = problemDTO.Name };
        }
    }
}
