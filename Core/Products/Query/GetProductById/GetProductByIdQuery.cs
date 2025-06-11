using Application.Products.Dtos;
using MediatR;

namespace Application.Products.Query.GetProductById
{
    internal class GetProductByIdQuery:IRequest<ProductDto>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
    
}
