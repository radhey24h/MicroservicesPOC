using FluentValidation;

namespace login.Models.DTO.User.CommandValidator
{
    public class UpdateUserPasswordValidator : AbstractValidator<UpdatePassword>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(p => p.Id)
                   .NotEmpty().WithMessage("{UserID} is required.")
                   .NotNull();

            RuleFor(p => p.Password)
                   .NotNull().NotEmpty().WithMessage("{Password} is required.")
                   .MinimumLength(8).WithMessage("{Password} must be atleaset 8 characters long.");

            RuleFor(p => p.Password)
                .Must((f, t) => f.Password!.Any(c => char.IsLower(c))).WithMessage("{Password} must contain atleast 1 Lower case characters long.")
                .Must((f, t) => f.Password!.Any(c => char.IsUpper(c))).WithMessage("{Password} must contain atleast 1 Upper case characters long.")
                .Must((f, t) => f.Password!.Any(c => char.IsDigit(c))).WithMessage("{Password} must contain atleast 1 number.")
                .Must((f, t) => f.Password!.Any(c => char.IsSymbol(c))).WithMessage("{Password} must contain atleast 1 special character.");

        }
    }
}
