using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    internal class CreateProductHandler:IRequestHandler<CreateProductCommand, int>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;   
        public CreateProductHandler(IGenericRepository<Product> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

           var product= _mapper.Map<Product>(request);
            _repository.Add(product);
             int id= await _repository.SaveChangesAsync();
            return id;
        }
    }
    
}
