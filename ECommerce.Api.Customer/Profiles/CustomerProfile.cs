using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerce.Api.Orders.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customers.Db.Customer, Customers.Models.Customer>();
        }
    }
}
