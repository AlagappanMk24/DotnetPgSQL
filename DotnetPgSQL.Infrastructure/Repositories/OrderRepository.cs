using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Domain.Models.Entities;
using DotnetPgSQL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetPgSQL.Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                        .ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                        .FirstOrDefaultAsync(o => o.OrderId == id);
        }
        public async Task<Order> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }
        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await Task.CompletedTask;
        }
        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
