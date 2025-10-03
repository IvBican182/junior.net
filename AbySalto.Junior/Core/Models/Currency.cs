namespace AbySalto.Junior.Core.Models
{
    public class Currency
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
    }
}
