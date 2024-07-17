using System.ComponentModel.DataAnnotations;

namespace InvoiceSystemModel
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

}
