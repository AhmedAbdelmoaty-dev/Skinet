using Application.Contracts.Repositories;
using Application.Products.Dtos;
using Application.Specification;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Skinet.RequestHelpers;

namespace Application.Products.Query.GetAllProducts
{
    internal class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductDto>>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Pagination<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            //IReadOnlyList<Product> products;
            //if ((request.SpecParams.Brands.Any()) || (request.SpecParams.Types.Any()) || !string.IsNullOrEmpty(request.SpecParams.sort))
            //{
            //    var spec =new ProductSpecification(request.SpecParams);
            //     products= await _repository.GetAllWithSpecAsync(spec);
            //}
            //else
            //{
            //    products = await _repository.GetAllAsync();
            //}
            var spec = new ProductSpecification(request.SpecParams);
            var products = await _repository.GetAllWithSpecAsync(spec);
            var ProductsDtoList=  _mapper.Map<IReadOnlyList <ProductDto>>(products);
            var Count= await _repository.CountAsync(spec);
            var resultedProduct = new Pagination<ProductDto>(request.SpecParams.PageSize, request.SpecParams.PageIndex,
                Count, ProductsDtoList);
            return (resultedProduct);
        }
    }
    
}
