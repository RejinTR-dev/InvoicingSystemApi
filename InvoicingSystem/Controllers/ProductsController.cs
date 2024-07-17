using InvoiceSystemDAL;
using InvoiceSystemModel;
using InvoicingSystem.ExceptionLoger;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingSystem.Controllers
{
    // ProductsController.cs
    using System.Collections.Generic;
    using InvoiceSystemDAL;
    using InvoiceSystemModel;
    using Microsoft.AspNetCore.Mvc;

    namespace InvoicingSystem.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [ServiceFilter(typeof(GlobalExceptionFilter))]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _productService;

            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }

            [HttpGet]
            public ActionResult<IEnumerable<Product>> GetProducts()
            {
                var products = _productService.GetProducts();
                return Ok(products);
            }

            [HttpGet("{id}")]
            public ActionResult<Product> GetProduct(int id)
            {
                var product = _productService.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }

            [HttpPost]
            public ActionResult<Product> PostProduct(Product product)
            {
                var addedProduct = _productService.AddProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, addedProduct);
            }

            [HttpPut("{id}")]
            public IActionResult PutProduct(int id, Product product)
            {
                if (!_productService.UpdateProduct(id, product))
                {
                    return NotFound();
                }
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteProduct(int id)
            {
                if (!_productService.DeleteProduct(id))
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
    }

}
