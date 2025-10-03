namespace AbySalto.Junior.Core.ModelDtos
{
    public class DeleteProductResponseDto
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public Guid DeletedProductId { get; set; }
    }
}
