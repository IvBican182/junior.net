namespace AbySalto.Junior.Core.Models
{
    public class PaymentType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PaymentMethodName { get; set; }
    }
}
