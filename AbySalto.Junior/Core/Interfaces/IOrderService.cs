using AbySalto.Junior.Core.ModelDtos;
using AbySalto.Junior.Core.Models;

namespace AbySalto.Junior.Core.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync(Guid userId);
        Task<Order> GetOrderByIdAsync(Guid orderId, Guid userId);
        Task<Order> CreateOrderAsync(Guid userId, CreateOrderDto newOrder);
        Task<Order> UpdateOrderStatusAsync(Guid orderId, Guid statusId, Guid userId);
        Task<decimal> CalculateOrderTotalAsync(Guid orderId, Guid userId);
        Task<List<Order>> SortOrdersByTotal(Guid userId, bool userInput);
    }
}
