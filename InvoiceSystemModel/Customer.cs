using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemModel
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string ContactNumber { get; set; }
    }

}
