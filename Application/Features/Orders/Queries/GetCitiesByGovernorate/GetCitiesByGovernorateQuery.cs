using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetCitiesByGovernorate
{
    public record GetCitiesByGovernorateQuery(Guid GovernorateId) : IRequest<List<CityDto>>;
}
