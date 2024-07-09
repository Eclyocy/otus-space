using FluentValidation;
using Spaceship.DataLayer.EfClasses;
using System.Security.AccessControl;

namespace ServiceLayer.Validations.ResourceTypeValidations
{
    public class ResourceValidator : AbstractValidator<ResourceDTO>
    {
        public ResourceValidator()
        {
            RuleFor(resource => resource.Amount < 0).NotEmpty();
        }
    }
}
