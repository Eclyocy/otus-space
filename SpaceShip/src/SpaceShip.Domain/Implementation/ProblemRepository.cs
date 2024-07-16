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

        public bool FindByName(string name)
        {
            var problem = _context.Problems
              .Where(problem => problem.Name.ToUpper() == name.ToUpper());

            if (problem == null)
            {
                return true;
            }

            return false;
        }

        public Problem Create(string name)
        {
            var newProblem = new Problem() { Name = name };

            _context.Add(newProblem);
            _context.SaveChanges();

            return newProblem;
        }
    }
}
