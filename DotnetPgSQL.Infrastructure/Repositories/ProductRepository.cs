using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Domain.Models.Entities;
using DotnetPgSQL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetPgSQL.Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }
        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product); // Mark the entity as modified
            await Task.CompletedTask; 
        }
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product); // Mark the entity for deletion
            }
            await Task.CompletedTask; 
        }
    }
}
