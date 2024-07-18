using AutoMapper;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Services
{
    /// <summary>
    /// Сервис для работы с сущностью "Проблема".
    /// </summary>
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ProblemService(IProblemRepository problemRepository, IMapper mapper)
        {
            _problemRepository = problemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать новую проблему.
        /// </summary>
        /// <returns>ID корабля</returns>
        public ProblemDTO Create(ProblemDTO problemDTO)
        {
            return _mapper.Map<ProblemDTO>(_problemRepository.Create(problemDTO.Name));
        }

        /// <summary>
        /// Получить метрики проблемы.
        /// </summary>
        /// <returns>Метрики проблемы</returns>
        public ProblemDTO Get(ProblemDTO problemDTO)
        {
            return _mapper.Map<ProblemDTO>(_problemRepository.Get(problemDTO.Id));
        }

        /// <summary>
        /// Изменение метрик существующей проблемы.
        /// </summary>
        /// <returns>Метрики проблемы</returns>
        public ProblemDTO Update(ProblemDTO problemDTO)
        {
            return _mapper.Map<ProblemDTO>(_problemRepository.Update(new Problem()
            {
                Name = problemDTO.Name,
                Id = problemDTO.Id,
                ResourceTypes = problemDTO.ResourceTypes
            }));
        }
    }
}
