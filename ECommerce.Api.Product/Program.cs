using ECommerce.Api.Orders.Profiles;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductsProvider, ProductsProvider>();
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});
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
