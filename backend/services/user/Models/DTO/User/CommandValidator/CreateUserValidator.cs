using FluentValidation;

namespace Users.Models.DTO.User.CommandValidator;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(p => p.UserID)
               .NotEmpty().WithMessage("{UserID} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{UserID} must not exceed 50 characters.");

    }
}
