using AbySalto.Junior.Core.Interfaces;
using AbySalto.Junior.Core.ModelDtos;
using AbySalto.Junior.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            var created = await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(Guid id, [FromBody] Product product)
        {
            try
            {
                var updated = await _productService.UpdateProduct(id, product);
                return Ok(updated);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteProductResponseDto>> Delete(Guid id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result.Success)
                return NotFound(result.Error);

            return Ok(result);
        }
    }
}
