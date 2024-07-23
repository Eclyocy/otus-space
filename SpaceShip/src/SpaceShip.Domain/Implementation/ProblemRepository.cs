using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ProblemRepository : BaseRepository<Problem>, IProblemRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProblemRepository(EfCoreContext context)
            : base(context)
        {
        }

        #endregion

        #region public methods

        /// <summary>
        /// Метод создания проблемы.
        /// </summary>
        /// <returns>Проблема</returns>
        public Problem Create(string name)
        {
            var newProblem = new Problem() { Name = name };

            Context.Add(newProblem);
            Context.SaveChanges();

            return newProblem;
        }

        /// <summary>
        /// Метод возвращает иформацию по существующей проблеме.
        /// </summary>
        /// <returns>Модель проблемы</returns>
        public Problem Get(int id)
        {
            return Context.Problems.Find(id) ?? throw new Exception("Spaceship not found");
        }

        /// <summary>
        /// Обновить существующую проблему.
        /// </summary>
        /// <param name="problem">новая модель проблемы</param>
        /// <returns>обновленная модель проблемы</returns>
        /// <exception cref="Exception">Проблема не найдена</exception>
        public Problem Update(Problem problem)
        {
            Context.Problems.Update(problem);
            Context.SaveChanges();

            return Context.Problems.Find(problem.Id) ?? throw new Exception("Problem not found");
        }

        /// <summary>
        /// Выборка корабля по id
        /// </summary>
        /// <param name="id">ID проблемы</param>
        /// <returns>true если корабль есть в БД</returns>
        public Problem? Delete(Guid id)
        {
            var problem = Context.Problems.Find(id);
            var prb = Context.Problems.Remove(problem);
            Context.SaveChanges();

            return problem;
        }

        #endregion
    }
}
