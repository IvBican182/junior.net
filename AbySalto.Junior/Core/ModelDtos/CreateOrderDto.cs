namespace AbySalto.Junior.Core.ModelDtos
{
    public class CreateOrderDto
    {
        public string UserComment { get; set; }
        public List<AddProductDto> Products { get; set; }
        public string Contact { get; set; }
        public string DeliveryAddress { get; set; }
        public Guid PaymentTypeId { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
