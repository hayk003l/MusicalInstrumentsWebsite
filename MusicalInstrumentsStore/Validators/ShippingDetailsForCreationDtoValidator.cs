using FluentValidation;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Validators
{
    public class ShippingDetailsForCreationDtoValidator : AbstractValidator<ShippingDetailsForCreationDto>
    {
        public ShippingDetailsForCreationDtoValidator() 
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");

        }
    }
}
