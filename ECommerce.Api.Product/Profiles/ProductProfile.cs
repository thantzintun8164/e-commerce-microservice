using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerce.Api.Orders.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Products.Db.Product, Products.Models.Product>();
        }
    }
}
