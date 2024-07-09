using FluentValidation;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.Validations.ResourceTypeValidations
{
    public class ResourceTypeValidator : AbstractValidator<ResourceTypeDTO>
    {
        public ResourceTypeValidator()
        {
            RuleFor(resourceType => resourceType.Name).NotEmpty();
        }
    }
}
