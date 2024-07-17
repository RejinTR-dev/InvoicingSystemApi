using InvoiceSystemBL;
using InvoiceSystemDAL;
using InvoiceSystemModel;
using InvoicingSystem.ExceptionLoger;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingSystem.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [ServiceFilter(typeof(GlobalExceptionFilter))]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = _categoryService.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            try
            {
                _categoryService.AddCategory(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, Category category)
        {
            try
            {
                _categoryService.UpdateCategory(id, category);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
