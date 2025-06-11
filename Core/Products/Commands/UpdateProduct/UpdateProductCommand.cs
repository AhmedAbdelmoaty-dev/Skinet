using MediatR;


namespace Application.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommand:IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string PictureUrl { get; set; }
    }
}
