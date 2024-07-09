using DataLayer.Abstrations;
using DataLayer.DTO;
using DataLayer.EfCore;
using Spaceship.DataLayer.EfClasses;


namespace DataLayer.Implementation
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
                var newProblem = new Problem() { Name = name };

                _context.Add(newProblem);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public ProblemDataDTO Create(string name)
        {
            var check = FindByName(name);

            if (!check)
            {
                var newProblem = new Problem() { Name = name };
                var newDTO = new ProblemDataDTO() { Name = name };

                _context.Add(newProblem);
                _context.SaveChanges();
                return newDTO;
            }

            throw new Exception("This problem is already in the database");
        }
    }
}
