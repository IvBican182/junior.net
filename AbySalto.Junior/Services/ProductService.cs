using AbySalto.Junior.Core.Interfaces;
using AbySalto.Junior.Core.ModelDtos;
using AbySalto.Junior.Core.Models;
using AbySalto.Junior.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with id {id} wasn't found");

            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            var productToUpdate = await GetProductById(id);

            if (productToUpdate == null)
                throw new KeyNotFoundException($"Product with id {id} wasn't found");

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.ProductDescription = product.ProductDescription;
            productToUpdate.ProductQuantity = product.ProductQuantity;
            productToUpdate.ProductPrice = product.ProductPrice;

            await _context.SaveChangesAsync();

            return productToUpdate;
        }

        public async Task<DeleteProductResponseDto> DeleteProduct(Guid id)
        {
            var productToDelete = await GetProductById(id);

            if (productToDelete == null)
            {
                return new DeleteProductResponseDto { Success = false, Error = "product not found" };
            }

            _context.Remove(productToDelete);
            await _context.SaveChangesAsync();

            return new DeleteProductResponseDto { Success = true, DeletedProductId = productToDelete.Id };
        }
    }
}
