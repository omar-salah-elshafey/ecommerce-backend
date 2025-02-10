using Application.ExceptionHandling;
using Application.Features.Categories.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null) throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
