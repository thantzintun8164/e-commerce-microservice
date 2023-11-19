using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Orders.Db.Order, Orders.Models.Order>();
            CreateMap<Orders.Db.OrderItem, Orders.Models.OrderItem>();
        }
    }
}
