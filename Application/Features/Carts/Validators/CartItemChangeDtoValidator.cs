using Application.Features.Carts.Dtos;
using FluentValidation;

namespace Application.Features.Carts.Validators
{
    public class CartItemChangeDtoValidator : AbstractValidator<CartItemChangeDto>
    {
        public CartItemChangeDtoValidator()
        {
            RuleFor(x => x.productId).NotEmpty().WithMessage("Product Id is required.");

            RuleFor(x => x.quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
