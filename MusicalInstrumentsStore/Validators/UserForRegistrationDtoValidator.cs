using FluentValidation;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Validators
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator() 
        { 
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname is required")
                .Length(2, 50).WithMessage("Firsname length must be between 2 and 50");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname is required")
                .Length(2, 50).WithMessage("Lastname length must be between 2 and 50");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1-14}$").WithMessage("Invalid phone number format");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must contain minumum 8 characters")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number");
        }
    }
}
