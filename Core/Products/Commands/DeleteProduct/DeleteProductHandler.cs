using Application.Contracts.Repositories;
using Application.Exceptions;
using Domain.Entites;
using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IGenericRepository<Product> _repository;
        public DeleteProductHandler(IGenericRepository<Product> repo)
        {
            _repository = repo;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }
    }
}
