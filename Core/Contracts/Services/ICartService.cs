using Domain.Entites;
namespace Application.Contracts.Services
{
    public interface ICartService
    {
         Task<ShoppingCart> GetCartAsync(string key);
        Task<ShoppingCart> SetCartAsync(ShoppingCart cart);
        Task<bool> DeleteCartAsync(string key);
    }
}
