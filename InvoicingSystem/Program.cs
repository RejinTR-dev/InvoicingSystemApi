using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using InvoiceSystemBL;
using InvoiceSystemDAL;
using InvoiceSystemModel;
using InvoicingSystem.ExceptionLoger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<ExcelHelper>(); // Register ExcelHelper as singleton
builder.Services.AddScoped<ExcelHelper>(_ => new ExcelHelper(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xlsx")));
builder.Services.AddScoped<IProductService, ProductService>(_ => new ProductService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xlsx"))); // Register ProductService
builder.Services.AddScoped<InvoiceService>(_ => new InvoiceService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xlsx"))); // Register InvoiceService
builder.Services.AddScoped<CustomerService>(_ => new CustomerService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xlsx"))); // Register CustomerService
builder.Services.AddScoped<CategoryService>(_ => new CategoryService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xlsx"))); // Register CategoryService
// Add MVC with filters
builder.Services.AddScoped<GlobalExceptionFilter>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
