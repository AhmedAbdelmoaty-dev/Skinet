using MediatR;
using System.Net;

namespace Application.Products.Commands.CreateProduct
{
    internal class CreateProductCommand:IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }
        public string PictureUrl { get; set; }
    }
}
