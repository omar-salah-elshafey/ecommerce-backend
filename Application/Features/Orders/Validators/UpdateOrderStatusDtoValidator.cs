using Application.Features.Orders.Dtos;
using FluentValidation;

namespace Application.Features.Orders.Validators
{
    public class UpdateOrderStatusDtoValidator : AbstractValidator<UpdateOrderStatusDto>
    {
        public UpdateOrderStatusDtoValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("Order ID Cannot be empty!");

            RuleFor(x => x.NewStatus)
                .NotEmpty().WithMessage("New Status is required.")
                .IsInEnum()
                .WithMessage("Invalid order Status.");
        }
    }
}