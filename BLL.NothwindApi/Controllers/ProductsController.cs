using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BLL.NothwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products != null)
            {
                return Ok(products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {

            if (product == null)
            {
                ModelState.AddModelError("", "No data");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.CreateProductAsync(product);

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody]Product product)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(id, product);
                return Ok(product);
            }

            return NotFound();
        }
    }
}
