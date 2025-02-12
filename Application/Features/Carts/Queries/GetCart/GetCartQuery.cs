using Application.Features.Carts.Dtos;
using MediatR;

namespace Application.Features.Carts.Queries.GetCart
{
    public record GetCartQuery : IRequest<CartDto>;
}
