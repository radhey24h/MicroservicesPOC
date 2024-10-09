using FluentValidation;

namespace Users.Models.DTO.User.CommandValidator;

public class UpdateUserPasswordValidator : AbstractValidator<UpdatePassword>
{
    public UpdateUserPasswordValidator()
    {

        RuleFor(p => p.Password)
              .NotEmpty().WithMessage("{Password} is required.")
              .NotNull()
              .MaximumLength(200).WithMessage("{Password} must not exceed 200 characters.");

    }
}
