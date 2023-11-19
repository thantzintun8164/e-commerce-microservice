
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider productsProvider)
        {
            this.ordersProvider = productsProvider;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetCustomersAsync()
        //{
        //    var result = await ordersProvider.GetOrdersAsync();
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Orders);
        //    }
        //    return NotFound();
        //}

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await ordersProvider.GetOrderAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound();
        }

    }
}
