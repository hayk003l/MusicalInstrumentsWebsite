using FluentValidation;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Validators
{
    public class UserForAuthenticationDtoValidator : AbstractValidator<UserForAuthenticationDto>
    {
        public UserForAuthenticationDtoValidator() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
