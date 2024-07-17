using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystemModel
{
    public class ExceptionLogCapture
    {
        public DateTime Timestamp { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
