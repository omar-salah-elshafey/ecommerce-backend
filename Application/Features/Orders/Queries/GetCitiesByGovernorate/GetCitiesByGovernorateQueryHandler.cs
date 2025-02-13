using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetCitiesByGovernorate
{
    public class GetCitiesByGovernorateQueryHandler(ICityRepository _cityRepository, IMapper _mapper)
    : IRequestHandler<GetCitiesByGovernorateQuery, List<CityDto>>
    {
        public async Task<List<CityDto>> Handle(GetCitiesByGovernorateQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetCitiesByGovernorateIdAsync(request.GovernorateId);
            return _mapper.Map<List<CityDto>>(cities);
        }
    }
}
