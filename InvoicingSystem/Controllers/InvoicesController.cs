using Microsoft.AspNetCore.Mvc;

namespace InvoicingSystem.Controllers
{
    using global::InvoicingSystem.ExceptionLoger;
    using InvoiceSystemBL;
    using InvoiceSystemDAL;
    using InvoiceSystemModel;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    [ApiController]
    [Route("api/invoices")]
    [ServiceFilter(typeof(GlobalExceptionFilter))]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
        }

        [HttpPost("generate")]
        public ActionResult GenerateInvoice(InvoiceRequest request)
        {
            try
            {
                var invoice = _invoiceService.GenerateInvoice(request);
                return Ok(invoice); // Return generated invoice
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
