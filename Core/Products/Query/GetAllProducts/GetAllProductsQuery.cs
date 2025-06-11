using Application.Products.Dtos;
using MediatR;


namespace Application.Products.Query.GetAllProducts
{
    internal class GetAllProductsQuery:IRequest<IReadOnlyList<ProductDto>>
    {
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? Sort { get; set; }
        public GetAllProductsQuery(string brand="",string type="",string sort="")
        {
            Brand=brand;
            Type=type;
            Sort=sort;
        }
    }
    
}
