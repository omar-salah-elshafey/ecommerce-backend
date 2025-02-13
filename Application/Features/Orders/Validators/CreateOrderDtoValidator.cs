using Application.Features.Orders.Dtos;
using FluentValidation;

namespace Application.Features.Orders.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.GovernorateId).NotEmpty().WithMessage("Governorate Cannot be empty!");

            RuleFor(x => x.CityId).NotEmpty().WithMessage("City Cannot be empty!");

            RuleFor(x => x.Region).NotEmpty().WithMessage("Region Cannot be empty!");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber Cannot be empty!")
                .Matches(@"^(?:\+2)?(01(?:0|1|2|5)\d{8})$")
                .WithMessage("Phone number must be in the correct format (e.g. +2010xxxxxxx or 010xxxxxxx).");
        }
    }
}
