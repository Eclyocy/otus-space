using FluentValidation;
using GameController.API.Models.User;

namespace GameController.API.Validators.User
{
    /// <summary>
    /// Validator for <see cref="CreateUserRequest"/>.
    /// </summary>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        /// <summary>
        /// Verify that required properties are provided.
        /// </summary>
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => x.Name is not null || x.Password is not null)
                .WithMessage("Either user name or user password must be specified.")
                .WithName(nameof(UpdateUserRequest));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name must not be empty.")
                .When(x => x.Name is not null);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("User password must not be empty.")
                .When(x => x.Password is not null);
        }
    }
}
