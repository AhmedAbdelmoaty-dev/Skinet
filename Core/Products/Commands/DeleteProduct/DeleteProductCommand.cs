using MediatR;
namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand:IRequest
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
