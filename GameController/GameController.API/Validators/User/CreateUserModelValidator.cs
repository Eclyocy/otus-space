using FluentValidation;
using GameController.API.Models.User;

namespace GameController.API.Validators.User
{
    public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name must be provided.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("User password must be provided.");
        }
    }
}
