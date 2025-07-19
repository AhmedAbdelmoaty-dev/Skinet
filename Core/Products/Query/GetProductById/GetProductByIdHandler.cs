using Application.Contracts.Repositories;
using Application.Exceptions;
using Application.Products.Dtos;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Products.Query.GetProductById
{
    internal class GetProductByIdHandler:IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        public GetProductByIdHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if(product == null)
            {
                throw new NotFoundResourceException(nameof(product),request.Id);
            }
            return _mapper.Map<ProductDto>(product);
        }
    }
    
}
