using Application.Contracts.Repositories;
using Application.Exceptions;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Products.Commands.UpdateProduct
{
    internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IGenericRepository<Product> repo,IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if(product is null)
            {
                throw new NotFoundResourceException(nameof(Product), request.Id);
            }
            _mapper.Map(request, product);
            await _repository.SaveChangesAsync();
        }
    }

}
