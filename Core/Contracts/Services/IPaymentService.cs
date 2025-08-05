using Domain.Entites;

namespace Application.Contracts.Services
{
    public interface IPaymentService
    {
        Task<ShoppingCart?> CreateOrUpdatePaymentInit(string cartId);
    }
}
