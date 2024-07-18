using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ProblemRepository : IProblemRepository
    {
        private EfCoreContext _context;

        public ProblemRepository(EfCoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выборка проблемы по id
        /// </summary>
        /// <param name="id">ID проблемы</param>
        /// <returns>true если корабль есть в БД</returns>
        public bool FindById(int id)
        {
            var problem = _context.Problems
              .Where(prblm => prblm.Id == id);

            if (problem == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод создания проблемы.
        /// </summary>
        /// <returns>Проблема</returns>
        public Problem Create(string name)
        {
            var newProblem = new Problem() { Name = name };

            _context.Add(newProblem);
            _context.SaveChanges();

            return newProblem;
        }

        /// <summary>
        /// Метод возвращает иформацию по существующей проблеме.
        /// </summary>
        /// <returns>Модель проблемы</returns>
        public Problem Get(int id)
        {
            return _context.Problems.Find(id) ?? throw new Exception("Spaceship not found");
        }

        /// <summary>
        /// Обновить существующую проблему.
        /// </summary>
        /// <param name="problem">новая модель проблемы</param>
        /// <returns>обновленная модель проблемы</returns>
        /// <exception cref="Exception">Проблема не найдена</exception>
        public Problem Update(Problem problem)
        {
            if (!FindById(problem.Id))
            {
                throw new Exception("Problem not found");
            }

            _context.Problems.Update(problem);

            return _context.Problems.Find(problem.Id) ?? throw new Exception("Problem not found");
        }
    }
}
