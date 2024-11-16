using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Infrastructure.Data.Context;
using DotnetPgSQL.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace DotnetPgSQL.Infrastructure.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            Orders = new OrderRepository(_context);
            Products = new ProductRepository(_context);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
