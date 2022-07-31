using Catalog.API.Model.Entities;
using Catalog.API.Model.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseController
    {
        private IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("GetProductById/{id}")]

        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var prodcut = await _repository.GetProductById(id);

            if (prodcut == null) return NotFound();

            return Ok(prodcut);
        }

        [HttpGet("GetProductByCategory/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductBycategory(string category)
        {
            var products = await _repository.GetProductBycategory(category);
            return Ok(products);
        }

        [HttpPost("CreateProduct")]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            _repository.CreateProduct(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            var existingProduct = _repository.GetProductById(id);

            if(existingProduct == null) return NotFound();

            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }
    }
}
