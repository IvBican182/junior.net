using AbySalto.Junior.Core.Interfaces;
using AbySalto.Junior.Core.ModelDtos;
using AbySalto.Junior.Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Order>>> GetOrders(Guid userId)
        {
            var orders = await _orderService.GetOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{userId}/{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid userId, Guid orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId, userId);
                return Ok(order);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrder(Guid userId, [FromBody] CreateOrderDto newOrder)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(userId, newOrder);

                var orderDto = new OrderDto
                {
                    Id = order.Id,
                    Comment = order.Comment,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.ProductName,
                        ProductPrice = oi.Price,
                        Quantity = oi.Quantity
                    }).ToList()
                };

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{userId}/{orderId}/status/{statusId}")]
        public async Task<ActionResult<Order>> UpdateStatus(Guid userId, Guid orderId, Guid statusId)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(orderId, statusId, userId);
                return Ok(updatedOrder);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{userId}/{orderId}/total")]
        public async Task<ActionResult<decimal>> GetOrderTotal(Guid userId, Guid orderId)
        {
            try
            {
                var total = await _orderService.CalculateOrderTotalAsync(orderId, userId);
                return Ok(total);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{userId}/sort")]
        public async Task<ActionResult<List<Order>>> SortOrders(Guid userId, [FromQuery] bool ascending)
        {
            var sortedOrders = await _orderService.SortOrdersByTotal(userId, ascending);

            return Ok(sortedOrders);
        }
    }
}
