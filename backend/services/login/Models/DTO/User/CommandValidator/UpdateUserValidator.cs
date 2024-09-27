using FluentValidation;
namespace login.Models.DTO.User.CommandValidator;

public class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{FirstName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{FirstName} must not exceed 50 characters.");
    }
}
