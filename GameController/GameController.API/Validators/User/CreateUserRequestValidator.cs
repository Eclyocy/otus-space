using FluentValidation;
using GameController.API.Models.User;

namespace GameController.API.Validators.User
{
    /// <summary>
    /// Validator for <see cref="CreateUserRequest"/>.
    /// </summary>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Verify that required properties are provided.
        /// </summary>
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name must be provided.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("User password must be provided.");
        }
    }
}
