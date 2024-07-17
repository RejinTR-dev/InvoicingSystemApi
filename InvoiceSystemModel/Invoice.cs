using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemModel
{
    public class InvoiceRequest
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<Product> Products { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string PaymentOption { get; set; }
    }

    public class Invoice
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<Product> Products { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal Total { get; set; }
        public string PaymentOption { get; set; }
        public DateTime InvoiceDate { get; set; }
    }


}
