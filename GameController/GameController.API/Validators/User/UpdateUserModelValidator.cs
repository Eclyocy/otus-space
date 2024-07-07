﻿using FluentValidation;
using GameController.API.Models.User;

namespace GameController.API.Validators.User
{
    /// <summary>
    /// Validator for <see cref="CreateUserModel"/>.
    /// </summary>
    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        /// <summary>
        /// Verify that required properties are provided.
        /// </summary>
        public UpdateUserModelValidator()
        {
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrEmpty(x.Password))
                .WithMessage("Either name or password must be specified.")
                .WithName(nameof(UpdateUserModel));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name must not be empty.")
                .When(x => x.Name is not null);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("User password must not be empty.")
                .When(x => x.Password is not null);
        }
    }
}