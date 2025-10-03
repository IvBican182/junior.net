using AbySalto.Junior.Core.Constants;
using AbySalto.Junior.Core.Interfaces;
using AbySalto.Junior.Core.ModelDtos;
using AbySalto.Junior.Core.Models;
using AbySalto.Junior.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace AbySalto.Junior.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync(Guid userId)
        {
            var userOrders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentType)
                .Include(o => o.Currency)
                .ToListAsync();

            return userOrders;
        }
        public async Task<Order> GetOrderByIdAsync(Guid orderId, Guid userId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentType)
                .Include(o => o.Currency)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with id {orderId} not found");

            return order;
        }

        public async Task<Order> CreateOrderAsync(Guid userId, CreateOrderDto newOrder)
        {
            if (newOrder == null || !newOrder.Products.Any())
                throw new ArgumentException("No products provided");

            var order = new Order
            {
                UserId = userId,
                OrderStatusId = OrderStatusConstant.Active,
                CreatedAt = DateTime.Now,
                OrderItems = new List<OrderItem>(),
                Comment = newOrder.UserComment,
                Contact = newOrder.Contact,
                DeliveryAddress = newOrder.DeliveryAddress,
                
            };

            var currencyExist = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == newOrder.CurrencyId);
            if (currencyExist == null)
            {
                throw new KeyNotFoundException("chosen currency doesnt exist in database");
            }
            order.Currency = currencyExist;

            var paymentMethodExist = await _context.PaymentTypes.FirstOrDefaultAsync(c => c.Id == newOrder.PaymentTypeId);
            if (paymentMethodExist == null)
            {
                throw new KeyNotFoundException("chosen currency doesnt exist in database");
            }
            order.PaymentType = paymentMethodExist;

            foreach (var productDto in newOrder.Products)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productDto.ProductId);
                if (product == null)
                    throw new KeyNotFoundException($"Product with id {productDto.ProductId} not found");

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = productDto.quantity,
                    Price = product.ProductPrice * productDto.quantity
                });
            }

            order.OrderTotal = order.OrderItems.Sum(oi => oi.Price);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order> UpdateOrderStatusAsync(Guid orderId, Guid statusId, Guid userId)
        {
            var order = await GetUserOrderByIdAsync(orderId, userId);

            if (order == null)
                throw new KeyNotFoundException($"Order with id {orderId} not found for this user");

            var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(os => os.Id == statusId);

            if (orderStatus == null)
            {
                throw new KeyNotFoundException("Order status not available");
            }

            order.OrderStatusId = statusId;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<decimal> CalculateOrderTotalAsync(Guid orderId, Guid userId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with id {orderId} not found for this user");

            return order.OrderTotal;
        }

        public async Task<List<Order>> SortOrdersByTotal(Guid userId, bool userInput)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            if (userInput == true)
            {
                return orders.OrderBy(o => o.OrderTotal).ToList();
            }
            else 
            {
                return orders.OrderByDescending(o => o.OrderTotal).ToList();
            }

        }

        //helper metoda za dohvaćanje po useru, trebalo bi u drugi folder vjerojtano, ali nemam baš vremena više
        private async Task<Order> GetUserOrderByIdAsync(Guid orderId, Guid userId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with id {orderId} not found for this user");

            return order;
        }
    }
}
