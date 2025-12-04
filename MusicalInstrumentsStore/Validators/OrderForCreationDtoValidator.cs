using FluentValidation;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Validators
{
    public class OrderForCreationDtoValidator : AbstractValidator<OrderForCreationDto>
    {
        public OrderForCreationDtoValidator() 
        {
            RuleFor(x => x.ShippingDetails.Address)
                .NotEmpty().WithMessage("Address is required")
                .Length(3, 200).WithMessage("Address's length must be between 3 and 200");
        }
    }
}
