using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
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

            return new ProblemDTO { Name = problem.Name };
        }
    }
}
