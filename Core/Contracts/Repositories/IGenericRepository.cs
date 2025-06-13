using Application.Contracts.Specifications;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetEntitySpecAsync(ISpecefication<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecefication<T> spec);
        int Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
        bool Exists(int id);
        Task<int>CountAsync(ISpecefication<T> spec);
    }
   
}
