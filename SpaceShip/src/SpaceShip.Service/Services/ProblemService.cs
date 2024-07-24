using AutoMapper;
using GameController.Services.Exceptions;
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
        #region private fields

        private readonly IProblemRepository _problemRepository;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ProblemService(IProblemRepository problemRepository, IMapper mapper)
        {
            _problemRepository = problemRepository;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Создать новую проблему.
        /// </summary>
        /// <returns>ID корабля</returns>
        public ProblemDTO CreateProblem(ProblemDTO problemDTO)
        {
            Problem problemRequest = _mapper.Map<Problem>(problemDTO);

            Problem problem = _problemRepository.Create(problemRequest);

            return _mapper.Map<ProblemDTO>(problem);
        }

        /// <summary>
        /// Получить проблему.
        /// </summary>
        /// <returns>Метрики проблемы</returns>
        public ProblemDTO GetProblem(Guid problemId)
        {
            List<Problem> problem = _problemRepository.GetAll();

            return _mapper.Map<ProblemDTO>(problem);
        }

        /// <summary>
        /// Изменение метрик существующей проблемы.
        /// </summary>
        /// <returns>Метрики проблемы</returns>
        public ProblemDTO UpdateProblem(Guid problemId, ProblemDTO problemDTO)
        {
            Problem problem = UpdateRepositoryProblem(problemId, problemDTO);

            return _mapper.Map<ProblemDTO>(problem);
        }

        /// <summary>
        /// Удалить ресурс.
        /// </summary>
        public bool DeleteProblem(Guid problemId)
        {
            return _problemRepository.Delete(problemId);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get problem from repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the problem is not found by the repository.
        /// </exception>
        private Problem GetRepositoryProblem(Guid problemId)
        {
            Problem? problem = _problemRepository.Get(problemId);

            if (problem == null)
            {
                throw new NotFoundException($"User with ID {problemId} not found.");
            }

            return problem;
        }

        /// <summary>
        /// Update user in repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the user is not found by the repository.
        /// </exception>
        /// <exception cref="NotModifiedException">
        /// In case no changes are requested.
        /// </exception>
        private Problem UpdateRepositoryProblem(
            Guid problemId,
            ProblemDTO problemRequest)
        {
            Problem currentProblem = GetRepositoryProblem(problemId);

            bool updateRequested = false;

            if (problemRequest.Name != null && problemRequest.Name != currentProblem.Name)
            {
                updateRequested = true;
                currentProblem.Name = problemRequest.Name;
            }

            if (!updateRequested)
            {
                throw new NotModifiedException();
            }

            _problemRepository.Update(currentProblem); // updates entity in-place

            return currentProblem;
        }

        #endregion
    }
}
