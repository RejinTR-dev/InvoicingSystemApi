using InvoiceSystemDAL;
using InvoiceSystemModel;

namespace InvoiceSystemBL
{
    public class ProductService : IProductService
    {
        private readonly ExcelHelper _excelHelper;

        public ProductService(string filePath)
        {
            _excelHelper = new ExcelHelper(filePath);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _excelHelper.ReadProducts();
        }

        public Product GetProduct(int id)
        {
            return _excelHelper.ReadProducts().FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            var products = _excelHelper.ReadProducts();
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            _excelHelper.WriteProducts(products);
            return product;
        }

        public bool UpdateProduct(int id, Product product)
        {
            var products = _excelHelper.ReadProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.CategoryId = product.CategoryId;

            _excelHelper.WriteProducts(products);
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var products = _excelHelper.ReadProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return false;
            }

            products.Remove(product);
            _excelHelper.WriteProducts(products);
            return true;
        }
    }
}
