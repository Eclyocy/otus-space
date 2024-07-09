

using FluentValidation;
using Spaceship.DataLayer.EfClasses;

namespace ServiceLayer.Validations.StarshipValidations
{
    internal class SpaceshipValidator : AbstractValidator<SpaceshipDTO>
    {
        public SpaceshipValidator()
        {
            RuleFor(spaceship => spaceship.ThisDay < 0 || spaceship.ThisDay > 6).NotEmpty();
        }
    }
}
