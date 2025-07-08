using Application.Contracts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Query.GetAllBrands
{
    internal class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IReadOnlyList<string>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllBrandsHandler(IProductRepository repository)
        {
            _productRepository=repository;
        }
        public async Task<IReadOnlyList<string>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
          return await  _productRepository.GetBrandsAsync();
        }
    }
}

