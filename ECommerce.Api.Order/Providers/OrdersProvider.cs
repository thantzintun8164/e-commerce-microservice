using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                //dbContext.Orders.Add(new Db.Order() { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Total= 10, 
                //    Items = { new Db.OrderItem() { Id = 1, ProductId = 1, Quantity = 10, UnitPrice = 100 } } });
               

                dbContext.SaveChanges();
            }
        }

        //public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        //{
        //    try
        //    {
        //        var products = await dbContext.Orders.ToListAsync();
        //        if(products != null && products.Any())
        //        {
        //            var result = mapper.Map<IEnumerable<Orders.Db.Order>,IEnumerable<Orders.Models.Order>>(products);
        //            return (true, result, null);
        //        }
        //        return (false!, null!, "Not Found");
        //    }
        //    catch(Exception ex)
        //    {
        //        logger?.LogError(ex.ToString());
        //        return (false!, null!, ex.Message);
        //        throw;
        //    }
        //}

        public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int customerId)
        {
            try
            {
                var product = await dbContext.Orders.FirstOrDefaultAsync(p => p.CustomerId == customerId);
                if (product != null)
                {
                    var result = mapper.Map<Orders.Db.Order, Orders.Models.Order>(product);
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
