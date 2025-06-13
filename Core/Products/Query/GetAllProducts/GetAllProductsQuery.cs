using Application.Products.Dtos;
using Application.Specification;
using MediatR;
using Skinet.RequestHelpers;


namespace Application.Products.Query.GetAllProducts
{
    public class GetAllProductsQuery:IRequest<Pagination<ProductDto>>
    {
       public ProductSpecParams SpecParams { get; set; }
        public GetAllProductsQuery(ProductSpecParams specParams)
        {
           SpecParams = specParams;
        }
    }
    
}
