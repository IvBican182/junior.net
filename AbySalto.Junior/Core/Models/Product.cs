namespace AbySalto.Junior.Core.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
