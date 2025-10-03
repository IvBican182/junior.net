namespace AbySalto.Junior.Core.ModelDtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
