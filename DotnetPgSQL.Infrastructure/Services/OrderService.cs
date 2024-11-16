using AutoMapper;
using DotnetPgSQL.Application.Contracts.DTOs;
using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Application.Contracts.Services;
using DotnetPgSQL.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetPgSQL.Infrastructure.Services
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _unitOfWork.Orders.GetAllOrders();
            return orders;
        }
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _unitOfWork.Orders.GetOrderById(id);
            return order;
        }
        public async Task<Order> AddOrder(OrderDto orderDto)
        {
            // Ensure that the orderDto is not null
            ArgumentNullException.ThrowIfNull(orderDto);

            orderDto.OrderDate = orderDto.OrderDate?.ToUniversalTime();

            // Map the DTO to the entity using AutoMapper
            var order = _mapper.Map<Order>(orderDto);

            // Add the mapped order to the database via the Unit of Work pattern
            var createdOrder = await _unitOfWork.Orders.AddOrder(order);

            // Save changes to the database
            await _unitOfWork.Save();

            // Explicitly load related OrderItems and Products using GetOrderById
            var orderWithProducts = await _unitOfWork.Orders.GetOrderById(createdOrder.OrderId);

            // Return the order with the loaded product details
            return orderWithProducts;
        }
        public async Task<Order> UpdateOrder(int id, OrderDto orderDto)
        {
            // Convert OrderDate to UTC
            if (orderDto.OrderDate.HasValue)
            {
                orderDto.OrderDate = DateTime.SpecifyKind(orderDto.OrderDate.Value, DateTimeKind.Utc);
            }

            // Fetch the existing order from the repository
            var existingOrder = await _unitOfWork.Orders.GetOrderById(id);

            if (existingOrder == null)
            {
                return null;
            }

            // Map the incoming orderDto to the existing order
            _mapper.Map(orderDto, existingOrder);

            // Update the order items
            foreach (var orderItemDto in orderDto.OrderItems)
            {
                var existingOrderItem = existingOrder.OrderItems
                    .FirstOrDefault(oi => oi.OrderItemId == orderItemDto.OrderItemId);

                if (existingOrderItem != null)
                {
                    // Update existing order item
                    _mapper.Map(orderItemDto, existingOrderItem);
                }
                else
                {
                    // Add new order item
                    var newOrderItem = _mapper.Map<OrderItem>(orderItemDto);
                    existingOrder.OrderItems.Add(newOrderItem);
                }
            }

            // Update the product in the repository
            await _unitOfWork.Orders.UpdateOrder(existingOrder);

            // Save changes through UnitOfWork
            await _unitOfWork.Save();

            return existingOrder;
        }

        public async Task DeleteOrder(int id)
        {
            // Perform the delete operation
            await _unitOfWork.Orders.DeleteOrder(id);

            // Save changes through UnitOfWork
            await _unitOfWork.Save();
        }
    }
}
