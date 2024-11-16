using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Domain.Models.Entities;

namespace DotnetPgSQL.Application.Contracts.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(OrderDto order);
        Task<Order> UpdateOrder(int id, OrderDto order);
        Task DeleteOrder(int id);
    }
}
