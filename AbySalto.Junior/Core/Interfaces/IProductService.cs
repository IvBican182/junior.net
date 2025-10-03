using AbySalto.Junior.Core.ModelDtos;
using AbySalto.Junior.Core.Models;
using AbySalto.Junior.Infrastructure.Database;

namespace AbySalto.Junior.Core.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(Guid id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Guid id, Product product);
        Task<DeleteProductResponseDto> DeleteProduct(Guid id);
    }
}
