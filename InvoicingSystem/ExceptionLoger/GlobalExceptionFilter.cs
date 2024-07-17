namespace InvoicingSystem.ExceptionLoger
{
    using InvoiceSystemDAL;
    using InvoiceSystemModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ExcelHelper _excelHelper;

        public GlobalExceptionFilter(ExcelHelper excelHelper)
        {
            _excelHelper = excelHelper;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception to Excel
            LogExceptionToExcel(context.Exception);

            // Optionally, handle or modify the exception context
            context.ExceptionHandled = true;
            context.Result = new ObjectResult("Internal server error")
            {
                StatusCode = 500
            };
        }

        private void LogExceptionToExcel(Exception exception)
        {
            try
            {
                // Create a log entry in Excel
                var logEntry = new ExceptionLogCapture
                {
                    Timestamp = DateTime.UtcNow,
                    ExceptionMessage = exception.Message,
                    StackTrace = exception.StackTrace
                };

                _excelHelper.WriteExceptionLog(logEntry);
            }
            catch (Exception ex)
            {
                // Handle the logging failure as needed
                Console.WriteLine($"Failed to log exception to Excel: {ex.Message}");
            }
        }
    }

}
