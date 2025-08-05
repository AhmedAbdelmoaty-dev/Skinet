
using Application.Contracts.Repositories;
using Domain.Entites;
using MediatR;

namespace Application.Payment.Queries.GetAllDeliveryMethods
{
    public class GetAllDeliveryMethodsQueryHandler : IRequestHandler<GetAllDeliveryMethodsQuery, IReadOnlyList<DeliveryMethod>>
    {
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;

        public GetAllDeliveryMethodsQueryHandler(IGenericRepository<DeliveryMethod> dmRepo)
        {
            _dmRepo = dmRepo;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> Handle(GetAllDeliveryMethodsQuery request, CancellationToken cancellationToken)
        {
          var DelveryMethods= await  _dmRepo.GetAllAsync();
            return DelveryMethods;
        }
    }
}
