using Application.Contracts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Query.GetAllTypes
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IReadOnlyList<string>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllTypesHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }
        public async Task<IReadOnlyList<string>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
           return await _productRepository.GetAllTypesAsync();
        }
    }
}
