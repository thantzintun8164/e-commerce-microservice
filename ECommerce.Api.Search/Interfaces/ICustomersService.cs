using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsnyc(int id);
    }
}
