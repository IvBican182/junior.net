namespace AbySalto.Junior.Core.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public Guid PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Contact { get; set; }
        public string Comment { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
