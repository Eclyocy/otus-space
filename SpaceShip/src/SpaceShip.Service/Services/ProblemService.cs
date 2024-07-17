using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IMapper _mapper;

        public ProblemService(IProblemRepository problemRepository, IMapper mapper)
        {
            _problemRepository = problemRepository;
            _mapper = mapper;
        }

        public ProblemDTO Create(ProblemDTO problemDTO)
        {
            return _mapper.Map<ProblemDTO>(_problemRepository.Create(problemDTO.Name));
        }
    }
}
