using MediatR;
namespace Application.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommand(int Id):IRequest
    {
        public int Id { get; set; }
    }
}
