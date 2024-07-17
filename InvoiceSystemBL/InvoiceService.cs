using InvoiceSystemDAL;
using InvoiceSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemBL
{
    public class InvoiceService
    {
        private readonly ExcelHelper _excelHelper;

        public InvoiceService(string filePath)
        {
            _excelHelper = new ExcelHelper(filePath);
        }

        public Invoice GenerateInvoice(InvoiceRequest request)
        {
            // Calculate total amount
            decimal subtotal = CalculateSubtotal(request.Products);
            decimal total = CalculateTotal(subtotal, request.DiscountPercentage);

            // Create invoice object
            var invoice = new Invoice
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                Products = request.Products,
                Subtotal = subtotal,
                DiscountPercentage = request.DiscountPercentage,
                Total = total,
                PaymentOption = request.PaymentOption,
                InvoiceDate = DateTime.UtcNow
            };

            // Save invoice to Excel or database
            _excelHelper.WriteInvoice(invoice);

            return invoice;
        }

        private decimal CalculateSubtotal(List<Product> products)
        {
            decimal subtotal = 0;

            foreach (var product in products)
            {
                subtotal += product.Price * product.Quantity;
            }

            return subtotal;
        }

        private decimal CalculateTotal(decimal subtotal, decimal discountPercentage)
        {
            decimal discountAmount = subtotal * (discountPercentage / 100);
            decimal total = subtotal - discountAmount;

            return total;
        }
    }
}
