using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetUserAddresses
{
    public record GetUserAddressesQuery : IRequest<List<AddressDto>>;
}
