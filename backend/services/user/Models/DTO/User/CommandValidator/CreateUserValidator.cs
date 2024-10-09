using FluentValidation;

namespace Users.Models.DTO.User.CommandValidator;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{Email} is required.")
               .NotNull()
               .MaximumLength(200).WithMessage("{Email} must not exceed 200 characters.");

    }
}
