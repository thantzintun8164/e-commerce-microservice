using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Providers;
using ECommerce.Api.Orders.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();
builder.Services.AddAutoMapper(typeof(CustomerProfile));
builder.Services.AddDbContext<CustomersDbContext>(options =>
{
    options.UseInMemoryDatabase("Customers");
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
