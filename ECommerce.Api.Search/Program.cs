using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("OrdersService", client =>
{
    var ordersServiceUri = builder.Configuration.GetSection("Services:Orders").Get<Uri>();
    client.BaseAddress = ordersServiceUri;
});
builder.Services.AddHttpClient("ProductsService", client =>
{
    var productsServiceUri = builder.Configuration.GetSection("Services:Products").Get<Uri>();
    client.BaseAddress = productsServiceUri;
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("CustomersService", client =>
{
    var customersServiceUri = builder.Configuration.GetSection("Services:Customers").Get<Uri>();
    client.BaseAddress = customersServiceUri;
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<ICustomersService, CustomersService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
