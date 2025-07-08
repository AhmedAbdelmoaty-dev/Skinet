using Application.Contracts.Repositories;
using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        
        public ProductRepository(AppDbContext context):base(context)
        {
        }
        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await _context.Products.Select(p=>p.Brand).Distinct().ToListAsync();
        }
        public async Task<IReadOnlyList<string>> GetAllTypesAsync()
        {
            return await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
        }
    }
}
