namespace AbySalto.Junior.Core.Models
{
    public class OrderStatus
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string StatusName { get; set; }
        public string Description { get; set; }
    }
}
