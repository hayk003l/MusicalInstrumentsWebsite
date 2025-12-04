using FluentValidation;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Validators
{
    public class DescriptionForManipulationDtoValidator : AbstractValidator<DescriptionForManipulationDto>
    {
        public DescriptionForManipulationDtoValidator()
        {
            RuleFor(x => x.DescriptionText)
                .NotEmpty().WithMessage("Description text is required")
                .Length(2, 1000).WithMessage("Description text's length must be between 2 and 1000");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .Length(4, 60).WithMessage("Conutry's length must be between 4 and 60");
        }
    }
}
