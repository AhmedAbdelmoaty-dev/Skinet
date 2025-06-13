using Application.Contracts.Repositories;
using Application.Contracts.Specifications;
using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync(int id)
        {
          return await  _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetEntitySpecAsync(ISpecefication<T> spec)
        {
            return await ApplySpecefication(spec).FirstOrDefaultAsync();
        }   

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecefication<T> spec)
        {
          return await  ApplySpecefication(spec).ToListAsync();
        }

        public int  Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity.Id; 

        }

        public void Update(T entity)
        {
              
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public bool Exists(int id)
        {
           return _context.Set<T>().Any(e => e.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        private IQueryable<T> ApplySpecefication(ISpecefication<T> spec)
        {
           return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<int> CountAsync(ISpecefication<T> spec)
        {
            var query= _context.Set<T>().AsQueryable();
            query = spec.ApplyCriteria(query);
            return await query.CountAsync();
        }
    }
}
