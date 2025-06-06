using Domain.Entites;
namespace Application.Contracts.Repositories
{
    public interface IProductRepository
    {
        public  Task<IReadOnlyList<Product>> GetAllProductsAsync(string? brand,string? type,string? sort );
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<IReadOnlyList<string>> GetTypesAsync();
        public Task<IReadOnlyList<string>> GetBrandsAsync();
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
        bool ProductExists(int id);
        Task <bool> SaveChangesAsync();
    }
}
