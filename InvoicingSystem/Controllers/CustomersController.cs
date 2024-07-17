using InvoiceSystemBL;
using InvoiceSystemDAL;
using InvoiceSystemModel;
using InvoicingSystem.ExceptionLoger;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingSystem.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [ServiceFilter(typeof(GlobalExceptionFilter))]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                var customers = _customerService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            try
            {
                _customerService.AddCustomer(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, Customer customer)
        {
            try
            {
                _customerService.UpdateCustomer(id, customer);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
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
