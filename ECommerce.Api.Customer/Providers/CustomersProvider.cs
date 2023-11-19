
using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "John Smith", Address = "20 Elm St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Elon Musk", Address = "30 Main St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Michel", Address = "Rose St." });
                
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if(customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Customers.Db.Customer>,IEnumerable<Customers.Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false!, null!, "Not Found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false!, null!, ex.Message);
                throw;
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customer != null)
                {
                    var result = mapper.Map<Customers.Db.Customer, Customers.Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false!, null!, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false!, null!, ex.Message);
                throw;
            }
        }
    }
}
