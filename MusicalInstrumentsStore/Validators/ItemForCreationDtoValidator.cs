using FluentValidation;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Validators
{
    public class ItemForCreationDtoValidator : AbstractValidator<ItemForCreationDto>
    {
        public ItemForCreationDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Item's name is required field")
                .Length(3, 200).WithMessage("Item's name's length must be between 3 and 200");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Item's price is required")
                .InclusiveBetween(0, 10_000_000).WithMessage("Item's price must be between 0 and 10_000_000");

        }
    }
}
