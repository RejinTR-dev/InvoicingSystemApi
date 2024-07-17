using InvoiceSystemDAL;
using InvoiceSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemBL
{
    public class CategoryService
    {
        private readonly ExcelHelper _excelHelper;

        public CategoryService(string filePath)
        {
            _excelHelper = new ExcelHelper(filePath);
        }

        public List<Category> GetCategories()
        {
            return _excelHelper.ReadCategories();
        }

        public void AddCategory(Category category)
        {
            // Perform any validation here if necessary
            _excelHelper.WriteCategories(new List<Category> { category });
        }

        public void UpdateCategory(int id, Category category)
        {
            category.Id = id; // Ensure the ID in the object matches the ID in the route
            var categories = _excelHelper.ReadCategories();
            var existingCategory = categories.FirstOrDefault(c => c.Id == id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;

                _excelHelper.WriteCategories(categories);
            }
            // Optionally, throw an exception or handle if category not found
        }

        public void DeleteCategory(int id)
        {
            var categories = _excelHelper.ReadCategories();
            var existingCategory = categories.FirstOrDefault(c => c.Id == id);

            if (existingCategory != null)
            {
                categories.Remove(existingCategory);
                _excelHelper.WriteCategories(categories);
            }
            // Optionally, throw an exception or handle if category not found
        }
    }
}
