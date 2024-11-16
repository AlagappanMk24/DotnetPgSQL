using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Domain.Models.Entities;

namespace DotnetPgSQL.Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
    }
}
