using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetGovernorates
{
    public class GetGovernoratesQueryHandler(IGovernorateRepository _governorateRepository, IMapper _mapper)
        : IRequestHandler<GetGovernoratesQuery, List<GovernorateDto>>
    {
        public async Task<List<GovernorateDto>> Handle(GetGovernoratesQuery request, CancellationToken cancellationToken)
        {
            var governorates = await _governorateRepository.GetAllAsync();
            return _mapper.Map<List<GovernorateDto>>(governorates);
        }
    }
}
