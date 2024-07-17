using InvoiceSystemDAL;
using InvoiceSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemBL
{
    public class CustomerService
    {
        private readonly ExcelHelper _excelHelper;

        public CustomerService(string filePath)
        {
            _excelHelper = new ExcelHelper(filePath);
        }

        public List<Customer> GetCustomers()
        {
            return _excelHelper.ReadCustomers();
        }

        public void AddCustomer(Customer customer)
        {
            // Perform any validation here if necessary
            _excelHelper.WriteCustomers(new List<Customer> { customer });
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            customer.Id = id; // Ensure the ID in the object matches the ID in the route
            var customers = _excelHelper.ReadCustomers();
            var existingCustomer = customers.FirstOrDefault(c => c.Id == id);

            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
                existingCustomer.ContactNumber = customer.ContactNumber;

                _excelHelper.WriteCustomers(customers);
            }
            // Optionally, throw an exception or handle if customer not found
        }

        public void DeleteCustomer(int id)
        {
            var customers = _excelHelper.ReadCustomers();
            var existingCustomer = customers.FirstOrDefault(c => c.Id == id);

            if (existingCustomer != null)
            {
                customers.Remove(existingCustomer);
                _excelHelper.WriteCustomers(customers);
            }
            // Optionally, throw an exception or handle if customer not found
        }
    }
}
