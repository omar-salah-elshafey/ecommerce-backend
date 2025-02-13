using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetGovernorates
{
    public record GetGovernoratesQuery : IRequest<List<GovernorateDto>>;
}
