using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemModel
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
        bool UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
    }
}
