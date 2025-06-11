using Application.Contracts.Repositories;
using Application.Products.Dtos;
using Application.Specification;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Products.Query.GetAllProducts
{
    internal class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Product> products;
            if (!string.IsNullOrEmpty(request.Brand) || !string.IsNullOrEmpty(request.Type) || !string.IsNullOrEmpty(request.Sort))
            {
                var spec =new ProductSpecification(request.Brand, request.Type, request.Sort);
                 products= await _repository.GetAllWithSpecAsync(spec);
              return _mapper.Map<IReadOnlyList<ProductDto>>(products);
            }
           products= await _repository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
    }
    
}
