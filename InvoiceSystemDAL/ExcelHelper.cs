namespace InvoiceSystemDAL
{
    using ClosedXML.Excel;
    using InvoiceSystemModel;
    using System.Collections.Generic;
    using System.IO;

    public class ExcelHelper
    {
        private readonly string _filePath;
        private XLWorkbook _workbook;

        public ExcelHelper(string filePath)
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            //_filePath = filePath;
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            bool fileExists = File.Exists(_filePath);

            if (!fileExists)
            {
                CreateNewFileWithWorksheets();
            }
            else
            {
                LoadWorkbook();
            }
        }

        private void CreateNewFileWithWorksheets()
        {
            using (_workbook = new XLWorkbook())
            {
                _workbook.AddWorksheet("Products");
                _workbook.AddWorksheet("Categories");
                _workbook.AddWorksheet("Customers");
                _workbook.AddWorksheet("Invoices");

                _workbook.SaveAs(_filePath);
            }
        }

        private void LoadWorkbook()
        {
            _workbook = new XLWorkbook(_filePath);
        }

        private void SaveWorkbook()
        {
            _workbook.SaveAs(_filePath);
        }

        private void AddMissingWorksheets()
        {
            var worksheetNames = _workbook.Worksheets.Select(ws => ws.Name).ToList();

            if (!worksheetNames.Contains("Products"))
            {
                _workbook.AddWorksheet("Products");
            }
            if (!worksheetNames.Contains("Categories"))
            {
                _workbook.AddWorksheet("Categories");
            }
            if (!worksheetNames.Contains("Customers"))
            {
                _workbook.AddWorksheet("Customers");
            }
            if (!worksheetNames.Contains("Invoices"))
            {
                _workbook.AddWorksheet("Invoices");
            }
        }

        public List<Product> ReadProducts()
        {
            var products = new List<Product>();

            var worksheet = _workbook.Worksheet("Products");
            if (worksheet != null)
            {
                var range = worksheet.RangeUsed();
                if (range != null)
                {
                    foreach (var row in range.RowsUsed().Skip(1)) // Skip header row
                    {
                        var product = new Product
                        {
                            Id = row.Cell(1).GetValue<int>(),
                            Name = row.Cell(2).GetValue<string>(),
                            Description = row.Cell(3).GetValue<string>(),
                            Price = row.Cell(4).GetValue<decimal>(),
                            Quantity = row.Cell(5).GetValue<int>(),
                            CategoryId = row.Cell(6).GetValue<int>()
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        public void WriteProducts(List<Product> products)
        {
            var worksheet = _workbook.Worksheets.Add("Products");

            // Header row
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Description";
            worksheet.Cell(1, 4).Value = "Price";
            worksheet.Cell(1, 5).Value = "Quantity";
            worksheet.Cell(1, 6).Value = "CategoryId";

            // Data rows
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                worksheet.Cell(i + 2, 1).Value = product.Id;
                worksheet.Cell(i + 2, 2).Value = product.Name;
                worksheet.Cell(i + 2, 3).Value = product.Description;
                worksheet.Cell(i + 2, 4).Value = product.Price;
                worksheet.Cell(i + 2, 5).Value = product.Quantity;
                worksheet.Cell(i + 2, 6).Value = product.CategoryId;
            }

            SaveWorkbook();
        }

        public List<Category> ReadCategories()
        {
            var categories = new List<Category>();

            var worksheet = _workbook.Worksheet("Categories");
            if (worksheet != null)
            {
                var range = worksheet.RangeUsed();
                if (range != null)
                {
                    foreach (var row in range.RowsUsed().Skip(1)) // Skip header row
                    {
                        var category = new Category
                        {
                            Id = row.Cell(1).GetValue<int>(),
                            Name = row.Cell(2).GetValue<string>(),
                            Description = row.Cell(3).GetValue<string>()
                        };
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public void WriteCategories(List<Category> categories)
        {
            var worksheet = _workbook.Worksheets.Add("Categories");

            // Header row
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Description";

            // Data rows
            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];
                worksheet.Cell(i + 2, 1).Value = category.Id;
                worksheet.Cell(i + 2, 2).Value = category.Name;
                worksheet.Cell(i + 2, 3).Value = category.Description;
            }

            SaveWorkbook();
        }

        public List<Customer> ReadCustomers()
        {
            var customers = new List<Customer>();

            var worksheet = _workbook.Worksheet("Customers");
            if (worksheet != null)
            {
                var range = worksheet.RangeUsed();
                if (range != null)
                {
                    foreach (var row in range.RowsUsed().Skip(1)) // Skip header row
                    {
                        var customer = new Customer
                        {
                            Id = row.Cell(1).GetValue<int>(),
                            Name = row.Cell(2).GetValue<string>(),
                            Email = row.Cell(3).GetValue<string>(),
                            Address = row.Cell(4).GetValue<string>(),
                            ContactNumber = row.Cell(5).GetValue<string>()
                        };
                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        public void WriteCustomers(List<Customer> customers)
        {
            var worksheet = _workbook.Worksheets.Add("Customers");

            // Header row
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Email";
            worksheet.Cell(1, 4).Value = "Address";
            worksheet.Cell(1, 5).Value = "ContactNumber";

            // Data rows
            for (int i = 0; i < customers.Count; i++)
            {
                var customer = customers[i];
                worksheet.Cell(i + 2, 1).Value = customer.Id;
                worksheet.Cell(i + 2, 2).Value = customer.Name;
                worksheet.Cell(i + 2, 3).Value = customer.Email;
                worksheet.Cell(i + 2, 4).Value = customer.Address;
                worksheet.Cell(i + 2, 5).Value = customer.ContactNumber;
            }

            SaveWorkbook();
        }

        public void WriteInvoice(Invoice invoice)
        {
            var worksheet = _workbook.Worksheets.Add("Invoices");

            // Header row
            worksheet.Cell(1, 1).Value = "CustomerName";
            worksheet.Cell(1, 2).Value = "CustomerEmail";
            worksheet.Cell(1, 3).Value = "Product";
            worksheet.Cell(1, 4).Value = "Quantity";
            worksheet.Cell(1, 5).Value = "Price";
            worksheet.Cell(1, 6).Value = "Subtotal";
            worksheet.Cell(1, 7).Value = "DiscountPercentage";
            worksheet.Cell(1, 8).Value = "Total";
            worksheet.Cell(1, 9).Value = "PaymentOption";
            worksheet.Cell(1, 10).Value = "InvoiceDate";

            // Data rows
            for (int i = 0; i < invoice.Products.Count; i++)
            {
                var product = invoice.Products[i];
                worksheet.Cell(i + 2, 1).Value = invoice.CustomerName;
                worksheet.Cell(i + 2, 2).Value = invoice.CustomerEmail;
                worksheet.Cell(i + 2, 3).Value = product.Name;
                worksheet.Cell(i + 2, 4).Value = product.Quantity;
                worksheet.Cell(i + 2, 5).Value = product.Price;
                worksheet.Cell(i + 2, 6).Value = product.Price * product.Quantity; // Subtotal for each product
                worksheet.Cell(i + 2, 7).Value = invoice.DiscountPercentage;
                worksheet.Cell(i + 2, 8).Value = invoice.Total;
                worksheet.Cell(i + 2, 9).Value = invoice.PaymentOption;
                worksheet.Cell(i + 2, 10).Value = invoice.InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss");
            }

            SaveWorkbook();
        }

        public void WriteExceptionLog(ExceptionLogCapture logEntry)
        {
            var worksheet = GetOrCreateWorksheet("ExceptionLog");

            // Header row (if not already created)
            if (worksheet.Row(1).Cell(1).Value.ToString() != "Timestamp")
            {
                worksheet.Cell(1, 1).Value = "Timestamp";
                worksheet.Cell(1, 2).Value = "ExceptionMessage";
                worksheet.Cell(1, 3).Value = "StackTrace";
            }

            // Find next empty row
            int currentRow = worksheet.LastRowUsed().RowNumber() + 1;

            // Write log entry
            worksheet.Cell(currentRow, 1).Value = logEntry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            worksheet.Cell(currentRow, 2).Value = logEntry.ExceptionMessage;
            worksheet.Cell(currentRow, 3).Value = logEntry.StackTrace;

            SaveWorkbook();
        }

        private IXLWorksheet GetOrCreateWorksheet(string worksheetName)
        {
            var worksheet = _workbook.Worksheets.FirstOrDefault(ws => ws.Name == worksheetName);
            if (worksheet == null)
            {
                worksheet = _workbook.Worksheets.Add(worksheetName);
            }
            return worksheet;
        }

        public void Dispose()
        {
            _workbook?.Dispose();
        }
    }
}
