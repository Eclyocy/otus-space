using FluentValidation;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.Validations.ProblemValidations
{
    public class ProblemValidator : AbstractValidator<ProblemDTO>
    {
        public ProblemValidator()
        {
            RuleFor(problem => problem.Name).NotEmpty();
        }
    }
}
