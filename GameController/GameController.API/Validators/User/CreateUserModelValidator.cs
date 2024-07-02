using FluentValidation;
using GameController.API.Models.User;

namespace GameController.API.Validators.User
{
    /// <summary>
    /// Validator for <see cref="CreateUserModel"/>.
    /// </summary>
    public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        /// <summary>
        /// Verify that required properties are provided.
        /// </summary>
        public CreateUserModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name must be provided.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("User password must be provided.");
        }
    }
}
