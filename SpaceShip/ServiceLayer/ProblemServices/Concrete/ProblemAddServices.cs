using DataLayer.EfCore;
using ServiceLayer.Abstractions.ReturnResult;
using ServiceLayer.Validations.ProblemValidations;
using Spaceship.DataLayer.EfClasses;
using FluentValidation.Results;


namespace ServiceLayer.ProblemServices.Concrete
{
    public class ProblemAddServices
    {
        private readonly EfCoreContext _context;

        public ProblemAddServices(EfCoreContext context)
        {
            _context = context;
        }

        ProblemValidator validator = new ProblemValidator();

        public ReturnResult<ProblemDTO> Add(ProblemDTO problemDTO)
        {
            ValidationResult result = validator.Validate(problemDTO);

            List<string> errors = new List<string>();

            foreach (var item in result.Errors)
            {
                errors.Add(item.ToString());
            }

            var problem = _context.Problems
                .Where(problem => problem.Name.ToUpper() == problemDTO.Name.ToUpper())
                .FirstOrDefault();

            if (problem != null) 
            {
                problem.Name = problemDTO.Name;
            }

            var newProblem = new Problem()
            { 
                Name = problemDTO.Name 
            };

            _context.Add(newProblem);
            _context.SaveChanges();

            return new ReturnResult<ProblemDTO>(problemDTO);
        }

    }
}
