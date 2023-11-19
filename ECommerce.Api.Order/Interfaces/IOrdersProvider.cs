using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, Order Order, string ErrorMessage)> GetOrderAsync(int customerId);
    }
}
